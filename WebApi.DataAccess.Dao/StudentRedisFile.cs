using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Helpers;
using WebApi.Common.Logic.Models;
using WebApi.Common.Logic.Properties;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.DataAccess.Dao
{
    public class StudentRedisFile : IFileStudent
    {
        #region Fields
        private readonly string ConnectionString = Configuration.DbConnectionString();
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        public StudentRedisFile()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);                
            }
        }
        public Student Add(Student student)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                var redis = RedisStore.RedisCache;
                var students = GetAll();
                students.Add(student);
                redis.StringSet(ConfigStrings.StudentsList, JsonConvert.SerializeObject(students));
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return student;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public List<Student> DeleteByGuid(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                var redis = RedisStore.RedisCache;
                var students = GetAll();
                students.RemoveAll(x => x.GUID == new Guid(guid));
                redis.StringSet(ConfigStrings.StudentsList, JsonConvert.SerializeObject(students));
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return students;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public List<Student> GetAll()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                var redis = RedisStore.RedisCache;
                var key = ConfigStrings.StudentsList;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return JsonConvert.DeserializeObject<List<Student>>(redis.StringGet(key));
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public List<Student> GetSingletonInstance()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return GetAll();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public Student Select(Guid guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                var redis = RedisStore.RedisCache;
                var students = GetAll();
                var student = students.First(s => s.GUID == guid);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return student;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public void Update(Student student)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                var redis = RedisStore.RedisCache;
                var students = GetAll();
                int index = students.FindIndex(s => s.GUID == student.GUID);
                students[index].Name = student.Name;
                students[index].Surname = student.Surname;
                students[index].DNI = student.DNI;
                students[index].BirthDate = student.BirthDate;
                students[index].Age = student.Age;
                redis.StringSet(ConfigStrings.StudentsList, JsonConvert.SerializeObject(students));
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }
    }
}
