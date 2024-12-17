using System;
using System.Threading.Tasks;
using GameAssets.Meta.Items.Interfaces;
using GameAssets.Meta.Items.ScriptableObjects;
using Newtonsoft.Json;
using SerializableDictionary.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameAssets.Meta.Shop
{
    [CreateAssetMenu(fileName = "ShopConf", menuName = "Configs/Shop", order = 1)]
    public sealed class ShopConfig : ItemInfo
    {
        [field: SerializeField, BoxGroup("Item Parameters")]
        public SerializableDictionary<string, Item> itemsForBuy { get; set; } = new();
    }

    [Serializable]
    public sealed class Item : ISaveable<Item>
    {
        [JsonIgnore, BoxGroup("Parameters")] public string itemName;

        [JsonIgnore, PreviewField, BoxGroup("Parameters")] public Sprite icon;

        [JsonIgnore, BoxGroup("Parameters")] public double buyCost;

        [SerializeField, BoxGroup("Parameters")] public bool isBuyed;

        [SerializeField, BoxGroup("Parameters")] public bool isSelected;

        [JsonIgnore] string ISaveable<Item>.SaveId => itemName;

        [field: NonSerialized] bool ISaveable<Item>.Loaded { get; set; }


        Task ISaveable<Item>.OnFirstTimeLoad()
        {
            isBuyed = false;
            isSelected = false;
            return Task.CompletedTask;
        }

        void ISaveable<Item>.OnLoad(Item loadedItem)
        {
            isBuyed = loadedItem.isBuyed;
            isSelected = loadedItem.isSelected;
        }

        public void Buy()
        {

        }

        public void Select()
        {

        }

        Task IPreloadable.PreloadAsync() => Task.CompletedTask;
    }
}
