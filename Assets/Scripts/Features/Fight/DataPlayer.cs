using System.Collections.Generic;

public class DataPlayer
{
    private string _titleData;

    private int _countMoney;
    private int _countHealth;
    private int _countPower;
    private int _countCrime;
    private int _playerAttackType;

    private List<IEnemy> _enemies = new List<IEnemy>();

    public DataPlayer(string titleData)
    {
        _titleData = titleData;
    }

    public string TitleData => _titleData;

    public int CountMoney 
    { 
        get => _countMoney;
        set
        {
            if (_countMoney != value)
            {
                _countMoney = value;
                Notifier(DataType.Money);
            }
        }
    }
    public int CountHealth
    {
        get => _countHealth;
        set
        {
            if (_countHealth != value)
            {
                _countHealth = value;
                Notifier(DataType.Health);
            }
        }
    }
    public int CountPower
    {
        get => _countPower;
        set
        {
            if (_countPower != value)
            {
                _countPower = value;
                Notifier(DataType.Power);
            }
        }
    }
    public int CountCrime
    {
        get => _countCrime;
        set
        {
            if (_countCrime != value)
            {               
                _countCrime = value;
                Notifier(DataType.Crime);
            }
        }
    }
    public int PlayerAttackType
    {
        get => _playerAttackType;
        set
        {
            if(_playerAttackType != value)
            {
                _playerAttackType = value;

                Notifier(DataType.Attack);
            }
        }
    }

    public void Attach(IEnemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void Detach(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void Notifier(DataType dataType)
    {
        foreach(var enemy in _enemies)
            enemy.Update(this, dataType);
    }
}

public class Money : DataPlayer
{
    public Money(string titleData) : base(titleData)
    {
    }
}

public class Health : DataPlayer
{
    public Health(string titleData) : base(titleData)
    {
    }
}

public class Power : DataPlayer
{
    public Power(string titleData) : base(titleData)
    {
    }
}

public class Crime : DataPlayer
{
    public Crime(string titleData) : base(titleData)
    {
    }
}

public class Attaсk : DataPlayer
{
    public Attaсk(string titleData) : base(titleData)
    {
    }
}