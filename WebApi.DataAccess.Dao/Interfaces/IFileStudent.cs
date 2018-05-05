using System;
using System.Collections.Generic;
using WebApi.Common.Logic.Models;

namespace WebApi.DataAccess.Dao.Interfaces
{
    public interface IFileStudent
    {
        Student Add(Student student);
        Student Select(Guid guid);
        List<Student> GetAll();
        List<Student> GetSingletonInstance();
        List<Student> DeleteByGuid(string guid);
        void Update(Student student);
    }
}