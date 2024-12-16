using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ByteCobra.Logging.Examples
{
    public class ExampleClass : MonoBehaviour
    {
        private CancellationTokenSource cancellationToken = new CancellationTokenSource();

        private void OnDisable()
        {
            cancellationToken.Cancel();
        }

        private async void Start()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                Log.Debug("This is a debug log");
                Log.Info("This is some information.");
                Log.Assert(false, "This is an assert.", throwException: false);
                Log.Warning("This is a warning.");
                Log.Error("This is an error.", throwException: false);
                //If throwException is true, then it'll throw an exception after logging the message.

                try
                {
                    Log.Fatal("This is a fatal log", quit: false);
                    //If quit is true, then it will log the message and quit the application.
                    //It will always throw an exception after logging the message.
                }
                catch { }
            }
        }
    }
}