using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace GameAssets.Meta.Shop
{
    public interface IShopModel
    {
        UniTask<Dictionary<string, Item>> InitAndGetItemsAsync();

        UniTask<bool> TryBuyAsync(string indexItem);

        bool TrySelect(string indexItem);
    }
}
