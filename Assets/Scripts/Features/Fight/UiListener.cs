using System;
using TMPro;

public class UiListener : IEnemy
{
    private readonly TMP_Text _countPower;
    private readonly TMP_Text _countMoney;
    private readonly TMP_Text _countHealth;
    private readonly TMP_Text _countCrimeRate;
    private readonly TMP_Text _playerAttackType;
    private readonly TMP_Text _enemyAttackType;
    public UiListener(TMP_Text countPower, TMP_Text countMoney, TMP_Text countHealth, TMP_Text countCrimeRate, 
                        TMP_Text playerAttackType, TMP_Text enemyAttackType)
    {
        _countPower = countPower;
        _countMoney = countMoney;
        _countHealth = countHealth;
        _countCrimeRate = countCrimeRate;
        _playerAttackType = playerAttackType;
        _enemyAttackType = enemyAttackType;
    }
    public void Update(DataPlayer dataPlayer, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _countMoney.text = $"Player Money: {dataPlayer.CountMoney}";
                break;

            case DataType.Health:
                _countHealth.text = $"Player Health: {dataPlayer.CountHealth}";
                break;

            case DataType.Power:
                _countPower.text = $"Player Power: {dataPlayer.CountPower}";
                break;

            case DataType.Crime:
                _countCrimeRate.text = $"Player CrimeRate: {dataPlayer.CountCrime}";
                break;

            case DataType.Attack:
                _playerAttackType.text = $"Attack Type: {Enum.GetName(typeof(AttackType), dataPlayer.PlayerAttackType)}";
                _enemyAttackType.text = $"Enemy Attack Type: {Enum.GetName(typeof(AttackType), dataPlayer.PlayerAttackType)}";                
                break;
        }
    }
}

