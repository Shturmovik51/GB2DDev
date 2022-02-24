using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStartGarage;
    [SerializeField] private Button _buttonStartRewards;

    public void Init(UnityAction startGame, UnityAction startGarage) 
    {
        _buttonStartGarage.onClick.AddListener(startGame);
        _buttonStartRewards.onClick.AddListener(startGarage);
    }

    protected void OnDestroy()
    {
        _buttonStartGarage.onClick.RemoveAllListeners();
        _buttonStartRewards.onClick.RemoveAllListeners();
    }

    public void Show()
    {
        
    }

    public void Hide()
    {
        
    }
}