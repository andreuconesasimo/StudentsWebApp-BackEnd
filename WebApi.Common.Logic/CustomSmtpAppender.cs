using System.Reflection;

namespace WebApi.Common.Logic
{
    public class CustomSmtpAppender : log4net.Appender.SmtpAppender
    {
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void SendEmail(string messageBody)
        {
            try
            {
                base.SendEmail(messageBody);
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
