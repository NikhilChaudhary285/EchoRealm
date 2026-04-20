// Shows a "Press E" prompt when the player is near a lever.
// Attach this to a Canvas with a Text or TMP_Text element.
using UnityEngine;
using TMPro;

public class LeverPromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Lever[] leversInScene; // drag all levers in here

    private void Update()
    {
        bool anyInRange = false;
        foreach (var lever in leversInScene)
        {
            if (lever != null && lever.IsPlayerInRange)
            {
                anyInRange = true;
                break;
            }
        }

        if (promptText != null)
            promptText.gameObject.SetActive(anyInRange);
    }
}