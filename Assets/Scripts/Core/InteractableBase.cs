// WHY: Shared logic (state tracking, event firing, audio/visual feedback hooks)
// lives here once. Concrete classes only implement what makes them unique.
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableBase : MonoBehaviour, IInteractable, IResettable
{
    [Header("Feedback")]
    [SerializeField] protected AudioClip activateSound;
    [SerializeField] protected AudioClip deactivateSound;
    [SerializeField] protected Renderer visualRenderer; // optional mesh to tint

    [Header("Colors")]
    [SerializeField] protected Color activeColor = Color.green;
    [SerializeField] protected Color inactiveColor = Color.red;

    public bool IsActive { get; protected set; } = false;
    public event UnityAction<bool> OnStateChanged;

    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        // Get or auto-add an AudioSource so every interactable can play sounds
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D audio

        ApplyColor(inactiveColor);
    }

    // Call this from subclasses when the state should change.
    // Centralising here means subclasses can never forget to fire the event.
    protected void SetState(bool newState)
    {
        if (IsActive == newState) return; // no change — do nothing

        IsActive = newState;
        ApplyColor(IsActive ? activeColor : inactiveColor);
        PlayFeedbackSound(IsActive ? activateSound : deactivateSound);
        OnStateChanged?.Invoke(IsActive); // notify all connected receivers
    }

    private void ApplyColor(Color color)
    {
        if (visualRenderer != null)
            visualRenderer.material.color = color;
    }

    private void PlayFeedbackSound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

    // Each subclass defines how to reset itself. At minimum, call SetState(false).
    public abstract void ResetToInitial();
}