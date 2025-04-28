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
            private CardService cardService;
            private CardUIRenderer cardUIRenderer;

            public bool DevMode = true;

            private void Awake()
            {
                cardService = gameObject.GetComponent<CardService>();
                cardUIRenderer = gameObject.GetComponent<CardUIRenderer>();
                if (Instance != null && Instance != this)
                {
                    Destroy(gameObject);
                    return;
                }

                Instance = this;

            }
            public void Start()
            {
                cardService.CreatePlayerCards();
                //cardService.CreateEnemyCards();
                cardUIRenderer.RenderPlayerCards(cardService.PlayerCards);
                //cardUIRenderer.RenderEnemyCards(cardService.EnemyCards);
            }
            internal void PlayCard(int index)
            {
                Card selectedCard = cardService.PlayerCards[index];
                selectedCard.Play();
            }
        }
    }