using UnityEngine;
using System.Collections;
using Assets.Scripts.Cards.Factory;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;


namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private GameManager() { }

        [SerializeField] int MaxCardsInHand = 5;
        [SerializeField] GameObject Player;
        [SerializeField] GameObject Enemy;
        [SerializeField] GameObject CardButtonPrefab;
        [SerializeField] Transform CardPanel;
        IFactory attackCardFactory;
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
            attackCardFactory = new AttackCardFactory();
            cards = new List<Card>();
            Card brain = new CardBuilder()
                .CreateBaseCard("The last braincell", "Remember most useless information stored in a brain.")
                .WithEffect("Decrease your defence by 2")
                .WithTarget(Player)
                .WithPlayAction(() =>
                {
                    if (Player != null)
                    {
                        Player.GetComponent<CharacterStats>().DecreaseDefense(2);
                        Debug.Log($"Player defense = {Player.GetComponent<CharacterStats>().def}");
                    }
                })
                .Build();
            cards.Add(brain);
            for (int i = 0; i < MaxCardsInHand + 1; i++)
            {
                Card card = (Card)attackCardFactory.CreateCard();
                cards.Add(card);
                card.Target = Player;
                // Instantiate button and assign data
                GameObject buttonGO = Instantiate(CardButtonPrefab, CardPanel);
                CardButtonUI buttonUI = buttonGO.GetComponent<CardButtonUI>();
                buttonUI.Init(cards[i], this, i);
            }
        }
        internal void PlayCard(int index, GameObject Target)
        {
            Card selectedCard = cards[index];
            selectedCard.Target = Target;
            selectedCard.Play();
        }
    }
}