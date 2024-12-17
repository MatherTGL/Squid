using Cysharp.Threading.Tasks;

namespace GameAssets.Core.Data
{
    public interface IModel
    {
        void Init();

        UniTask<bool> TryAddCoinsAsync(double amount);
        
        UniTask<bool> TrySpendCoinsAsync(double amount);

        UniTask<bool> TryAddLevelAsync();
    }
}