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

        private Stack<ICommand> undoStack = new();
        private Stack<ICommand> redoStack = new();

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
        internal void PlayCard(ICommand command)
        {
            if (redoStack.Count > 0) redoStack.Clear();

            undoStack.Push(command);

            command.Execute();
        }
        public void UnPlayCard()
        {
            if (undoStack.Count <= 0) return;

            ICommand command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
        }

        public void RedoPlayCard()
        {
            if (redoStack.Count <= 0) return;

            ICommand command = redoStack.Pop();
            command.Execute();
            undoStack.Push(command);
        }
        internal void DisableCards(List<ICard> cardsToDisable)
        {
            cardUIRenderer.DisableDefenseCards(cardsToDisable);
        }
    }
}