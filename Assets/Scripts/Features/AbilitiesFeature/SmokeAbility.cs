using System;
using DG.Tweening;
using System.Collections;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class SmokeAbility : BaseController, IAbility
{
    private SpriteRenderer[] _spriteRenderers;
    private readonly GameObject _smoke;
    private readonly float _abilityDuration;
    private const float ShowAndHideDuration = 2;
    private Sequence _sequence;
    
    public SmokeAbility([NotNull] GameObject viewPrefab, float abilityDuration)
    {
        _abilityDuration = abilityDuration;
        _smoke = Object.Instantiate(viewPrefab);
        AddGameObjects(_smoke);
        _smoke.SetActive(false);

        _spriteRenderers = _smoke.GetComponentsInChildren<SpriteRenderer>();

        foreach (var spriteRenderer in _spriteRenderers)
        {
            var color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
        }
    }

    public void Apply(IAbilityActivator activator)
    {
        if (_sequence != null)
            return;       

        _smoke.transform.position = activator.GetViewObject().transform.position;
        _smoke.SetActive(true);

        var showColor = new Color(_spriteRenderers[0].color.r, _spriteRenderers[0].color.g, _spriteRenderers[0].color.b, 1);
        var hideColor = new Color(_spriteRenderers[0].color.r, _spriteRenderers[0].color.g, _spriteRenderers[0].color.b, 0);

        _sequence = DOTween.Sequence();
        _sequence.Append(_spriteRenderers[0].DOColor(showColor, ShowAndHideDuration));

        for (int i = 1; i < _spriteRenderers.Length; i++)
        {
            _sequence.Join(_spriteRenderers[i].DOColor(showColor, ShowAndHideDuration));          

        }

        _sequence.AppendInterval(_abilityDuration);
        _sequence.Append(_spriteRenderers[0].DOColor(hideColor, ShowAndHideDuration));

        for (int i = 1; i < _spriteRenderers.Length; i++)
        {
            _sequence.Join(_spriteRenderers[i].DOColor(hideColor, ShowAndHideDuration));

        }
        _sequence.OnComplete(DisApply);
    }

    private void DisApply()
    {
        _sequence = null;
        _smoke.SetActive(false);
    }
}
