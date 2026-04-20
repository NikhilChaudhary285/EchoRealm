// WHY: Any object that needs to restore its initial state implements this.
// ResetManager finds all IResettable objects in the scene via FindObjectsOfType.
public interface IResettable
{
    void ResetToInitial();
}