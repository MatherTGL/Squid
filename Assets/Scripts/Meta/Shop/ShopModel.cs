using System.Collections.Generic;
using System.Linq;
using ByteCobra.Logging;
using Cysharp.Threading.Tasks;
using GameAssets.Meta.Items.Interfaces;
using UnityEngine.AddressableAssets;

namespace GameAssets.Meta.Shop
{
    public sealed class ShopModel : IShopModel
    {
        private ShopConfig _shopConfig;


        async UniTask<bool> IShopModel.TryBuyAsync(string indexItem)
        {
            var item = _shopConfig.itemsForBuy.Dictionary[indexItem];
            var result = await item.TryBuy();
            
            bool anyItem = _shopConfig.itemsForBuy.Dictionary.Any(item => item.Value.isSelected);

            if (anyItem == false)
                item.TrySelect(true);
            
            Log.Debug($"IsAnySelected: {anyItem} / selected by default: {item.isSelected}");
            Log.Debug($"Buying item: {result}");
            
            return result;
        }

        bool IShopModel.TrySelect(string indexTargetItem)
        {
            foreach (var item in _shopConfig.itemsForBuy.Dictionary.Keys)
            {
                if (_shopConfig.itemsForBuy.Dictionary[item].isSelected)
                {
                    _shopConfig.itemsForBuy.Dictionary[item].TrySelect(false);
                    Log.Debug($"Selected item: {item} & state: {_shopConfig.itemsForBuy.Dictionary[item].isSelected}");
                    _shopConfig.itemsForBuy.Dictionary[indexTargetItem].TrySelect(true); 
                    Log.Debug($"New selected item: {_shopConfig.itemsForBuy.Dictionary[indexTargetItem]} / state: {_shopConfig.itemsForBuy.Dictionary[indexTargetItem].isSelected}");
                    return true;
                }
            }
            
            return false;
        }

        async UniTask<Dictionary<string, Item>> IShopModel.InitAndGetItemsAsync()
        {
            await LoadConfigAsync();
            return await GenerateItemsAsync();
        }

        private async UniTask LoadConfigAsync()
            => _shopConfig = await Addressables.LoadAssetAsync<ShopConfig>("ShopCookies").Task;
        
        private async UniTask<Dictionary<string, Item>> GenerateItemsAsync()
        {
            Dictionary<string, Item> items = new();

            foreach (var item in _shopConfig.itemsForBuy.Dictionary.Keys)
            {
                items.Add(item, _shopConfig.itemsForBuy.Dictionary[item]);
                await ((ISaveable<Item>)items[item]).AsyncLoad();
            }

            return GetSortItemsList(ref items);
        }

        private Dictionary<string, Item> GetSortItemsList(ref Dictionary<string, Item> items)
            => items = items.OrderByDescending(item => item.Value.buyCost).ToDictionary(item => item.Key, item => item.Value);
    }
}
