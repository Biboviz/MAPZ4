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
    public class RapidFireCard : Card, IDamage
    {
        int damage = 12;
        public int Damage => damage;
        public RapidFireCard()
        {
            Name = "Rapid Fire";
            Description = $"Speak so quickly, that opponent doesn't have time to react. Inflict {damage} damage";
        }
        public override void Play()
        {
            if(Target != null)
            {
                Target.GetComponent<CharacterStats>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("No target selected for Rapid Fire!");
            }
        }
    }
    public class TrickQuestionCard : Card, IDamage, IDefense
    {
        int defense = 3;
        int damage = 1;
        public int Damage => damage;
        public int Defense => defense;
        public TrickQuestionCard()
        {
            Name = "Trick Question";
            Description = $"Ask a misleading question. Opponent loses {defense} Defense and takes {damage} damage.";
        }

        public override void Play()
        {
            if (Target != null)
            {
                Target.GetComponent<CharacterStats>().DecreaseDefense(defense);
                Target.GetComponent<CharacterStats>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("No target selected for Trick Question!");
            }
        }
    }
}