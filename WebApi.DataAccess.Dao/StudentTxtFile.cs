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
    public class StudentTxtFile : IFileStudent
    {
        #region Properties
        private string Ruta { get; set; }
        #endregion

        #region Fields
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructors
        public StudentTxtFile()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + ConfigStrings.TxtFile;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);

            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public StudentTxtFile(string ruta)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                Ruta = ruta;
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }
        #endregion

        #region Public methods
        public Student Add(Student alumno)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (StreamWriter sw = File.AppendText(Ruta))
                {
                    sw.WriteLine(alumno.ToString());
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return Select(alumno.GUID);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public Student Select(Guid guid)
        {
            string linea;
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (StreamReader sr = new StreamReader(Ruta))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        var alumno = Deserialize(linea);
                        if (alumno.GUID == guid) return alumno;
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return null;
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
                List<Student> alumnos = new List<Student>();
                string linea;

                using (StreamReader sr = new StreamReader(Ruta))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        var alumno = Deserialize(linea);
                        alumnos.Add(alumno);
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnos;
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
                List<Student> alumnos = GetAll();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumnos;

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

                var lines = File.ReadAllLines(Ruta);
                var remaining = lines.Where(x => !x.Contains(guid)).ToArray();
                File.WriteAllLines(Ruta, remaining);

                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);

                return GetAll();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }

        public void Update(Student alumno)
        {
            try
            {
                var lines = File.ReadAllLines(Ruta);
                var line = lines.Where(x => x.Contains(alumno.GUID.ToString())).ToArray();
                var alumnoAeditar = Deserialize(line[0]);
                alumnoAeditar.Name = alumno.Name;
                alumnoAeditar.Surname = alumno.Surname;
                alumnoAeditar.DNI = alumno.DNI;
                alumnoAeditar.BirthDate = alumno.BirthDate;
                alumnoAeditar.Age = alumno.Age;
                lines[lines.ToList().IndexOf(lines.First(x => x.Contains(alumno.GUID.ToString())))] = alumnoAeditar.ToString();
                File.WriteAllLines(Ruta, lines);
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        }
        #endregion

        #region Private methods
        private Student Deserialize(string alumnoTxt)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                List<string> paramsAlumno = alumnoTxt.Split(',').ToList<string>();

                var alumno = new Student(Guid.Parse(paramsAlumno[0]), Convert.ToInt32(paramsAlumno[1]), paramsAlumno[2],
                        paramsAlumno[3], paramsAlumno[4], DateTime.Parse(paramsAlumno[5]),
                        Convert.ToInt32(paramsAlumno[6]), DateTime.Parse(paramsAlumno[7]));
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumno;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw new DAOException(ex.Message, ex.InnerException);
            }
        } 
        #endregion

    }
}