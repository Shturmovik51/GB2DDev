using System;
using System.Collections;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class SmokeAbility : IAbility
{
    private readonly GameObject _smoke;
    private readonly float _abilityDuration;
    private Coroutine _abilityProgress;

    public SmokeAbility([NotNull] GameObject viewPrefab, float abilityDuration)
    {
        _abilityDuration = abilityDuration;
        _smoke = Object.Instantiate(viewPrefab);
        _smoke.SetActive(false);
    }

    public void Apply(IAbilityActivator activator, AbilitiesView sender)
    {
        _smoke.transform.position = activator.GetViewObject().transform.position;
        _smoke.SetActive(true);

        if (_abilityProgress == null)
            _abilityProgress = sender.StartCoroutine(ShieldProgress(_abilityDuration));
    }

    private IEnumerator ShieldProgress(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        _smoke.SetActive(false);
        _abilityProgress = null;
        yield break;
    }
}
