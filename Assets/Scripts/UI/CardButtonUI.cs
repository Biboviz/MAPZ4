using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts;
using UnityEngine.EventSystems;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CardButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    private Card card;
    private int cardIndex;

    [SerializeField] private ShowSkillDescription tooltip;
    public void Init(Card cardData, int index)
    {
        card = cardData;
        cardIndex = index;
        nameText.text = card.Name;
        descriptionText.text = card.Description;
        GetComponent<Button>().onClick.AddListener(OnCardClicked);
    }

    public void OnCardClicked()
    {
        GameManager.Instance.PlayCard(cardIndex);
        Debug.Log($"Card {card.Name} played.");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show tooltip with card description
        tooltip.ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide tooltip
        tooltip.HideTooltip();
    }
}
