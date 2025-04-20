using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.Cards.Factory
{
    public class TeacherCardFactory : IFactory
    {
        private static List<Type> attackCardTypes = new List<Type>
    {
        typeof(ZeroToleranceCard),
        typeof(PopQuizCard)
    };
        public ICard CreateCard()
        {
            int index = UnityEngine.Random.Range(0, attackCardTypes.Count);
            return (Card)Activator.CreateInstance(attackCardTypes[index]);
        }
    }
    public class PopQuizCard : Card, IDamage, IDefense
    {
        private int defense = 5;
        private int damage = 3;
        public int Damage => damage;
        public int Defense => defense;
        public PopQuizCard()
        {
            Name = "Pop Quiz";
            Description = $"You should’ve done the reading\nReduces player's defense by {defense}. If defense is 0, take {damage} “stress” damage.";
        }
        public override void Play()
        {
            if (Target != null)
            {
                if(Target.GetComponent<CharacterStats>().def > 0) Target.GetComponent<CharacterStats>().DecreaseDefense(defense);
                else
                {
                    Target.GetComponent<CharacterStats>().TakeDamage(damage);
                }
            }
            else
            {
                Debug.Log("No target selected for Pop Quiz!");
            }
        }
    }
    public class ZeroToleranceCard : Card, IDamage
    {
        int damage = 10;
        public int Damage => damage;
        public ZeroToleranceCard()
        {
            Name = "Zero Tolerance";
            Description = $"Teacher doesn't liek your answer by any means\nTake {damage} damage";
        }

        public override void Play()
        {
            if (Target != null)
            {
                Target.GetComponent<CharacterStats>().TakeDamage(damage);
            }
            else
            {
                Debug.Log($"No target selected for {Name}!");
            }
        }
    }
}