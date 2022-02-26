using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UI;

public partial class FightWindowView : MonoBehaviour, IView
{
    [SerializeField] private TMP_Text _countMoneyText;
    [SerializeField] private TMP_Text _countHealthText;
    [SerializeField] private TMP_Text _countPowerText;
    [SerializeField] private TMP_Text _countCrimeRateText;
    [SerializeField] private TMP_Text _countPowerEnemyText;
    [SerializeField] private TMP_Text _attackTypePlayerText;
    [SerializeField] private TMP_Text _attackTypeEnemyText; 
    [SerializeField] private TMP_Text _fightResult;

    public TMP_Text CountMoneyText => _countMoneyText;
    public TMP_Text CountHealthText => _countHealthText;
    public TMP_Text CountPowerText => _countPowerText;
    public TMP_Text CountCrimeRateText => _countCrimeRateText;
    public TMP_Text CountPowerEnemyText => _countPowerEnemyText;
    public TMP_Text AttackTypePlayerText => _attackTypePlayerText;
    public TMP_Text AttackTypeEnemyText => _attackTypeEnemyText;
    public TMP_Text FightResult => _fightResult;

    #region Buttons
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
    [SerializeField] private Button _closeButton;

    public Button SkipButton => _skipButton;
    #endregion

    public void Init(UnityAction<DataType, int> changeAction, UnityAction fight, UnityAction skip, UnityAction close)
    {
        SubscribeButtons(changeAction, fight, skip, close);
    }

    private void SubscribeButtons(UnityAction<DataType, int> changeAction, UnityAction fight, UnityAction skip, UnityAction close)
    {
        _addMoneyButton.onClick.AddListener(() => changeAction(DataType.Money, 1));
        _minusMoneyButton.onClick.AddListener(() => changeAction(DataType.Money, -1));

        _addHealthButton.onClick.AddListener(() => changeAction(DataType.Health, 1));
        _minusHealthButton.onClick.AddListener(() => changeAction(DataType.Health, -1));

        _addPowerButton.onClick.AddListener(() => changeAction(DataType.Power, 1));
        _minusPowerButton.onClick.AddListener(() => changeAction(DataType.Power, -1));

        _addCrimeRate.onClick.AddListener(() => changeAction(DataType.Crime, 1));
        _minusCrimeRate.onClick.AddListener(() => changeAction(DataType.Crime, -1));

        _setKnife.onClick.AddListener(() => changeAction(DataType.Attack, (int)AttackType.Knife));
        _setGun.onClick.AddListener(() => changeAction(DataType.Attack, (int)AttackType.Gun));

        _fightButton.onClick.AddListener(fight);
        _skipButton.onClick.AddListener(skip);
        _closeButton.onClick.AddListener(close);
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
      
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
