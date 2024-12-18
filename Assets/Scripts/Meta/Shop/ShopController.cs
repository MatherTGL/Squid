using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameAssets.Meta.Shop;

public static class ShopController
{
    private static IShopModel _IshopModel = new ShopModel();


    public static async Task<Dictionary<string, Item>> InitAndGetItemsAsync()
        => await _IshopModel.InitAndGetItemsAsync();

    public static UniTask<bool> TryBuyAsync(string indexItem)
        => _IshopModel.TryBuyAsync(indexItem);
    
    public static bool TrySelect(string indexItem)
        => _IshopModel.TrySelect(indexItem);
}
