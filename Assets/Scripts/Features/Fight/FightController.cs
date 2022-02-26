using Profile;

public class FightController : BaseController
{
    private readonly FightWindowView _view;
    private readonly ProfilePlayer _profilePlayer;

    private Enemy _enemy;    

    private Money _money;
    private Health _health;
    private Power _power;
    private Crime _crime;
    private Attaсk _attaсk;

    private const int _maxCrimeRate = 5;
    private const int _crimeRateValueToLoseOpportunityToMissTheFight = 2;  // зато понятно =)

    private UiListener _uiListener;

    public FightController(FightWindowView view, ProfilePlayer profilePlayer)
    {
        _view = view;
        _profilePlayer = profilePlayer;
        AddGameObjects(view.gameObject);
        CreateParticipants();
        InitView();
    }

    private void InitView()
    {
        _view.Init(ChangeData, Fight, Skip, CloseWindow);

        _uiListener = new UiListener(_view.CountPowerText, _view.CountMoneyText, _view.CountHealthText, _view.CountCrimeRateText, 
                                        _view.AttackTypePlayerText, _view.AttackTypeEnemyText);
        _money.Attach(_uiListener);
        _power.Attach(_uiListener);
        _health.Attach(_uiListener);
        _crime.Attach(_uiListener);
        _attaсk.Attach(_uiListener);
    }

    private void ChangeData(DataType Datatype, int value)
    {
        switch (Datatype)
        {
            case DataType.Money:
                _money.CountMoney += value;
                break;
            case DataType.Health:
                _health.CountHealth += value;
                break;
            case DataType.Power:
                _power.CountPower += value;
                break;
            case DataType.Crime:
                _crime.CountCrime += value;
                ChangeSkipButtonStatus(_crime.CountCrime);
                break;
            case DataType.Attack:
                _attaсk.PlayerAttackType = value;
                break;
            default:
                break;
        }

        _view.FightResult.text = "";
    }

    private void CreateParticipants()
    {
        _enemy = new Enemy("Flappy", _view.CountPowerEnemyText);
        _view.CountPowerEnemyText.text = $"Enemy Power: {_enemy.EnemyPover.Value}";

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _crime = new Crime(nameof(Crime));
        _crime.Attach(_enemy);

        _attaсk = new Attaсk(nameof(Attaсk));
        _attaсk.Attach(_enemy);

        AddController(_enemy);
    }

    private void Fight()
    {
        switch (_attaсk.PlayerAttackType)
        {
            case (int)AttackType.None:
                _view.FightResult.text = "Choose your weapon";
                break;
            case (int)AttackType.Knife:
                _view.FightResult.text = _power.CountPower >= _enemy.EnemyPover.Value ? $"You defeated the enemy with {AttackType.Knife}" : $"You were stabbed";
                break;
            case (int)AttackType.Gun:
                _view.FightResult.text = _power.CountPower >= _enemy.EnemyPover.Value ? $"You defeated the enemy with {AttackType.Gun}" : $"You were shot";
                break;
        }
    }
    private void Skip()
    {
        _view.FightResult.text = "You Succesfully avoid fight";
    }

    private void CloseWindow()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
    }

    private void ChangeSkipButtonStatus(int value)
    {
        _view.SkipButton.interactable = value > _crimeRateValueToLoseOpportunityToMissTheFight ? false : true;
    }
}
