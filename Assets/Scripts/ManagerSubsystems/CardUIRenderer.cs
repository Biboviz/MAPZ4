using UnityEngine;
using Assets.Scripts.Cards;
using Assets.Scripts.Cards.Factory;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class CardUIRenderer : MonoBehaviour
{


    [SerializeField] GameObject CardButtonPrefab;
    [SerializeField] Transform PlayerCardsPanel;
    [SerializeField] Transform EnemyCardsPanel;


    internal void RenderPlayerCards(List<Card> playerCards)
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
    internal void RenderEnemyCards(List<Card> enemyCards)
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
    internal void DisableDefenseCards(List<ICard> cardsToDisable)
    {
        foreach (ICard card in cardsToDisable)
        {
            if (card is Card concreteCard)
            {
                concreteCard.Disable();
            }
        }
    }
}
