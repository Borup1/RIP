using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // The panel or GameObject to toggle

    // This function will toggle the panel's visibility
    public void TogglePanelVisibility()
    {
        // Toggles the active state of the panel
        panel.SetActive(!panel.activeSelf);
    }
}
