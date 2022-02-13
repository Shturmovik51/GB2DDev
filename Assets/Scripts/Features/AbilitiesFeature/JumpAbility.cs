using System;
using System.Collections;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class JumpAbility : IAbility
{
    private readonly float _abilityDuration;
    private readonly float _jumpForse;
    private float _currentTimeJump;
    private Coroutine _goingUp;
    private Coroutine _goingDown;
    private Transform _ownerTransform;
    private float _groundHight;

    public JumpAbility([NotNull] GameObject viewPrefab, float abilityDuration, float jumpForse)
    {
        _currentTimeJump = 0f;
        _abilityDuration = abilityDuration;
        _jumpForse = jumpForse;
    }

    public void Apply(IAbilityActivator activator, AbilitiesView sender)
    {
        if (_ownerTransform == null)
            _ownerTransform = activator.GetViewObject().transform;
        if (_goingUp == null)
        {
            _groundHight = _ownerTransform.position.y;
            _goingUp = sender.StartCoroutine(GoingUp(_abilityDuration, _jumpForse, sender));
        }
    }

    private IEnumerator GoingUp(float abilityDuration, float _jumpForse, AbilitiesView sender)
    {
        while (_currentTimeJump < abilityDuration)
        {
            yield return new WaitForFixedUpdate();
            var position = _ownerTransform.position;
            position.y += _jumpForse;
            _ownerTransform.position = position;
            _currentTimeJump += Time.fixedDeltaTime;
        }        
        _goingDown = sender.StartCoroutine(GoingDown(_jumpForse));
        _goingUp = null;
        yield break;
    }
    private IEnumerator GoingDown(float _jumpForse)
    {
        while (_ownerTransform.position.y > _groundHight)
        {
            yield return new WaitForFixedUpdate();
            var position = _ownerTransform.position;
            position.y -= _jumpForse;
            _ownerTransform.position = position;
        }
        var startPosition = _ownerTransform.position;
        startPosition.y = _groundHight;
        _ownerTransform.position = startPosition;
        _currentTimeJump = 0f;
        _goingDown = null;        
        yield break;
    }
}
