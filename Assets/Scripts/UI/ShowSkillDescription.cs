using TMPro;
using UnityEngine;

public class ShowSkillDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Vector2 tooltipTextOffset = new Vector2(10f, 10f); // Optional offset from mouse position

    private void Start()
    {
        tooltipText.gameObject.SetActive(false);
    }
    public void ShowTooltip()
    {
        tooltipText.gameObject.SetActive(true);

        Vector3 mousePos = Input.mousePosition;

        // Get the screen width and height
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Adjust the tooltip posityion to prevent it from going off-screen
        if (mousePos.x + tooltipText.GetComponent<RectTransform>().rect.width > screenWidth)
        {
            mousePos.x = screenWidth - tooltipText.GetComponent<RectTransform>().rect.width - tooltipTextOffset.x; // Keep the tooltipText within screen width
        }

        if (mousePos.y + tooltipText.GetComponent<RectTransform>().rect.height > screenHeight)
        {
            mousePos.y = screenHeight - tooltipText.GetComponent<RectTransform>().rect.height - tooltipTextOffset.y; // Keep the tooltipText within screen height
        }

        // Apply the mouse position plus the optional offset to the tooltipText
        tooltipText.transform.position = mousePos + new Vector3(tooltipTextOffset.x, tooltipTextOffset.y, 0f);

    }

    public void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false); 
    }
}
