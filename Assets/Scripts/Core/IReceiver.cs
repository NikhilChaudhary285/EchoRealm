// WHY: Every door, platform, light, etc. implements this contract.
// InteractionConnector calls these methods when its linked interactable changes state.
public interface IReceiver
{
    void OnActivated();
    void OnDeactivated();
}