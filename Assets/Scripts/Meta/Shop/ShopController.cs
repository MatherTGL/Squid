using System.Collections.Generic;
using System.Threading.Tasks;
using GameAssets.Meta.Shop;

public static class ShopController
{
    private static IShopModel _IshopModel = new ShopModel();


    public static async Task<Dictionary<string, Item>> InitAndGetItemsAsync()
        => await _IshopModel.InitAndGetItemsAsync();

    public static bool TryBuy(string indexItem)
        => _IshopModel.TryBuy(indexItem);
    
    public static bool TrySelect(string indexItem)
        => _IshopModel.TrySelect(indexItem);
}
