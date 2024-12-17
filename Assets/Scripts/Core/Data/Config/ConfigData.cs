using System;
using System.Threading.Tasks;
using GameAssets.Meta.Items.Interfaces;
using GameAssets.Meta.Items.ScriptableObjects;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameAssets.Core.Data
{
    [CreateAssetMenu(fileName = "ConfigDataPlayer", menuName = "Configs/Data/Player", order = 1)]
    public sealed class ConfigData : ItemInfo, ISaveable<ConfigData>
    {
        [field: SerializeField, BoxGroup("Parameters"), ReadOnly]
        public ushort currentLevel { get; private set; } = 1;

        [field: SerializeField, BoxGroup("Parameters"), MinValue("@currentLevel")]
        public ushort maxLevel { get; private set; } = 20;

        [SerializeField, BoxGroup("Parameters")]
        private double _baseCoins;

        [field: SerializeField, BoxGroup("Parameters")]
        public double currentCoins { get; private set; }

        [JsonIgnore] string ISaveable<ConfigData>.SaveId => GUID;

        [field: NonSerialized] bool ISaveable<ConfigData>.Loaded { get; set; }


        void ISaveable<ConfigData>.OnLoad(ConfigData loadedItem)
        {
            currentCoins = loadedItem.currentCoins;
            currentLevel = loadedItem.currentLevel;
        }

        Task ISaveable<ConfigData>.OnFirstTimeLoad()
        {
            currentCoins = _baseCoins;
            currentLevel = 1;
            return Task.CompletedTask;
        }

        public Task PreloadAsync() => Task.CompletedTask;

        public bool TryAddCoins(double amount)
        {
            return false;
        }

        public bool TrySpendCoins(double amount)
        {
            return false;
        }

        public bool TryAddLevel()
        {
            return false;
        }
    }
}
