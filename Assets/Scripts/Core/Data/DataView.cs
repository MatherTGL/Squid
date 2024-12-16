using Boot;
using ByteCobra.Logging;
using UnityEngine;

namespace GameAssets.Core.Data
{
    public sealed class DataView : MonoBehaviour, IBoot
    {
        void IBoot.InitAwake()
        {
            Log.Debug("Init awake data view", state: true);
        }

        void IBoot.InitStart()
        {
            Log.Debug("Init start data view", state: true);
        }

        (Bootstrap.TypeLoadObject typeLoad, Bootstrap.TypeSingleOrLotsOf singleOrLotsOf) IBoot.GetTypeLoad()
            => (Bootstrap.TypeLoadObject.SuperImportant, Bootstrap.TypeSingleOrLotsOf.Single);
    }
}
