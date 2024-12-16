using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Doozy.Engine.SceneManagement
{
    public class AsyncOperationAdapter
    {
        public AsyncOperation AsyncOperation;
        private AsyncOperationHandle<SceneInstance> handle;
        public bool UseHandle { get; private set; }
        public float progress => UseHandle ? handle.IsDone ? allowSceneActivationInternal ? 1 : 0.9f : 0 : AsyncOperation.progress;
        public bool isDone => UseHandle ? handle.IsDone && allowSceneActivationInternal : AsyncOperation.isDone;
        private bool allowSceneActivationInternal;
        public bool allowSceneActivation
        {
            get
            {
                if (UseHandle)
                {
                    return allowSceneActivationInternal;
                }
                else
                {
                    return AsyncOperation.allowSceneActivation;
                }
            }
            set
            {
                if (UseHandle)
                {
                    if (value)
                    {
                        handle.Result.ActivateAsync();
                        allowSceneActivationInternal = true;
                    }
                }
                else
                {
                    AsyncOperation.allowSceneActivation = value;
                }
            }
        }

        public AsyncOperationAdapter(AsyncOperation asyncOperation)
        {
            AsyncOperation = asyncOperation;
        }

        public AsyncOperationAdapter(string sceneName, LoadSceneMode mode)
        {
            UseHandle = true;
            handle = Addressables.LoadSceneAsync(sceneName, mode, false);
            handle.Completed += OnComplete;
        }

        private void OnComplete(AsyncOperationHandle<SceneInstance> scene)
        {
            SceneInstance sceneInstance = scene.Result;
            AddressablesScenesManager.AddLoadedScene(sceneInstance.Scene.name, sceneInstance);
        }
    }
}