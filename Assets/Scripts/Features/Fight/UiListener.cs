using TMPro;

public class UiListener : IEnemy
{
    private readonly TMP_Text _countPower;
    private readonly TMP_Text _countMoney;
    private readonly TMP_Text _countHealth;
    private readonly TMP_Text _countCrimeRate;
    public UiListener(TMP_Text countPower, TMP_Text countMoney, TMP_Text countHealth, TMP_Text countCrimeRate)
    {
        _countPower = countPower;
        _countMoney = countMoney;
        _countHealth = countHealth;
        _countCrimeRate = countCrimeRate;
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
                ChangeSkipButtonStatus(countChangeData);
                break;
            case DataType.Attack:
                _attackTypePlayerText.text = $"Attack Type: {_playerAttackType}";
                _attackTypeEnemyText.text = $"Enemy Attack Type: {_playerAttackType}";
                break;
        }
    }
}

