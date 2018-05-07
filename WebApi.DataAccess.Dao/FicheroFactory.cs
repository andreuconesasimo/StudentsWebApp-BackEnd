using System;
using System.Reflection;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Enums;
using WebApi.Common.Logic.Properties;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.DataAccess.Dao
{
    public class FicheroFactory
    {
        #region Fields
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType); 
        #endregion

        #region Public methods
        public IFileStudent CrearFichero(Extension extension)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
                switch (extension)
                {
                    case Extension.TXT:
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Extension.TXT + " " + LogStrings.Starts);
                        return new StudentTxtFile();
                    case Extension.JSON:
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Extension.JSON + " " + LogStrings.Starts);
                        return new StudentJsonFile();
                    case Extension.XML:
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Extension.XML + " " + LogStrings.Starts);
                        return new StudentXmlFile();
                    case Extension.SQL:
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Extension.SQL + " " + LogStrings.Starts);
                        return new StudentSqlFile();
                    case Extension.SP:
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Extension.SQL + " " + LogStrings.Starts);
                        return new StudentSPFile();
                    case Extension.Redis:
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Extension.Redis + " " + LogStrings.Starts);
                        return new StudentRedisFile();
                    default:
                        logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + Extension.TXT + " " + LogStrings.Starts);
                        return new StudentTxtFile();
                }
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
