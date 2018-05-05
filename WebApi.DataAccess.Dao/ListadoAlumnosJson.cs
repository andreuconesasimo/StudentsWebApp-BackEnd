using System.Collections.Generic;
using System;
using System.Reflection;
using WebApi.Common.Logic.Models;
using WebApi.Common.Logic;
using WebApi.DataAccess.Dao.Interfaces;
using WebApi.Common.Logic.Properties;

namespace WebApi.DataAccess.Dao
{
    public class ListadoAlumnosJson
    {
        #region Fields
        private static ListadoAlumnosJson _instance;
        private static object syncLock = new object();
        #endregion

        #region Properties
        public List<Student> ListadoAlumnos { get; set; }
        #endregion

        #region Constructors
        protected ListadoAlumnosJson()
        {
            ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Debug(LogStrings.Instantiating + " " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Singleton);
        }
        #endregion

        #region SingletonInstance
        public static ListadoAlumnosJson Instance()
        {
            ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ListadoAlumnosJson();
                            IFileStudent ficheroAlumno = new StudentJsonFile();
                            _instance.ListadoAlumnos = ficheroAlumno.GetAll();
                        }
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return _instance;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        } 
        #endregion

    }
}
