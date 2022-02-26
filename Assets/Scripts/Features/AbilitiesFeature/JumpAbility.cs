using System;
using DG.Tweening;
using System.Collections;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class JumpAbility : BaseController, IAbility
{
    private readonly float _abilityDuration;
    private readonly float _jumpForse;
    private Transform _ownerTransform;
    private Sequence _sequence;

    public JumpAbility(float abilityDuration, float jumpForse)
    {
        _abilityDuration = abilityDuration;
        _jumpForse = jumpForse;
    }

    public void Apply(IAbilityActivator activator)
    {
        if (_sequence != null)
            return;

        if (_ownerTransform == null)
            _ownerTransform = activator.GetViewObject().transform;

        _sequence = DOTween.Sequence();
        _sequence.Append(_ownerTransform.DOJump(_ownerTransform.position, _jumpForse, 1, _abilityDuration));
        _sequence.OnComplete(() => _sequence = null);
    }    
}
