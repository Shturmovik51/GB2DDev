using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShieldAbility : BaseController, IAbility
{
    private readonly GameObject _shield;
    private readonly float _abilityDuration;
    private SpriteRenderer _spriteRenderer;
    private const float ShowAndHideDuration = 2;
    private Sequence _sequence;
    public ShieldAbility([NotNull] GameObject viewPrefab, float abilityDuration)
    {
        _abilityDuration = abilityDuration;
        _shield = Object.Instantiate(viewPrefab);
        AddGameObjects(_shield);
        _shield.SetActive(false);
        _spriteRenderer = _shield.GetComponent<SpriteRenderer>();

        var color = _spriteRenderer.color;
        color.a = 0;
        _spriteRenderer.color = color;
    }

    public void Apply(IAbilityActivator activator)
    {
        if (_sequence != null)
            return;

        _shield.transform.position = activator.GetViewObject().transform.position;
        _shield.SetActive(true);

        var showColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
        var hideColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0);

        _sequence = DOTween.Sequence();
        _sequence.Append(_spriteRenderer.DOColor(showColor, ShowAndHideDuration));
        _sequence.AppendInterval(_abilityDuration);
        _sequence.Append(_spriteRenderer.DOColor(hideColor, ShowAndHideDuration));
        _sequence.OnComplete(DisApply);
    }
    private void DisApply()
    {
        _sequence = null;
        _shield.SetActive(false);
    }
}
