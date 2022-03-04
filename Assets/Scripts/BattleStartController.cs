using Profile;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BattleStartController : BaseController
{
    private readonly BattleStart _view;
    private readonly ProfilePlayer _model;

    public BattleStartController(AsyncOperationHandle<GameObject> viewHandle, ProfilePlayer model)
    {
        _view = viewHandle.Result.GetComponent<BattleStart>();
        _model = model;
        _view.Init(StartBattle);
        AddAsyncHamdle(viewHandle);
    }

    private void StartBattle()
    {
        _model.CurrentState.Value = GameState.Fight;
    }
}

