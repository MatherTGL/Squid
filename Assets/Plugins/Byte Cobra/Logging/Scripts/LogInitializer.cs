using ByteCobra.Logging.Settings;
using UnityEngine;
using UnityEngine.Events;

namespace ByteCobra.Logging
{
    public class LogInitializer : MonoBehaviour
    {
        public LogConfiguration logSettings;

        #region Action Overrides

        public UnityEvent<BaseLog> OnLog;

        #endregion Action Overrides

        private void Awake()
        {
#if UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL
            LogSettings.FileSettings.SaveLogs = false;
            LogSettings.FileSettings.SaveStates = false;
#endif

#if !UNITY_EDITOR
            LogSettings.IsEditor = false;
#else
            LogSettings.IsEditor = true;
#endif
            logSettings.Initialize();

            if (OnLog.GetPersistentEventCount() > 0)
                Log.OnLog += (msg) => OnLog.Invoke(msg);
        }
    }
}