using Assets.Scripts.Cards;
using Assets.Scripts.Cards.Factory;
using Assets.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

public class CardService : MonoBehaviour
{
    [SerializeField] int MaxAttackCardsInHand = 2;
    [SerializeField] int MaxDefenseCardsInHand = 3;
    [SerializeField] int MaxEnemiesCardsInHand = 1;

    IFactory attackCardFactory;
    IFactory defenseCardFactory;
    IFactory teacherCardFactory;
    private List<Card> playerCards { get; set; }
    private List<Card> enemyCards { get; set; }

    public List<Card> PlayerCards => playerCards;
    public List<Card> EnemyCards => enemyCards;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject Enemy;

    private TeacherStats teacherStats;

    private void Awake()
    {
        if (Enemy != null)
        {
            teacherStats = Enemy.GetComponent<TeacherStats>();
        }
    }

    public List<Card> CreateEnemyCards()
    {
        enemyCards = new List<Card>();
        teacherCardFactory = new TeacherCardFactory();
        ManufactureCardsInFactory(teacherCardFactory, MaxEnemiesCardsInHand, Player);

        return enemyCards;
    }

    public List<Card> CreatePlayerCards()
    {
        playerCards = new List<Card>();
        attackCardFactory = new AttackCardFactory();
        defenseCardFactory = new DefenseCardFactory();

        var brain = new CardBuilder();
        brain.WithName("The last braincell");
        brain.WithEffect("Remember most useless information stored in a brain.\n Decrease your defence by 2");
        brain.WithTarget(Player);
        brain.WithPlayAction(() =>
        {
            if (Player != null)
            {
                var stats = Player.GetComponent<CharacterStats>();
                stats.DecreaseDefense(2);
            }
        });
        var customCard = brain.Build();
        playerCards.Add(new CardPlayLoggerProxy(customCard));

        ManufactureCardsInFactory(defenseCardFactory, MaxDefenseCardsInHand, Player);
        ManufactureCardsInFactory(attackCardFactory, MaxAttackCardsInHand, Enemy);

        return playerCards;
    }

    public void ManufactureCardsInFactory(IFactory factoryName, int maxCardsInHand, GameObject Target)
    {
        for (int i = 0; i < maxCardsInHand; i++)
        {
            Card card = (Card)factoryName.CreateCard();
            card.Target = Target;
            if (UnityEngine.Random.Range(0, 2) == 1)
                card = ApplyRandomSingleDecorator(card);

            var wrappedCard = new CardPlayLoggerProxy(card);

            if (wrappedCard.TryGetInterface<IDefense>() != null)
            {
                teacherStats.Attach(wrappedCard);
            }

            if (factoryName is TeacherCardFactory) enemyCards.Add(wrappedCard);
            else playerCards.Add(wrappedCard);
        }
    }

    public Card ApplyRandomSingleDecorator(Card baseCard)
    {
        float roll = UnityEngine.Random.Range(0f, 1f);

        if (roll < 0.3f)
            return new BoostedCard(baseCard);
        else if (roll < 0.5f)
            return new CursedCard(baseCard);
        else if (roll < 0.7f)
            return new SlightlyBoostedCard(baseCard);

        // No decorator
        return baseCard;
    }
}
