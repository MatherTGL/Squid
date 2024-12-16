using ByteCobra.Logging.Filters;
using ByteCobra.Logging.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ByteCobra.Logging
{
    [CreateAssetMenu(fileName = "Log Settings", menuName = "Byte Cobra/Logging/Create Settings", order = 1)]
    [Serializable]
    public class LogConfiguration : ScriptableObject
    {
        public LogLevel MinimumLogLevel = LogSettings.MinimumLogLevel;

        [SerializeField]
        public FilterData DirectoryFilter;

        [SerializeField]
        public FilterData NamespaceFilter;

        [SerializeField]
        public bool CaptureStackTrace = LogSettings.CaptureStackTrace;

        public string DebugTag = LogSettings.TagSettings.DebugTag;
        public string InfoTag = LogSettings.TagSettings.InfoTag;
        public string WarningTag = LogSettings.TagSettings.WarningTag;
        public string ErrorTag = LogSettings.TagSettings.ErrorTag;
        public string AssertionTag = LogSettings.TagSettings.AssertTag;
        public string FatalTag = LogSettings.TagSettings.FatalTag;

        private static Dictionary<Colors, string> ColorLookup = new Dictionary<Colors, string>()
        {
            {Colors.Black, "black" },
            {Colors.Blue, "blue" },
            {Colors.Brown, "brown" },
            {Colors.Cyan, "cyan" },
            {Colors.Gray, "gray" },
            {Colors.Green, "green" },
            {Colors.Orange, "orange" },
            {Colors.Pink, "pink" },
            {Colors.Purple, "purple" },
            {Colors.Red, "red" },
            {Colors.White, "white" },
            {Colors.Yellow, "yellow" },
        };

        public string DateFormat = LogSettings.TimeFormat;
        public bool SaveLogs = LogSettings.FileSettings.SaveLogs;
        public bool SaveStates = LogSettings.FileSettings.SaveStates;

        public string LogsDirectory = LogSettings.FileSettings.LogFilesDirectory.Name;
        public string StatesDirectory = LogSettings.FileSettings.StatesDirectory.Name;

        public void Initialize()
        {
            LogSettings.CaptureStackTrace = CaptureStackTrace;
            LogSettings.MinimumLogLevel = MinimumLogLevel;
            LogSettings.TagSettings.DebugTag = DebugTag;
            LogSettings.TagSettings.InfoTag = InfoTag;
            LogSettings.TagSettings.WarningTag = WarningTag;
            LogSettings.TagSettings.ErrorTag = ErrorTag;
            LogSettings.TagSettings.FatalTag = FatalTag;
            LogSettings.TagSettings.AssertTag = AssertionTag;

            LogSettings.TimeFormat = DateFormat;

            LogSettings.FileSettings.LogFilesDirectory = new DirectoryInfo(LogsDirectory);
            LogSettings.FileSettings.StatesDirectory = new DirectoryInfo(StatesDirectory);

            LogSettings.FileSettings.SaveLogs = SaveLogs;
            LogSettings.FileSettings.SaveStates = SaveStates;

            LogSettings.Filters.Clear();

            if (DirectoryFilter != null)
            {
                DirectoryFilter directoryFilter = new DirectoryFilter();
                foreach (var filter in DirectoryFilter.Filters)
                {
                    if (string.IsNullOrWhiteSpace(filter.Value))
                        continue;

                    directoryFilter.DirectoryLevels.Add(filter.Value, filter.Level);
                }

                LogSettings.Filters.Add(directoryFilter);
            }

            if (NamespaceFilter != null)
            {
                NamespaceFilter namespaceFilter = new NamespaceFilter();
                foreach (var filter in NamespaceFilter.Filters)
                {
                    if (string.IsNullOrWhiteSpace(filter.Value))
                        continue;

                    namespaceFilter.NamespaceLevels.Add(filter.Value, filter.Level);
                }

                LogSettings.Filters.Add(namespaceFilter);
            }
        }
    }
}