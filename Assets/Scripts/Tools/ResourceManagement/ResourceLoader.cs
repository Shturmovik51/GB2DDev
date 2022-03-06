using System.Diagnostics;
using System.Timers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ResourceLoader
{
    public static AsyncOperationHandle LoadPrefab(AssetReference assetReference)
    {
        var timer = new Stopwatch();
        timer.Start();
        var handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
        handle.WaitForCompletion();
        timer.Stop();
        UnityEngine.Debug.Log($"Prefab load {timer.Elapsed.Milliseconds}");
        return handle;
    }

    public static T LoadObject<T>(ResourcePath path) where T:Object
    {
        return Resources.Load<T>(path.PathResource);
    }

    public static AsyncOperationHandle<GameObject> LoadAndInstantiatePrefab(AssetReference assetReference, Transform uiRoot)
    {
        var timer = new Stopwatch();
        timer.Start();
        var handle = Addressables.InstantiateAsync(assetReference, uiRoot);
        handle.WaitForCompletion();
        timer.Stop();
        UnityEngine.Debug.Log($"GO load and instantiate {timer.Elapsed.Milliseconds}");
        return handle; 
    }

    public static T LoadDataSource<T>(AssetReference assetReference)
    {
        var timer = new Stopwatch();
        timer.Start();
        var handle = assetReference.LoadAssetAsync<T>();
        handle.WaitForCompletion();
        timer.Stop();
        UnityEngine.Debug.Log($"AssetReference load {timer.Elapsed.Milliseconds}");
        var result = handle.Result;
        Addressables.Release(handle);
        return result;
    }
}
