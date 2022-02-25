using UnityEngine;

public class Enemy : IEnemy
{
    private string _name;
    private int _startEnemyPower = 10;  

    private int _moneyPlayer;
    private int _healthPlayer;
    private int _powerPlayer;
    private int _crimePlayer;

    public Enemy(string name, int startEnemyPower)
    {
        _name = name;
        _startEnemyPower = startEnemyPower;
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
        }

        Debug.Log($"Update {_name}, change {dataType}");
    }

    public int Power
    {
        get
        {
            var power = _startEnemyPower + _moneyPlayer + _healthPlayer - _powerPlayer - _crimePlayer;
            if (power < 0)
                power = 0;
            return power;
        }
    }
}
