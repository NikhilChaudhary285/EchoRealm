// WHY: Shared logic for all receivers — audio, visual feedback, and reset.
// Each concrete receiver only implements what it physically does (slide a door, etc.)
using UnityEngine;
using UnityEngine.Events;

public abstract class ReceiverBase : MonoBehaviour, IReceiver, IResettable
{
    [Header("Feedback")]
    [SerializeField] protected AudioClip activateSound;
    [SerializeField] protected AudioClip deactivateSound;
    [SerializeField] protected Renderer visualRenderer;

    [Header("Colors")]
    [SerializeField] protected Color activeColor = new Color(0.2f, 0.8f, 0.2f);
    [SerializeField] protected Color inactiveColor = new Color(0.8f, 0.8f, 0.8f);

    protected AudioSource audioSource;
    protected bool isActivated = false;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;

        ApplyVisualFeedback(inactiveColor);
    }

    // Called by InteractionConnector when the linked interactable becomes active
    public virtual void OnActivated()
    {
        if (isActivated) return;
        isActivated = true;
        ApplyVisualFeedback(activeColor);
        PlaySound(activateSound);
    }

    // Called when the interactable deactivates
    public virtual void OnDeactivated()
    {
        if (!isActivated) return;
        isActivated = false;
        ApplyVisualFeedback(inactiveColor);
        PlaySound(deactivateSound);
    }

    protected void ApplyVisualFeedback(Color color)
    {
        if (visualRenderer != null)
            visualRenderer.material.color = color;
    }

    protected void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

    public abstract void ResetToInitial();
}