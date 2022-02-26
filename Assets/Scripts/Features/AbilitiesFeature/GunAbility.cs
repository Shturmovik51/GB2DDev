using System;
using DG.Tweening;
using Features.AbilitiesFeature;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class GunAbility : BaseController, IAbility
{
    private readonly Rigidbody2D _viewPrefab;
    private readonly float _projectileSpeed;

    public GunAbility([NotNull] GameObject viewPrefab, float projectileSpeed)
    {
        _viewPrefab = viewPrefab.GetComponent<Rigidbody2D>();
        if (_viewPrefab == null) throw new InvalidOperationException($"{nameof(GunAbility)} view requires {nameof(Rigidbody2D)} component!");
        _projectileSpeed = projectileSpeed;
    }

    public void Apply(IAbilityActivator activator)
    {
        var projectile = Object.Instantiate(_viewPrefab, activator.GetViewObject().transform);
        projectile.transform.parent = null;
        projectile.AddForce(activator.GetViewObject().transform.right * _projectileSpeed, ForceMode2D.Impulse);

        var sequence = DOTween.Sequence();
        sequence.AppendInterval(4);
        sequence.OnComplete(() => Object.Destroy(projectile.gameObject));
    }
}