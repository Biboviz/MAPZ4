using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts;
using UnityEngine.EventSystems;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CardButtonUI : MonoBehaviour
{
    [SerializeField] Image sprite;
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
        card.ButtonUI = this;


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
    public void SetInteractable(bool state)
    {
        if (this == null) return;
        GetComponent<Button>().interactable = state;
        transform.localScale = new Vector3(0.75f, -0.75f, 1);
        sprite.color = Color.grey;
    }
    public void OnCardClicked()
    {
        var command = new CardClickCommand(card);
        GameManager.Instance.PlayCard(command); 
    }
}
