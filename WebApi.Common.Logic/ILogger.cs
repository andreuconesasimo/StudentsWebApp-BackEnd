using System;

namespace WebApi.Common.Logic
{
    public interface ILogger
    {
        #region Properties
        TimeSpan ExecutionTime { get; set; }
        int counter { get; set; }
        #endregion

        #region Methods
        void Debug(string message);
        void Error(string message);
        void Exception(Exception exception, string message);
        void Exception(Exception exception);
        #endregion
    }
}
