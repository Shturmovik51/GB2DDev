using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShedView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStartGame;
    [SerializeField] private TextMeshProUGUI _buttonStartGameTMPro;
    [SerializeField] private string _startGameButtonText;
    [SerializeField] private string _continueGameButtonText;
    [SerializeField] private Transform _placeForShedInventory;

    public Transform PlaceForShedInventory => _placeForShedInventory;

    public void Init(UnityAction startGame)
    {
        _buttonStartGame.onClick.AddListener(startGame);
        _buttonStartGameTMPro.text = _startGameButtonText;
    }

    public void SetButtonTextAsContinue()
    {
        _buttonStartGameTMPro.text = _continueGameButtonText;
    }

    protected void OnDestroy()
    {
        _buttonStartGame.onClick.RemoveAllListeners();
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
