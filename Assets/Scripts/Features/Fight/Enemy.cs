using Tools;
using UnityEngine;
using TMPro;

public class Enemy : BaseController, IEnemy
{
    private string _name;
    private int _startEnemyPower = Random.Range(0,10);  

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _crimePlayer;
    private int _attackType;

    private TMP_Text _enemyAttackpower;
    public SubscriptionProperty<int> EnemyPover;

    public Enemy(string name, TMP_Text enemyAttackpower)
    {
        _name = name;
        _enemyAttackpower = enemyAttackpower;
        EnemyPover = new SubscriptionProperty<int>();
        EnemyPover.Value = _startEnemyPower;
        EnemyPover.SubscribeOnChange(UpdatePowerText);
    }

    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Health:
                _healthPlayer = dataPlayer.CountHealth;
                break;

            case DataType.Money:
                _moneyPlayer = dataPlayer.CountMoney;
                break;

            case DataType.Power:
                _powerPlayer = dataPlayer.CountPower;
                break;

            case DataType.Crime:
                _crimePlayer = dataPlayer.CountCrime;
                break;

            case DataType.Attack:
                _attackType = dataPlayer.PlayerAttackType;
                break;
        }

        Debug.Log($"Update {_name}, change {dataType}");
        EnemyPover.Value = _startEnemyPower + _moneyPlayer + _healthPlayer - _powerPlayer - _crimePlayer;
    }

    private void UpdatePowerText(int value)
    {
        _enemyAttackpower.text = $"Enemy Power {value}";
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        EnemyPover.UnSubscriptionOnChange(UpdatePowerText);
    }
}
