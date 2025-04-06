using TMPro;
using UnityEngine;

public class ShowSkillDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Vector2 tooltipTextOffset = new Vector2(10f, 10f);

    private void Start()
    {
        tooltipText.gameObject.SetActive(false);
    }
    public void ShowTooltip()
    {
        tooltipText.gameObject.SetActive(true);

        Vector3 mousePos = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mousePos.x + tooltipText.GetComponent<RectTransform>().rect.width > screenWidth)
        {
            mousePos.x = screenWidth - tooltipText.GetComponent<RectTransform>().rect.width - tooltipTextOffset.x; 
        }

        if (mousePos.y + tooltipText.GetComponent<RectTransform>().rect.height > screenHeight)
        {
            mousePos.y = screenHeight - tooltipText.GetComponent<RectTransform>().rect.height - tooltipTextOffset.y; 
        }
        tooltipText.transform.position = mousePos + new Vector3(tooltipTextOffset.x, tooltipTextOffset.y, 0f);

    }

    public void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false); 
    }
}
