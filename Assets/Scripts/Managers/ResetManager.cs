// Listens for R key. Finds ALL IResettable objects in the scene and resets them.
// WHY FindObjectsByType: we never need to manually register objects — just implement IResettable.
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    [SerializeField] private KeyCode resetKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetKeyDown(resetKey))
            ResetAll();
    }

    private void ResetAll()
    {
        // FindObjectsByType finds all MonoBehaviours in the scene — we filter by interface
        var allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (var obj in allObjects)
        {
            if (obj is IResettable resettable)
                resettable.ResetToInitial();
        }

        // Also reset the win state
        WinManager.Instance?.Reset();

        Debug.Log("[ResetManager] Scene reset.");
    }
}