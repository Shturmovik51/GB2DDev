using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ResourceLoader
{
    public static GameObject LoadPrefab(ResourcePath path)
    {
        return Resources.Load<GameObject>(path.PathResource);
    }

    public static T LoadObject<T>(ResourcePath path) where T:Object
    {
        return Resources.Load<T>(path.PathResource);
    }

    public static AsyncOperationHandle<GameObject> LoadAndInstantiatePrefab(AssetReference assetReference, Transform uiRoot)
    {
        return Addressables.InstantiateAsync(assetReference, uiRoot); 
    }

    public static T LoadDataSource<T>(AssetReference assetReference)
    {
        return Addressables.LoadAssetAsync<T>(assetReference).Result;
    }

    //var pref = Addressables.LoadAssetAsync<GameObject>(_prefab);
} 
