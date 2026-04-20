// A door that slides open/closed. Direction and distance are configurable.
// Uses a coroutine for smooth lerped movement rather than physics snapping.
using System.Collections;
using UnityEngine;

public class SlidingDoor : ReceiverBase
{
    [Header("Door Movement")]
    [SerializeField] private Vector3 openOffset = new Vector3(0, 3f, 0); // how far it slides
    [SerializeField] private float slideDuration = 1.0f;

    private Vector3 _closedPosition;
    private Vector3 _openPosition;
    private Coroutine _moveCoroutine;

    protected override void Awake()
    {
        base.Awake();
        _closedPosition = transform.position;
        _openPosition = transform.position + openOffset;
    }

    public override void OnActivated()
    {
        base.OnActivated();
        StartMove(_openPosition);
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        StartMove(_closedPosition);
    }

    private void StartMove(Vector3 target)
    {
        // Cancel any in-progress move before starting a new one
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MoveToPosition(target));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / slideDuration); // ease in/out
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target; // snap to exact target at end
    }

    public override void ResetToInitial()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        transform.position = _closedPosition;
        isActivated = false;
        ApplyVisualFeedback(inactiveColor);
    }
}