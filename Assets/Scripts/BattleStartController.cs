using Profile;

public class BattleStartController : BaseController
{
    private readonly BattleStart _view;
    private readonly ProfilePlayer _model;

    public BattleStartController(BattleStart view, ProfilePlayer model)
    {
        _view = view;
        _model = model;
        _view.Init(StartBattle);
    }

    private void StartBattle()
    {
        _model.CurrentState.Value = GameState.Fight;
    }
}

