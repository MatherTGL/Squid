using Boot;
using Sirenix.OdinInspector;
using UnityEngine;
using static Boot.Bootstrap;

namespace GameAssets.Meta.Shop
{
    public sealed class ShopView : MonoBehaviour, IBoot
    {
        [SerializeField, Required, BoxGroup("Parameters"), AssetsOnly]
        private ShopItemView _prefabItem;

        [SerializeField, Required, BoxGroup("Parameters"), SceneObjectsOnly]
        private GameObject _root;


        void IBoot.InitAwake() { }

        void IBoot.InitStart() => Init();

        (TypeLoadObject typeLoad, TypeSingleOrLotsOf singleOrLotsOf) IBoot.GetTypeLoad()
            => (TypeLoadObject.UI, TypeSingleOrLotsOf.Single);

        private async void Init()
        {
            var items = await ShopController.InitAndGetItemsAsync();
            Debug.Log($"{items.Count} items");
            foreach (var item in items.Keys)
            {
                var createdItem = Instantiate(_prefabItem, transform.position, Quaternion.identity, _root.transform);
                createdItem.GetComponent<IShopItem>().Init(item);
            }
        }
    }
}
