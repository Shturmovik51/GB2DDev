using System;
using System.Collections;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShieldAbility : IAbility
{
    private readonly GameObject _shield;
    private readonly float _abilityDuration;
    private Coroutine _abilityProgress;

    public ShieldAbility([NotNull] GameObject viewPrefab, float abilityDuration)
    {
        _abilityDuration = abilityDuration;
        _shield = Object.Instantiate(viewPrefab);
        _shield.SetActive(false);
    }

    public void Apply(IAbilityActivator activator, AbilitiesView sender)
    {
        _shield.transform.position = activator.GetViewObject().transform.position;
        _shield.SetActive(true);

        if(_abilityProgress == null)
            _abilityProgress = sender.StartCoroutine(ShieldProgress(_abilityDuration));
    }

    private IEnumerator ShieldProgress(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        _shield.SetActive(false);
        _abilityProgress = null;
        yield break;
    }
}
