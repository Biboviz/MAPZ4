using UnityEngine;
using System.Collections;
using Assets.Scripts.Cards.Factory;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using Assets.Scripts.Cards;


namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private GameManager() { }
        

        [SerializeField] int MaxAttackCardsInHand = 2;
        [SerializeField] int MaxDefenseCardsInHand = 3;
        [SerializeField] int MaxEnemiesCardsInHand = 1;
        [SerializeField] GameObject Player;
        [SerializeField] GameObject Enemy;
        [SerializeField] GameObject CardButtonPrefab;
        [SerializeField] Transform PlayerCardsPanel;
        [SerializeField] Transform EnemyCardsPanel;

        IFactory attackCardFactory;
        IFactory defenseCardFactory;
        IFactory teacherCardFactory;
        List<Card> playerCards;
        List<Card> enemyCards;

        public bool DevMode = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        public void Start()
        {
            CreatePlayerCards();
            CreateEnemyCards();
            RenderPlayerCards();
            RenderEnemyCards();
        }

        private void CreateEnemyCards()
        {
            enemyCards = new List<Card>();
            teacherCardFactory = new TeacherCardFactory();
            ManufactureCardsInFactory(teacherCardFactory, MaxEnemiesCardsInHand, Player);
        }
        public void CreatePlayerCards()
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
        }
        void ManufactureCardsInFactory(IFactory factoryName, int maxCardsInHand, GameObject Target)
        {
            for (int i = 0; i < maxCardsInHand; i++)
            {
                Card card = (Card)factoryName.CreateCard();
                card.Target = Target;
                if (UnityEngine.Random.Range(0, 2) == 1)
                    card = ApplyRandomSingleDecorator(card);

                card = new CardPlayLoggerProxy(card);

                if (factoryName is TeacherCardFactory) enemyCards.Add(card);
                else playerCards.Add(card);
            }
        }
        internal void RenderPlayerCards()
        {
            foreach (Transform child in PlayerCardsPanel)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < playerCards.Count; i++)
            {
                GameObject buttonGO = Instantiate(CardButtonPrefab, PlayerCardsPanel); //Both set in unity UI
                CardButtonUI buttonUI = buttonGO.GetComponent<CardButtonUI>(); //Change Ui elements of card
                buttonUI.Init(playerCards[i], i);
            }
        }
        internal void RenderEnemyCards()
        {
            foreach (Transform child in EnemyCardsPanel)
            {
                Destroy(child.gameObject);
            }
            if (enemyCards.Count > 0)
            {
                GameObject intentButton = Instantiate(CardButtonPrefab, EnemyCardsPanel); 
                CardButtonUI buttonUI = intentButton.GetComponent<CardButtonUI>();
                buttonUI.Init(enemyCards[0], 0, true);
            }
        }
        public string GetEnemyIntent()
        {
            if (enemyCards.Count > 0)
            {
                return enemyCards[0].Description;
            }
            return "Thinking...";
        }
        internal void PlayCard(int index)
        {
            Card selectedCard = playerCards[index];
            selectedCard.Play();
        }
        Card ApplyRandomSingleDecorator(Card baseCard)
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
}