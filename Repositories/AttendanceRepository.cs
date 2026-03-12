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
            return await _collection.Find(a =>
            a.StudentId == studentId &&
            a.Datum.Date == date.Date
            ).FirstOrDefaultAsync();
        }
        public async Task<List<Attendance>> GetByStudentId(ObjectId studentId)
        {
            return await _collection.Find(a => a.StudentId == studentId)
                 .SortBy(a => a.Datum)
                .ToListAsync();
        }
        public async Task Add(Attendance attendance) =>
            await _collection.InsertOneAsync(attendance);
        public async Task Update(Attendance attendance) =>
            await _collection.ReplaceOneAsync(a => a.Id == attendance.Id, attendance);
    }
}
