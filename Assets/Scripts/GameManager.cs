using UnityEngine;
using System.Collections;
using Assets.Scripts.Cards.Factory;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using System;
using static UnityEngine.EventSystems.EventTrigger;


namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int MaxCardsInHand = 5;
        [SerializeField] GameObject Player;
        [SerializeField] GameObject Enemy;
        [SerializeField] GameObject CardButtonPrefab;
        [SerializeField] Transform CardPanel;
        IFactory attackCardFactory;
        List<Card> cards;

        void Start()
        {
            attackCardFactory = new AttackCardFactory();
            cards = new List<Card>();
            for (int i = 0; i < MaxCardsInHand; i++)
            {
                Card card = (Card)attackCardFactory.CreateCard();
                cards.Add(card);
                card.Target = Player;
                // Instantiate button and assign data
                GameObject buttonGO = Instantiate(CardButtonPrefab, CardPanel);
                CardButtonUI buttonUI = buttonGO.GetComponent<CardButtonUI>();
                buttonUI.Init(card, this, i);
            }
        }
        internal void PlayCard(int index, bool Target)
        {
            Card selectedCard = cards[index];
            selectedCard.Target = Target ? Enemy : Player;
            selectedCard.Play();
        }
    }
}