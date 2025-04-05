using UnityEngine;
using System.Collections;
using Assets.Scripts.Cards.Factory;
using NUnit.Framework;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private GameManager() { }

        [SerializeField] int MaxAttackCardsInHand = 2;
        [SerializeField] int MaxDefenseCardsInHand = 3;
        [SerializeField] GameObject Player;
        [SerializeField] GameObject Enemy;
        [SerializeField] GameObject CardButtonPrefab;
        [SerializeField] Transform CardPanel;
        IFactory attackCardFactory;
        IFactory defenseCardFactory;
        List<Card> cards;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        void Start()
        {
            InitializeCards();
            RenderCards();
        }
        void InitializeCards()
        {
            attackCardFactory = new AttackCardFactory();
            defenseCardFactory = new DefenseCardFactory();
            cards = new List<Card>();

            Card brain = new CardBuilder()
                .CreateBaseCard("The last braincell", "Remember most useless information stored in a brain.")
                .WithEffect("Decrease your defence by 2")
                .WithTarget(Player)
                .WithPlayAction(() =>
                {
                    Debug.Log("PlayAction triggered!");
                    if (Player != null)
                    {
                        var stats = Player.GetComponent<CharacterStats>();
                        stats.DecreaseDefense(2);
                        Debug.Log($"The last braincell played! Defense decreased by 2. Player's defence now is {stats.def}");
                    }
                    else
                    {
                        Debug.LogWarning("Player is null in brain card!");
                    }
                })
                .Build();

            cards.Add(brain);

            ManufactureCardsInFactory(defenseCardFactory, MaxDefenseCardsInHand, Player);
            ManufactureCardsInFactory(attackCardFactory, MaxAttackCardsInHand, Enemy);

        }
        void ManufactureCardsInFactory(IFactory factoryName, int maxCardsInHand, GameObject Target)
        {
            for (int i = 0; i < maxCardsInHand; i++)
            {
                Card card = (Card)factoryName.CreateCard();
                card.Target = Target;
                cards.Add(card);
            }
        }
        void RenderCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                GameObject buttonGO = Instantiate(CardButtonPrefab, CardPanel);
                CardButtonUI buttonUI = buttonGO.GetComponent<CardButtonUI>();
                buttonUI.Init(cards[i], this, i);
            }
        }

        internal void PlayCard(int index)
        {
            Card selectedCard = cards[index];
            selectedCard.Play();
        }
    }
}