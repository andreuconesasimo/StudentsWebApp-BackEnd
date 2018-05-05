using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Models;
using WebApi.Common.Logic.Properties;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.DataAccess.Dao
{
    public class StudentJsonFile : IFileStudent
    {
        #region Properties
        private string Ruta { get; set; }
        #endregion

        #region Fields
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType); 
        #endregion

        #region Constructors
        public StudentJsonFile()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + ConfigStrings.JsonFile;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        public StudentJsonFile(string ruta)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = ruta;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }
        #endregion

        #region Public methods
        public Student Add(Student alumno)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Student> alumnosFicheroExistente = DeserializeJson();
                alumnosFicheroExistente.Add(alumno);
                var jsonNuevo = JsonConvert.SerializeObject(alumnosFicheroExistente, Formatting.Indented);
                FileUtils.EscribirFichero(jsonNuevo, Ruta);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return Select(alumno.GUID);
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        public Student Select(Guid guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Student> alumnosFicheroExistente = DeserializeJson();
                foreach (Student alumno in alumnosFicheroExistente)
                {
                    if (alumno.GUID == guid) return alumno;
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return null;
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        public List<Student> GetAll()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Student> alumnos = DeserializeJson();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnos;
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        public List<Student> GetSingletonInstance()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                ListadoAlumnosJson listadoAlumnosJson = ListadoAlumnosJson.Instance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return listadoAlumnosJson.ListadoAlumnos;
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        public List<Student> DeleteByGuid(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);

                List<Student> alumnosExistentes = DeserializeJson();
                var encontrado = false;
                var alumno = alumnosExistentes.FirstOrDefault((a) => a.GUID == new Guid(guid));
                encontrado = alumnosExistentes.Remove(alumno);                
                if (encontrado)
                {
                    var jsonNuevo = JsonConvert.SerializeObject(alumnosExistentes, Formatting.Indented);
                    FileUtils.EscribirFichero(jsonNuevo, Ruta);
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnosExistentes;
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        public void Update(Student alumno)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);

                List<Student> alumnosExistentes = DeserializeJson();
                var encontrado = false;
                var i = 0;
                while (!encontrado && i < alumnosExistentes.Count)
                {
                    if (alumnosExistentes[i].GUID == alumno.GUID)
                    {
                        alumnosExistentes[i].Name = alumno.Name;
                        alumnosExistentes[i].Surname = alumno.Surname;
                        alumnosExistentes[i].DNI = alumno.DNI;
                        alumnosExistentes[i].Age = alumno.Age;
                        alumnosExistentes[i].BirthDate = alumno.BirthDate;
                        encontrado = true;
                    }
                    ++i;
                }
                if (encontrado)
                {
                    var jsonNuevo = JsonConvert.SerializeObject(alumnosExistentes, Formatting.Indented);
                    FileUtils.EscribirFichero(jsonNuevo, Ruta);
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }
        #endregion

        #region Private methods
        private List<Student> DeserializeJson()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Student> alumnosFicheroExistente = new List<Student>();
                if (File.Exists(Ruta) && new FileInfo(Ruta).Length != 0)
                {
                    alumnosFicheroExistente = JsonConvert.DeserializeObject<List<Student>>(File.ReadAllText(Ruta));
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnosFicheroExistente;
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        } 
        #endregion
    }
}
