using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UI;

public partial class FightWindowView : MonoBehaviour, IView
{
    [SerializeField] public TMP_Text _countMoneyText;
    [SerializeField] public TMP_Text _countHealthText;
    [SerializeField] public TMP_Text _countPowerText;
    [SerializeField] public TMP_Text _countCrimeRateText;
    [SerializeField] public TMP_Text _countPowerEnemyText;
    [SerializeField] private TMP_Text _fightResult;
    [SerializeField] private TMP_Text _attackTypePlayerText;
    [SerializeField] private TMP_Text _attackTypeEnemyText; 

    [SerializeField] private Button _addMoneyButton;
    [SerializeField] private Button _minusMoneyButton;
    [SerializeField] private Button _addHealthButton;
    [SerializeField] private Button _minusHealthButton;
    [SerializeField] private Button _addPowerButton;
    [SerializeField] private Button _minusPowerButton;
    [SerializeField] private Button _addCrimeRate;
    [SerializeField] private Button _minusCrimeRate;
    [SerializeField] private Button _setKnife;
    [SerializeField] private Button _setGun;

    [SerializeField] private Button _fightButton;
    [SerializeField] private Button _skipButton;


        
    private AttackType _playerAttackType = AttackType.None;

    public void Init(UnityAction<DataType, int> changeAction, UnityAction<AttackType> fight)
    {
        SubscribeButtons(changeAction, fight);
    }

    private void SubscribeButtons(UnityAction<DataType, int> changeAction, UnityAction<AttackType> fight)
    {
        _addMoneyButton.onClick.AddListener(() => changeAction(DataType.Money, 1));
        _minusMoneyButton.onClick.AddListener(() => changeAction(DataType.Money, -1));

        _addHealthButton.onClick.AddListener(() => changeAction(DataType.Health, 1));
        _minusHealthButton.onClick.AddListener(() => changeAction(DataType.Health, -1));

        _addPowerButton.onClick.AddListener(() => changeAction(DataType.Power, 1));
        _minusPowerButton.onClick.AddListener(() => changeAction(DataType.Health, -1));

        _addCrimeRate.onClick.AddListener(() => changeAction(DataType.Crime, 1));
        _minusCrimeRate.onClick.AddListener(() => changeAction(DataType.Health, -1));

        _setKnife.onClick.AddListener(() => ChangeAttackType(AttackType.Knife));
        _setGun.onClick.AddListener(() => ChangeAttackType(AttackType.Gun));

        _fightButton.onClick.AddListener(() => fight(_playerAttackType));
        _skipButton.onClick.AddListener(Skip);
    }
    private void UnsubscribeButtons()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addCrimeRate.onClick.RemoveAllListeners();
        _minusCrimeRate.onClick.RemoveAllListeners();

        _setKnife.onClick.RemoveAllListeners();
        _setGun.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();
    } 

    private void OnDestroy()
    {
        UnsubscribeButtons();
    }  

    private void Skip()
    {
        _fightResult.text = "You Succesfully avoid fight";
    }

    //private void ChangePower(bool isAddCount)
    //{
    //    if (isAddCount)
    //        _allCountPowerPlayer++;
    //    else
    //        NotNegativeValueControllerOnDecrease(ref _allCountPowerPlayer);

    //    ChangeDataWindow(DataType.Power, _allCountPowerPlayer);
    //}

    //private void ChangeHealth(bool isAddCount)
    //{
    //    if (isAddCount)
    //        _allCountHealthPlayer++;
    //    else
    //        NotNegativeValueControllerOnDecrease(ref _allCountHealthPlayer);

    //    ChangeDataWindow(DataType.Health, _allCountHealthPlayer);
    //}

    //private void ChangeMoney(bool isAddCount)
    //{
    //    if (isAddCount)
    //        _allCountMoneyPlayer++;
    //    else
    //        NotNegativeValueControllerOnDecrease(ref _allCountMoneyPlayer);

    //    ChangeDataWindow(DataType.Money, _allCountMoneyPlayer);
    //}
      
    //private void ChangeCrimeRate(bool isAddCount)
    //{
    //    if (isAddCount)
    //    {
    //        _allCrimeRate++;

    //        if (_allCrimeRate > _maxCrimeRate)
    //            _allCrimeRate = _maxCrimeRate;
    //    }
    //    else
    //        NotNegativeValueControllerOnDecrease(ref _allCrimeRate);

    //    ChangeDataWindow(DataType.Crime, _allCrimeRate);
    //}

    private void ChangeAttackType(AttackType type)
    {
        _playerAttackType = type;
        ChangeDataWindow(DataType.Attack, default);
    }

    private void NotNegativeValueControllerOnDecrease(ref int value)
    {
        value = value == 0 ? value : value - 1;        
    }

    private void ChangeSkipButtonStatus(int value)
    {
        _skipButton.interactable = value > _crimeRateValueToLoseOpportunityToMissTheFight ? false : true;
    }

    private void ChangeDataWindow(DataType dataType, int countChangeData = 0)
    {    
        _fightResult.text = "";
        _countPowerEnemyText.text = $"Enemy Power: {_enemy.Power}";        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
