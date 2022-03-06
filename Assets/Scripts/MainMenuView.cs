using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStartGarage;
    [SerializeField] private Button _buttonStartRewards;
    [SerializeField] private Button _buttonStartLocalization;
    public void Init(UnityAction startGame, UnityAction startGarage, UnityAction startLocalization) 
    {
        _buttonStartGarage.onClick.AddListener(startGame);
        _buttonStartRewards.onClick.AddListener(startGarage);
        _buttonStartLocalization.onClick.AddListener(startLocalization);
    }

    protected void OnDestroy()
    {
        _buttonStartGarage.onClick.RemoveAllListeners();
        _buttonStartRewards.onClick.RemoveAllListeners();
        _buttonStartLocalization.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        
    }

    public void Hide()
    {
        
    }
}