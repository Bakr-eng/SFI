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
    public interface IAttendanceRepository
    {
        
        Task<List<Attendance>> GetByStudentId(ObjectId studentId);
        Task<Attendance> GetByDate(ObjectId studentId, DateTime date);
        Task Add(Attendance attendance);
        Task Update(Attendance attendance);
    }
}
