using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameAssets.Meta.Shop
{
    public interface IShopModel
    {
        Task<Dictionary<string, Item>> InitAndGetItemsAsync();

        void Buy(string indexItem);
    }
}
