using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GarageMenuView : MonoBehaviour, IView
{
    [SerializeField] private Button _buttonStartGame;
    [SerializeField] private TextMeshProUGUI _buttonStartGameTMPro;
    [SerializeField] string _startGameButtonText;
    [SerializeField] string _continueGameButtonText;

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
