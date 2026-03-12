using MongoDB.Bson;
using MongoDB.Driver;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    internal class AttendanceRepository : IAttendanceRepository
    {
        private readonly IMongoCollection<Attendance> _collection;

        public AttendanceRepository()
        {
            var db = new Data.MongoDb();
            _collection = db.Attendance;
        }

        public async Task<Attendance> GetByDate(ObjectId studentId, DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            return await _collection.Find(a =>
                a.StudentId == studentId &&
                a.Datum >= start &&
                a.Datum < end
            ).FirstOrDefaultAsync();
        }

        public async Task<List<Attendance>> GetByStudentId(ObjectId studentId)
        {
            var list = await _collection.Find(a => a.StudentId == studentId)
                .SortBy(a => a.Datum)
                .ToListAsync();

            foreach (var a in list)
                a.Datum = a.Datum.Date;

            return list;
        }

        public async Task Add(Attendance attendance)
        {
            attendance.Datum = attendance.Datum.Date.ToLocalTime();
            await _collection.InsertOneAsync(attendance);
        }

        public async Task Update(Attendance attendance)
        {
            attendance.Datum = attendance.Datum.Date.ToLocalTime();

            var filter = Builders<Attendance>.Filter.Eq(a => a.Id, attendance.Id);
            await _collection.ReplaceOneAsync(filter, attendance);
        }

    }
}
