// A platform that moves between two positions when activated.
// Players standing on it are carried along via OnCollisionStay parenting trick.
using System.Collections;
using UnityEngine;

public class MovingPlatform : ReceiverBase
{
    [Header("Platform Movement")]
    [SerializeField] private Vector3 moveOffset = new Vector3(5f, 0, 0);
    [SerializeField] private float moveDuration = 1.5f;
    [SerializeField] private bool returnOnDeactivate = true;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Coroutine _moveCoroutine;

    protected override void Awake()
    {
        base.Awake();
        _startPosition = transform.position;
        _endPosition = transform.position + moveOffset;
    }

    // Carry the player by temporarily making them a child of the platform
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(null);
    }

    public override void OnActivated()
    {
        base.OnActivated();
        StartMove(_endPosition);
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        if (returnOnDeactivate)
            StartMove(_startPosition);
    }

    private void StartMove(Vector3 target)
    {
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _moveCoroutine = StartCoroutine(MoveToPosition(target));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0, 1, elapsed / moveDuration));
            yield return null;
        }

        transform.position = target;
    }

    public override void ResetToInitial()
    {
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        transform.position = _startPosition;
        isActivated = false;
        ApplyVisualFeedback(inactiveColor);
    }
}