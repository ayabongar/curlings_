using System.Diagnostics;

namespace ImsService.BLL
{
    public static class EventLoggingManager
    {
        private const string Source = "IMS Email Reminder Service";
        private const string LogName = "IMS Email Reminder Service";

        public static void LogInformation(string message)
        {
           // LogNotifyServiceEvent(message, EventLogEntryType.Information);
        }

        public static void LogWarning(string message)
        {
           // LogNotifyServiceEvent(message, EventLogEntryType.Warning);
        }

        public static void LogError(string message)
        {
           // LogNotifyServiceEvent(message, EventLogEntryType.Error);
        }

        private static void LogNotifyServiceEvent(string message, EventLogEntryType type)
        {
            //if (!EventLog.SourceExists(Source))
            //{
            //    EventLog.CreateEventSource(Source, LogName);
            //}

            //var eLog = new EventLog { Source = Source };

            //eLog.WriteEntry(message, type);
        }


    }
}
