using System;
using System.Collections.Generic;
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
                throw;
            }
        }
        public Student Add(Student student)
        {
            throw new NotImplementedException();
        }

        public List<Student> DeleteByGuid(string guid)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetSingletonInstance()
        {
            throw new NotImplementedException();
        }

        public Student Select(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
