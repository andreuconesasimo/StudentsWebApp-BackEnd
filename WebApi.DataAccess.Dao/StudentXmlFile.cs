using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Models;
using WebApi.Common.Logic.Properties;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.DataAccess.Dao
{
    public class StudentXmlFile : IFileStudent
    {
        #region Properties
        private string Ruta { get; set; }
        #endregion

        #region Fields
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        
        #region Constructors

        public StudentXmlFile()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + ConfigStrings.XmlFile;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        public StudentXmlFile(string ruta)
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
                List<Student> alumnosFicheroExistente = DeserializeXml();
                alumnosFicheroExistente.Add(alumno);
                var xmlNuevo = SerializeXml(alumnosFicheroExistente);
                FileUtils.EscribirFichero(xmlNuevo, Ruta);
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
                List<Student> alumnosFicheroExistente = DeserializeXml();
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

        public List<Student> GetSingletonInstance()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                ListadoAlumnosXml listadoAlumnosXml = ListadoAlumnosXml.Instance();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return listadoAlumnosXml.ListadoAlumnos;
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
                List<Student> alumnos = DeserializeXml();
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

        public List<Student> DeleteByGuid(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);

                List<Student> alumnosExistentes = DeserializeXml();
                var encontrado = false;                
                var alumno = alumnosExistentes.FirstOrDefault((a) => a.GUID == new Guid(guid));
                encontrado = alumnosExistentes.Remove(alumno);               
                if (encontrado)
                {
                    var xmlNuevo = SerializeXml(alumnosExistentes);
                    FileUtils.EscribirFichero(xmlNuevo, Ruta);
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

                List<Student> alumnosExistentes = DeserializeXml();
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
                    var xmlNuevo = SerializeXml(alumnosExistentes);
                    FileUtils.EscribirFichero(xmlNuevo, Ruta);
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

        private string SerializeXml(List<Student> alumnosFicheroExistente)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                using (StringWriter writer = new StringWriter())
                {
                    xmlSerializer.Serialize(writer, alumnosFicheroExistente);
                    logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                DAOException dAOException = new DAOException(ex.Message, ex.InnerException);
                logger.Exception(dAOException);
                throw;
            }
        }

        private List<Student> DeserializeXml()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<Student> alumnosFicheroExistente = new List<Student>();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));

                if (File.Exists(Ruta) && new FileInfo(Ruta).Length != 0)
                {
                    alumnosFicheroExistente = (List<Student>)xmlSerializer.Deserialize(new StringReader(File.ReadAllText(Ruta)));
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
