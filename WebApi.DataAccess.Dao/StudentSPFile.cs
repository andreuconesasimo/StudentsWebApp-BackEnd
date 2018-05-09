using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Helpers;
using WebApi.Common.Logic.Models;
using WebApi.Common.Logic.Properties;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.DataAccess.Dao
{
    public class StudentSPFile : IFileStudent
    {

        #region Fields
        private readonly string ConnectionString = Configuration.DbConnectionString();
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructors
        public StudentSPFile()
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
        #endregion

        #region Public methods
        public Student Add(Student alumno)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("spInsertStudent", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@GUID", SqlDbType.NVarChar).Value = alumno.GUID.ToString();
                        command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = alumno.Name;
                        command.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = alumno.Surname;
                        command.Parameters.Add("@DNI", SqlDbType.NVarChar).Value = alumno.DNI;
                        command.Parameters.Add("@FechaNacimiento", SqlDbType.DateTime2).Value = alumno.BirthDate;
                        command.Parameters.Add("@Edad", SqlDbType.Int).Value = alumno.Age;
                        command.Parameters.Add("@FechaCompletaAlta", SqlDbType.DateTime2).Value = alumno.RegistryDate;
                        conn.Open();
                        int result = command.ExecuteNonQuery();
                        if (result < 0)
                            logger.Error(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.InsertError);
                        else
                            logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());
                    }
                }
                return alumno;
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
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("spDeleteStudent", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@GUID", SqlDbType.NVarChar).Value = guid;
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return GetAll();
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
                List<Student> alumnos = new List<Student>();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("spSelectAllStudents", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Student alumno = new Student(Guid.Parse(reader.GetString(1)), reader.GetInt32(0),
                                        reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5),
                                        reader.GetInt32(6), reader.GetDateTime(7));
                                    logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());
                                    alumnos.Add(alumno);
                                }
                            }
                        }
                    }
                }
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
                List<Student> alumnos = GetAll();
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

        public Student Select(Guid guid)
        {
            try
            {
                Student alumno = new Student();
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spSelectByGuid", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@GUID", SqlDbType.NVarChar).Value = guid.ToString();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    alumno = new Student(Guid.Parse(reader.GetString(1)), reader.GetInt32(0),
                                        reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5),
                                        reader.GetInt32(6), reader.GetDateTime(7));
                                    logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());
                                }
                            }
                        }
                    }
                }
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
                return alumno;
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
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("spUpdateByGuid", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GUID", alumno.GUID);
                        command.Parameters.AddWithValue("@Nombre", alumno.Name);
                        command.Parameters.AddWithValue("@Apellidos", alumno.Surname);
                        command.Parameters.AddWithValue("@DNI", alumno.DNI);
                        command.Parameters.AddWithValue("@FechaNacimiento", alumno.BirthDate);
                        command.Parameters.AddWithValue("@Edad", alumno.Age);
                        command.ExecuteNonQuery();
                    }
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
    }
}
