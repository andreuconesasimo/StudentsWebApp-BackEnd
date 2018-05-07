using System;
using System.IO;
using System.Reflection;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Properties;

namespace WebApi.DataAccess.Dao
{
    public static class FileUtils
    {        
        public static void EscribirFichero(string fileContent, string Ruta)
        {
            ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Starts);
            try
            {
                using (StreamWriter sw = File.CreateText(Ruta))
                {
                    sw.WriteLine(fileContent);                    
                }
            }
            catch (Exception ex)
            {                
                logger.Exception(ex);                
                throw;
            }
            logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name + " " + LogStrings.Ends);
        }

    }
}
