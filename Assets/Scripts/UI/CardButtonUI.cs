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
    private GameManager gameManager;
    private int cardIndex;

    // Tooltip for showing card description
    [SerializeField] private ShowSkillDescription tooltip;
    public void Init(Card cardData, GameManager gm, int index)
    {
        card = cardData;
        gameManager = gm;
        cardIndex = index;
        nameText.text = card.Name;
        descriptionText.text = card.Description;
        GetComponent<Button>().onClick.AddListener(OnCardClicked);
    }

    public void OnCardClicked()
    {
        // Play card on enemy by default
        GameManager.Instance.PlayCard(cardIndex, card.Target);
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
