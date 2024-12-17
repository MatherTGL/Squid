using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameAssets.Meta.Items.Interfaces;
using UnityEngine.AddressableAssets;

namespace GameAssets.Meta.Shop
{
    public sealed class ShopModel : IShopModel
    {
        private ShopConfig _shopConfig;


        bool IShopModel.TryBuy(string indexItem)
        {
            var item = _shopConfig.itemsForBuy.Dictionary[indexItem];
            return false;
            // if (item.currentLevel < item.maxLevel && DataContoller.Imodel.IsSpendCoins(item.currentCost, true))
            // {
            //     var oldProfitPerHour = item.currentProfitPerHour;
            //     item.Upgrade();
            //     ((ISaveable<Item>)item).Save();
            //     var differenceProfit = item.currentProfitPerHour - oldProfitPerHour;

            //     DataContoller.Imodel.UpgradePassiveIncome(differenceProfit);
            //     Debug.Log($"{item.itemName} - current level: {item.currentLevel}");
            // }
        }

        bool IShopModel.TrySelect(string indexItem)
        {
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
