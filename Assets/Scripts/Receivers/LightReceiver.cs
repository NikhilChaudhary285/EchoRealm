// Changes a Light component's color and intensity when activated/deactivated.
// The "visual cue" requirement for the feedback system.
using UnityEngine;

public class LightReceiver : ReceiverBase
{
    [Header("Light Settings")]
    [SerializeField] private Light targetLight;
    [SerializeField] private Color onColor = new Color(0.2f, 1f, 0.2f);   // green
    [SerializeField] private Color offColor = new Color(1f, 0.1f, 0.1f);   // red
    [SerializeField] private float onIntensity = 2f;
    [SerializeField] private float offIntensity = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        if (targetLight == null)
            targetLight = GetComponentInChildren<Light>();

        ApplyLightState(false);
    }

    public override void OnActivated()
    {
        base.OnActivated();
        ApplyLightState(true);
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        ApplyLightState(false);
    }

    private void ApplyLightState(bool on)
    {
        if (targetLight == null) return;
        targetLight.color = on ? onColor : offColor;
        targetLight.intensity = on ? onIntensity : offIntensity;
    }

    public override void ResetToInitial()
    {
        isActivated = false;
        ApplyLightState(false);
        ApplyVisualFeedback(inactiveColor);
    }
}