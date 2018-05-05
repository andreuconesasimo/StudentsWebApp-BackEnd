using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Enums;
using WebApi.Common.Logic.Models;
using WebApi.Common.Logic.Properties;
using WebApi.DataAccess.Dao;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.Business.Logic
{
    /// <summary>
    /// Main class of the business logic layer.
    /// Implements the corresponding interface.
    /// </summary>
    public class StudentBL : IStudentBL
    {
        #region Fields
        private IFileStudent ficheroAlumno;
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Main class constructor.
        /// </summary>
        public StudentBL()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);                
                FicheroFactory ficheroFactory = new FicheroFactory();
                ficheroAlumno = ficheroFactory.CrearFichero((Extension)Enum.Parse(typeof(Extension), ConfigurationManager.AppSettings[ConfigStrings.FileType]));                
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message,ex.InnerException);
                logger.Exception(blException);
            }
        }
        #endregion Constructors

        #region Public methods
                
        public Student Add(Student student)
        {
            Student alumnoInsertado = new Student();
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                student.RegistryDate = DateTime.Now;
                student.Age = ComputeAge(DateTime.Now, student.BirthDate);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                alumnoInsertado = ficheroAlumno.Add(student);
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message,ex.InnerException);
                logger.Exception(blException);
            }
            return alumnoInsertado;
        }
        
        public void SelectFileType(Extension extension)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                FicheroFactory ficheroFactory = new FicheroFactory();
                ficheroAlumno = ficheroFactory.CrearFichero(extension);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                students = ficheroAlumno.GetAll();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
            return students;
        }

        public List<Student> GetSingletonInstance()
        {
            List<Student> students = new List<Student>();
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                students = ficheroAlumno.GetSingletonInstance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);                
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
            return students;
        }
              
        public List<Student> Filter(string guid, string nombre, string apellidos, string dni, string id, DateTime dtFechaNacimiento,bool fechaNacimientoChecked, string edad, DateTime dtFechaRegistro, bool fechaRegistroChecked)
        {
            List<Student> result = new List<Student>();
            try
            {
                List<Student> students = GetSingletonInstance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                int idInt, edadInt;
                var query = from alu in students select alu;
                if (!String.IsNullOrEmpty(guid))
                    query = query.Where(alu => alu.GUID.Equals(Guid.Parse(guid)));
                if (!String.IsNullOrEmpty(nombre))
                    query = query.Where(alu => alu.Name.Equals(nombre));
                if (!String.IsNullOrEmpty(apellidos))
                    query = query.Where(alu => alu.Surname.Equals(apellidos));
                if (!String.IsNullOrEmpty(dni))
                    query = query.Where(alu => alu.DNI.Equals(dni));
                if (!String.IsNullOrEmpty(id))
                {
                    idInt = Convert.ToInt32(id);
                    query = query.Where(alu => alu.ID.Equals(idInt));
                }
                if (fechaNacimientoChecked)
                    query = query.Where(alu => alu.BirthDate.Date.Equals(dtFechaNacimiento.Date));
                if (!String.IsNullOrEmpty(edad))
                {
                    edadInt = Convert.ToInt32(edad);
                    query = query.Where(alu => alu.Age.Equals(edadInt));
                }
                if (fechaRegistroChecked)
                    query = query.Where(alu => alu.RegistryDate.Date.Equals(dtFechaRegistro.Date));

                query = query.OrderBy(alu => alu.ID);

                result = query.ToList();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
            return result;
        }

        public List<Student> DeleteByGuid(string guid)
        {
            List<Student> students = new List<Student>();
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                 students = ficheroAlumno.DeleteByGuid(guid);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);                 
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
            return students;
        }

        public Student SelectByGuid(string guid)
        {
            Student student = new Student();
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                student = ficheroAlumno.Select(new Guid(guid));
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);                
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
            return student;
        }

        public void Update(Student student)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                student.Age = ComputeAge(DateTime.Now, student.BirthDate);
                ficheroAlumno.Update(student);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
        }
                
        public int ComputeAge(DateTime fechaCompletaActual, DateTime fechaNacimiento)
        {
            var edad = 0;
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                edad = (Convert.ToInt32((fechaCompletaActual - fechaNacimiento).TotalDays) / 365);
            }
            catch (Exception ex)
            {
                BLException blException = new BLException(ex.Message, ex.InnerException);
                logger.Exception(blException);
            }
            return edad;
        }

        #endregion Public methods
    }
}
