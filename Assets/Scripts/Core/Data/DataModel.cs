using System;
using ByteCobra.Logging;
using Cysharp.Threading.Tasks;
using GameAssets.Meta.Items.Interfaces;
using UniRx;
using UnityEngine.AddressableAssets;

namespace GameAssets.Core.Data
{
    public sealed class DataModel : IModel
    {
        private UniTaskCompletionSource<ConfigData> _taskCompletionConfigLoad = null;

        
        async void IModel.Init()
        {
            try
            {
                var config = await GetConfigAsync();
                await (config as ISaveable<ConfigData>).AsyncLoad();
                _taskCompletionConfigLoad = null;
                Addressables.Release(config);
                Log.Info("DataModel init successful");
            }
            catch (Exception ex)
            {
                Log.Fatal($"DataModel not loaded!! {ex}");
                throw;
            }
        }

        async UniTask<bool> IModel.TryAddCoinsAsync(double amount)
        {
            var config = await GetConfigAsync();
            var result = config.TryAddCoins(amount);
            (config as ISaveable<ConfigData>).Save();
            
            Addressables.Release(config);
            Log.Debug($"TryAddCoins successful: {result}", state: result);
            return result;
        }

        async UniTask<bool> IModel.TrySpendCoinsAsync(double amount)
        {
            var config = await GetConfigAsync();
            var result = config.TrySpendCoins(amount);
            (config as ISaveable<ConfigData>).Save();
            
            Addressables.Release(config);
            Log.Debug($"TrySpendCoins successful: {result}", state: result);
            return result;
        }

        async UniTask<bool> IModel.TryAddLevelAsync()
        {
            var config = await GetConfigAsync();
            var result = config.TryAddLevel();
            (config as ISaveable<ConfigData>).Save();
            
            Addressables.Release(config);
            Log.Debug($"TryAddLevel successful: {result}", state: result);
            return result;
        }

        private async UniTask<ConfigData> GetConfigAsync()
        {
            if (_taskCompletionConfigLoad == null)
            {
                _taskCompletionConfigLoad = new();
                var config = await Addressables.LoadAssetAsync<ConfigData>("Data").Task;
                
                if (config == null)
                    throw new NullReferenceException("Config is null");
                
                _taskCompletionConfigLoad.TrySetResult(config);
            }
            
            return await _taskCompletionConfigLoad.Task;
        }
    }
}
