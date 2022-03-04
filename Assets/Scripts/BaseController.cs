using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class BaseController : IDisposable
{
    private List<BaseController> _baseControllers = new List<BaseController>();
    private List<GameObject> _gameObjects = new List<GameObject>();
    private List<AsyncOperationHandle<GameObject>> _handls = new List<AsyncOperationHandle<GameObject>>();
    private bool _isDisposed;
    
    public void Dispose()
    {
        OnDispose();

        if (_isDisposed) 
            return;
        
        _isDisposed = true;
            
        foreach (var baseController in _baseControllers)
            baseController?.Dispose();
                
        _baseControllers.Clear();
        
        foreach (var cachedGameObject in _gameObjects)
            Object.Destroy(cachedGameObject);
                
        _gameObjects.Clear();

        foreach (var handle in _handls)
        {
            Addressables.Release(handle);
        }
    }

    protected void AddController(BaseController baseController)
    {
        _baseControllers.Add(baseController);
    }

    protected void AddGameObjects(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    protected void AddAsyncHamdle(AsyncOperationHandle<GameObject> handle)
    {
        _handls.Add(handle);
    }

    protected virtual void OnDispose()
    {
    }
}