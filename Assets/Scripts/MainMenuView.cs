using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private TextMeshProUGUI _fuelCount;

    public void Init(UnityAction startGame, int fuel)
    {
        _buttonStart.onClick.AddListener(startGame);
        _fuelCount.text = fuel.ToString();
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
    }
}