using System;

namespace DSV.Webservice.Central.DMSRetrieval.log4Net
{
    /// <summary>
    /// Logger
    /// </summary>
    public static class Logger
    {

        private static log4net.ILog Log { get; set; }

        static Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }



        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(object msg)
        {
            Log.Error(msg);
        }



        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }



        /// <summary>
        /// Error
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }



        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(object msg)
        {
            Log.Info(msg);
        }
    }
}