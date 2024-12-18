using ByteCobra.Logging;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GameAssets.Meta.Shop
{
    public sealed class ShopItemView : MonoBehaviour, IShopItem
    {
        [SerializeField, Required, BoxGroup("Parameters")]
        private Button _interact;

        private string _indexItem;


        void IShopItem.Init(string indexItem)
        {
            //TODO обновлять UI 
            _indexItem = indexItem;
        }

        [Button("Buy", ButtonSizes.Medium)]
        public void Buy()
        {
            var result = ShopController.TryBuyAsync(_indexItem);
            Log.Debug($"Buy: {_indexItem} & result: {result}");
        }

        [Button("Select", ButtonSizes.Medium)]
        public void Select()
        {
            var result = ShopController.TrySelect(_indexItem);
            Log.Debug($"Select: {_indexItem} & result: {result}");
        }
    }
}
