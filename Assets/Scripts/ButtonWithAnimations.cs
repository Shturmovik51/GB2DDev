using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWithAnimations : Button
{    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
    }
       
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }
}
