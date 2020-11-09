using System;

#region File Information

/* ----------------------------------------------------------------------
* File Name: NLogHelper
* Create Author: ZDK
* Create DateTime: 4/15/2020 6:08:18 PM
* Description:
*----------------------------------------------------------------------*/

#endregion File Information

namespace HTOL.Common
{
    public class NLogHelper
    {
        public static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void DebugLog(string msg)
        {
            logger.Debug(msg);
        }

        public static void ErrorLog(Exception e, string msg)
        {
            logger.Error(e, msg);
        }
    }
}