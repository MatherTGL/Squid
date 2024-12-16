using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Doozy.Engine.SceneManagement
{
    public static class AddressablesScenesManager
    {
        public static Dictionary<string, SceneInstance > Scenes { get; } = new Dictionary<string, SceneInstance>();

        public static void AddLoadedScene(string sceneName, SceneInstance sceneInstance)
        {
            Scenes.Add(sceneName, sceneInstance);
        }
        
        public static AsyncOperationHandle<SceneInstance> UnloadSceneAsync(string sceneName)
        {
            if(Scenes.Remove(sceneName, out SceneInstance sceneInstance))
            {
                return Addressables.UnloadSceneAsync(sceneInstance);
            }
            throw new System.Exception($"Scene {sceneName} is not loaded");
        }
    }
}