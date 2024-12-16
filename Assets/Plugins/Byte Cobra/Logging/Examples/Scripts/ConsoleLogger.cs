using UnityEngine;
using UnityEngine.UI;

namespace ByteCobra.Logging.Examples
{
    public class ConsoleLogger : MonoBehaviour
    {
        public Text text;

        private void Start()
        {
            Log.Assert(text != null, "Text was null");
            //Log.OnLog += OnLog; // You can also subscribe programatically if you prefer that
        }

        public void OnLog(BaseLog log)
        {
            text.text += log.FormattedMessage + "\n";
            Debug.Log(log.FormattedMessage);
        }
    }
}