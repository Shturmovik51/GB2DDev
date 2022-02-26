using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleStart : MonoBehaviour, IView
{
    [SerializeField] private Button _startFightButton;

    public void Init(UnityAction startBattle)
    {
        _startFightButton.onClick.AddListener(startBattle);
    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }

    public void Show()
    {
        throw new System.NotImplementedException();
    }
    private void OnDisable()
    {
        _startFightButton.onClick.RemoveAllListeners();
    }
}

