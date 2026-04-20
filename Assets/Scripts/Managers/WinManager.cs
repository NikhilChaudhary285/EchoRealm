// Singleton that handles the win state: UI, pause, event broadcast.
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance { get; private set; }

    [SerializeField] private GameObject winPanel; // assign a Canvas/Panel in Inspector

    private bool _hasWon = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (winPanel != null) winPanel.SetActive(false);
    }

    public void TriggerWin()
    {
        if (_hasWon) return;
        _hasWon = true;

        Debug.Log("[WinManager] Puzzle solved!");

        if (winPanel != null) winPanel.SetActive(true);

        // Optionally freeze time to pause physics after win
        // Time.timeScale = 0f;
    }

    public void Reset()
    {
        _hasWon = false;
        if (winPanel != null) winPanel.SetActive(false);
    }
}