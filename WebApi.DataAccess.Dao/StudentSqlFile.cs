using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Helpers;
using WebApi.Common.Logic.Models;
using WebApi.Common.Logic.Properties;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.DataAccess.Dao
{
    public class StudentSqlFile : IFileStudent
    {
        #region Fields
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string ConnectionString = Configuration.DbConnectionString();
        private readonly string queryInsert = "INSERT INTO dbo.Students (GUID, Nombre, Apellidos, DNI, FechaNacimiento, Edad, FechaCompletaAlta) VALUES (@GUID,@Nombre, @Apellidos, @DNI, @FechaNacimiento, @Edad, @FechaCompletaAlta)";
        private readonly string querySelectAll = "Select * from dbo.Students";
        private readonly string queryDeleteByGuid = "DELETE from dbo.Students WHERE GUID = @GUID";
        private readonly string querySelectByGuid = "Select * from dbo.Students where GUID = @GUID";
        private readonly string queryUpdateByGuid = "UPDATE dbo.Students SET Nombre = @Nombre, Apellidos = @Apellidos, DNI = @DNI, FechaNacimiento = @FechaNacimiento, Edad = @Edad Where GUID = @GUID";
        #endregion

        #region Constructors
        public StudentSqlFile()
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
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
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(queryInsert, connection))
                    {
                        command.Parameters.AddWithValue("@GUID", alumno.GUID.ToString());
                        command.Parameters.AddWithValue("@Nombre", alumno.Name);
                        command.Parameters.AddWithValue("@Apellidos", alumno.Surname);
                        command.Parameters.AddWithValue("@DNI", alumno.DNI);
                        command.Parameters.AddWithValue("@FechaNacimiento", alumno.BirthDate);
                        command.Parameters.AddWithValue("@Edad", alumno.Age);
                        command.Parameters.AddWithValue("@FechaCompletaAlta", alumno.RegistryDate);

                        connection.Open();
                        var result = command.ExecuteNonQuery();
                        if (result < 0)
                            logger.Error(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.InsertError);
                        else
                            logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + alumno.ToString());
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

        public List<Student> DeleteByGuid(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(queryDeleteByGuid, con))
                    {
                        command.Parameters.AddWithValue("@GUID", guid);
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
                    using (SqlCommand command = new SqlCommand(querySelectAll, connection))
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
                    using (SqlCommand command = new SqlCommand(querySelectByGuid, con))
                    {
                        command.Parameters.AddWithValue("@GUID", guid.ToString());
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    alumno = new Student(Guid.Parse(reader.GetString(1)), reader.GetInt32(0),
                                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                                    reader.GetDateTime(5), reader.GetInt32(6), reader.GetDateTime(7));
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
                    using (SqlCommand command = new SqlCommand(queryUpdateByGuid, con))
                    {
                        command.Parameters.AddWithValue("@GUID", alumno.GUID);
                        command.Parameters.AddWithValue("@Nombre", alumno.Name);
                        command.Parameters.AddWithValue("@Apellidos", alumno.Surname);
                        command.Parameters.AddWithValue("@DNI", alumno.DNI);
                        command.Parameters.AddWithValue("@FechaNacimiento", alumno.BirthDate);
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
