using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.Cards.Factory
{
	public class DefenseCardFactory : IFactory
	{
        private static List<Type> defenseCardTypes = new List<Type>
    {
        typeof(AbstractAnswer),
        typeof(TheOnlyRightAnswer)
    };
        public ICard CreateCard()
        {
            int index = UnityEngine.Random.Range(0, defenseCardTypes.Count);
            return (Card)Activator.CreateInstance(defenseCardTypes[index]);
        }
    }
    public class AbstractAnswer : Card, IDefense
    {
        private int defense = 1;
        public int Defense => defense;
        public AbstractAnswer()
        {
            Name = "Abstract Answer";
            Description = $"Answer in the most absract way possible.\n +{defense} defense";
        }
        public override void Play()
        {
            if (Target != null)
            {
                Target.GetComponent<CharacterStats>().AddDefense(defense);
            }
            else
            {
                Debug.Log("No target selected for Abstract Answer!");
            }
        }
    }
    public class TheOnlyRightAnswer : Card, IDefense
    {
        private int defense = 5;
        public int Defense => defense;
        public TheOnlyRightAnswer()
        {
            Name = "The Only Right Answer";
            Description = $"Remember correct answer for the asked question.\n +{defense} defense";
        }

        public override void Play()
        {
            if (Target != null)
            {
                Target.GetComponent<CharacterStats>().AddDefense(defense);
            }
            else
            {
                Debug.Log("No target selected for The Only Right Answer!");
            }
        }
    }
}
