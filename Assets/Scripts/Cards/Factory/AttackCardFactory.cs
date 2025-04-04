using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.Cards.Factory
{
    public class AttackCardFactory : IFactory
    {
        private static List<Type> attackCardTypes = new List<Type>
    {
        typeof(TrickQuestionCard),
        typeof(RapidFireCard)
    };
        public ICard CreateCard()
        {
            int index = UnityEngine.Random.Range(0, attackCardTypes.Count);
            return (Card)Activator.CreateInstance(attackCardTypes[index]);
        }
    }
    public class RapidFireCard : Card
    {
        public RapidFireCard()
        {
            Name = "Rapid Fire";
            Description = "Answer quickly! Opponent gets no time to think.";
        }
        public override void Play()
        {
            if(Target != null)
            {
                Target.GetComponent<CharacterStats>().TakeDamage(200);
            }
            else
            {
                Debug.Log("No target selected for Rapid Fire!");
            }
        }
    }
    public class TrickQuestionCard : Card
    {
        public TrickQuestionCard()
        {
            Name = "Trick Question";
            Description = "Ask a misleading question. Opponent loses 5 Defense.";
        }

        public override void Play()
        {
            if (Target != null)
            {
                Target.GetComponent<CharacterStats>().TakeDamage(5);
            }
            else
            {
                Debug.Log("No target selected for Trick Question!");
            }
        }
    }
}