public class FightController : BaseController
{
    private readonly FightWindowView _view;
    private readonly ProfilePlayer _profilePlayer;

    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private Crime _crime;

    private const int _maxCrimeRate = 5;
    private const int _startEnemyPower = 10;
    private const int _crimeRateValueToLoseOpportunityToMissTheFight = 2;  // зато понятно =)

    private UiListener _uiListener;

    public FightController(FightWindowView view, ProfilePlayer profilePlayer)
    {
        _view = view;
        _profilePlayer = profilePlayer;
        CreateParticipants();
    }

    private void InitView()
    {
        _view.Init(ChangeData, Fight);
        _uiListener = new UiListener(_view._countPowerText, _view._countMoneyText, _view._countHealthText, _view._countCrimeRateText);
        _money.Attach(_uiListener);
        _power.Attach(_uiListener);
        _health.Attach(_uiListener);
        _crime.Attach(_uiListener);
    }

    private void ChangeData(DataType type, int count)
    {
        switch (type)
        {
            case DataType.Money:
                _money.CountMoney += count;
                break;
            case DataType.Health:
                _health.CountHealth += count;
                break;
            case DataType.Power:
                _power.CountPower += count;
                break;
            case DataType.Crime:
                _crime.CountCrime += count;
                break;
            case DataType.Attack:

                break;
            default:
                break;
        }

        _view._countPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";
    }

    private void CreateParticipants()
    {
        _enemy = new Enemy("Flappy", _startEnemyPower);
        _countPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _crime = new Crime(nameof(Crime));
        _crime.Attach(_enemy);
    }

    private void Fight(AttackType type)
    {
        switch (type)
        {
            case AttackType.None:
                _fightResult.text = "Choose your weapon";
                break;
            case AttackType.Knife:
                _fightResult.text = _allCountPowerPlayer >= _enemy.Power ? $"You defeated the enemy with {type}" : $"You were stabbed";
                break;
            case AttackType.Gun:
                _fightResult.text = _allCountPowerPlayer >= _enemy.Power ? $"You defeated the enemy with {type}" : $"You were shot";
                break;
        }
    }
}
