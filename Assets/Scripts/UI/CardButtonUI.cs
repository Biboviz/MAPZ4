using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts;
using UnityEngine.EventSystems;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CardButtonUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI costText;

    private Card card;
    private int cardIndex;

    [SerializeField] private bool isIntentDisplay = false;
    public void Init(Card cardData, int index, bool intentDisplay = false)
    {
        card = cardData;
        cardIndex = index;
        nameText.text = card.Name;
        descriptionText.text = card.Description;
        isIntentDisplay = intentDisplay;

        defenseText.text = (card as IDefense)?.Defense.ToString() ?? " ";
        damageText.text = (card as IDamage)?.Damage.ToString() ?? " ";
        costText.text = (card as ICost)?.Cost.ToString() ?? " ";

        if (!isIntentDisplay)
        {
            GetComponent<Button>().onClick.AddListener(OnCardClicked);
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
    public void OnCardClicked()
    {
        GameManager.Instance.PlayCard(cardIndex);
    }

}
