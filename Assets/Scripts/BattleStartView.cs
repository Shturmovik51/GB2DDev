using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleStartView : MonoBehaviour, IView
{
    [SerializeField] private Button _startFightButton;

    public void Init(UnityAction startBattle)
    {

    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }

    public void Show()
    {
        throw new System.NotImplementedException();
    }
}

