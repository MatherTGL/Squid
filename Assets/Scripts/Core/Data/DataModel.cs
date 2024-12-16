using UnityEngine;

namespace GameAssets.Core.Data
{
    public sealed class DataModel : IModel
    {
        public double coins { get; private set; }

        public ushort level { get; private set; }


        void IModel.Init()
        {
            Debug.Log("Data model inited!");
        }
    }
}
