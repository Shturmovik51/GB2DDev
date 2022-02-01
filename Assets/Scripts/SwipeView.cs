using JoostenProductions;
using Tools;
using UnityEngine;

public class SwipeView : BaseInputView
{
    private float _swipeAcceleration = 0.1f;
    private float _slowUpPerSecond = 0.5f;
    private float _currentTouchX;
    private TrailRenderer _trailRenderer;
    private Camera _camera;

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        UpdateManager.SubscribeToUpdate(OnUpdate);
        _camera = Camera.main;
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.emitting = false;
    }

    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

                var touchWorldPos = _camera.ScreenToWorldPoint(touch.position);
                var trailRendererPos = _trailRenderer.transform.position;

                trailRendererPos.x = touchWorldPos.x;
                trailRendererPos.y = touchWorldPos.y;

                _trailRenderer.transform.position = trailRendererPos;

            if (touch.phase == TouchPhase.Began)
            {
                _currentTouchX = touch.position.x;
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (_trailRenderer.emitting == false)
                    _trailRenderer.emitting = true;
                var step = 0f;

                if (touch.position.x != _currentTouchX)
                {
                    step = touch.position.x - _currentTouchX;
                    _currentTouchX = touch.position.x;
                }

                AddAcceleration(step * Time.deltaTime * _swipeAcceleration);                
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _trailRenderer.emitting = false;
            }
        }

        Move();
        Slowdown();
    }
    private void AddAcceleration(float acc)
    {
        _speed = Mathf.Clamp(_speed + acc, -1f, 1f);
    }

    private void Move()
    {
        if (_speed > 0)
            OnRightMove(_speed);
        else if (_speed < 0)
            OnLeftMove(_speed);
    }

    private void Slowdown()
    {
        var sgn = Mathf.Sign(_speed);
        _speed = Mathf.Clamp01(Mathf.Abs(_speed) - _slowUpPerSecond * Time.deltaTime) * sgn;
    }
}
