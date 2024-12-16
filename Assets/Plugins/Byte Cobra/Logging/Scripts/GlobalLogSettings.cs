using ByteCobra.Logging.Settings;
using UnityEngine;

namespace ByteCobra.Logging
{
    public static class GlobalLogSettings
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            // Platform-specific settings
#if UNITY_ANDROID || UNITY_IOS || UNITY_WEBGL
            LogSettings.FileSettings.SaveLogs = false;
            LogSettings.FileSettings.SaveStates = false;
            LogSettings.CaptureStackTrace = false;
#endif

#if !UNITY_EDITOR
            LogSettings.IsEditor = false;
#else
            LogSettings.IsEditor = true;
#endif
        }
    }
}