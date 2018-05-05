using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using WebApi.Common.Logic.Properties;

namespace WebApi.Common.Logic.Helpers
{
    public static class Configuration
    {
        #region Fields

        private static readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Fields

        #region Public methods

        public static string DbConnectionString()
        {
            try
            {
                //ConfigurationManager.AppSettings[]                
                var conn = Environment.GetEnvironmentVariable(ConfigStrings.DbConnection, EnvironmentVariableTarget.User);
                if (String.IsNullOrEmpty(conn))
                {
                    Environment.SetEnvironmentVariable(ConfigStrings.DbConnection, ConfigStrings.DbConnectionString, EnvironmentVariableTarget.User);
                    conn = ConfigStrings.DbConnectionString;
                }
                return conn;
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        public static void DbCreateProcedures()
        {
            try
            {
                //CreateDataBase();
                CreateStudentsTable();
                CreateInsertProcedure();
                CreateSelectAllProcedure();
                CreateSelectByGuid();
                CreateUpdateProcedure();
                CreateDeleteProcedure();
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        #endregion Public methods

        #region Private methods

        private static bool ExistsProcedure(string name)
        {            
            var query = "select * from sysobjects where type='P' and name='"+ name + "'";
            bool spExists = false;
            using (SqlConnection conn = new SqlConnection(DbConnectionString()))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                            spExists = reader.Read();                            
                    }
                }
            }
            return spExists;
        }

        private static void CreateInsertProcedure()
        {
            try
            {
                if (!ExistsProcedure(ConfigStrings.spInsertStudent))
                {
                    StringBuilder sbSP = new StringBuilder();
                    var procedure = ConfigStrings.spInsertStudentQuery;
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateSelectAllProcedure()
        {
            try
            {
                if (!ExistsProcedure(ConfigStrings.spSelectAllStudents))
                {
                    StringBuilder sbSP = new StringBuilder();
                    var procedure = ConfigStrings.spSelectAllStudentsQuery;
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateSelectByGuid()
        {
            try
            {
                if (!ExistsProcedure(ConfigStrings.spSelectByGuid))
                {
                    StringBuilder sbSP = new StringBuilder();
                    var procedure = ConfigStrings.spSelectByGuidQuery;
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateUpdateProcedure()
        {            
            try
            {
                if (!ExistsProcedure(ConfigStrings.spUpdateByGuid))
                {
                    StringBuilder sbSP = new StringBuilder();
                    var procedure = ConfigStrings.spSelectByGuidQuery;
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateDeleteProcedure()
        {
            try
            {
                if (!ExistsProcedure(ConfigStrings.spDeleteStudent))
                {
                    StringBuilder sbSP = new StringBuilder();
                    var procedure = ConfigStrings.spDeleteStudentQuery;
                    sbSP.AppendLine(procedure);
                    using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand(sbSP.ToString(), connection))
                        {
                            connection.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static void CreateDataBase()
        {
            String str;
            SqlConnection myConn = new SqlConnection("Server=localhost;Integrated security=True;database=master");
            str = ConfigStrings.spCreateDatabaseQuery;

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        private static void CreateStudentsTable()
        {
            try
            {                
                if (!TableExists(ConfigStrings.Students))
                {
                    using (var conn = new SqlConnection(DbConnectionString()))
                    {
                        conn.Open();
                        var command = conn.CreateCommand();
                        command.CommandText = ConfigStrings.spCreateStudentsTableQuery;
                        command.ExecuteNonQuery();                        
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
        }

        private static bool TableExists(string name)
        {
            bool exists = false;
            try
            {                
                using (SqlConnection connection = new SqlConnection(DbConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(@"IF EXISTS(
  SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
  WHERE TABLE_NAME = @table) 
  SELECT 1 ELSE SELECT 0", connection);
                    cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = name;
                    var result = (int)cmd.ExecuteScalar();
                    exists = (result == 1);
                }
            }
            catch (Exception ex)
            {
                logger.Exception(ex);
                throw;
            }
            return exists;
        }

        #endregion Private methods
    }
}
