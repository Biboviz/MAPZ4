using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Cards
{
    public abstract class ModifiedCard : Card
    {
        protected Card _card;

        public ModifiedCard(Card card)
        {
            _card = card;
            Name = $"Boosted {_card.Name}";
            Description = $"Boosted version of {_card.Name}.";
            Target = card.Target;
            this._card = card;
        }

        public void SetCard(Card card)
        {
            this._card = card;
        }

        public override void Play()
        {
            if (this._card != null)
            {
                this._card.Play();
            }
        }
    }
    public class BoostedCard : ModifiedCard
    {
        public BoostedCard(Card card) : base(card)
        {
            Name = $"Boosted {_card.Name}";
            Description = $"Plays {_card.Name} twice!";
        }

        public override void Play()
        {
            if (_card != null && Target != null)
            {
                _card.Play();
                _card.Play();
            }
            else
            {
                Debug.Log("No target selected for Boosted Card!");
            }
        }
    }
    public class SlightlyBoostedCard : ModifiedCard
    {
        public SlightlyBoostedCard(Card card) : base(card)
        {
            Name = $"Slightly Boosted {_card.Name}";
            Description = $"Adds a small bonus to {_card.Name}.";
        }

        public override void Play()
        {
            if (_card != null && Target != null)
            {
                _card.Play();
                GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
                enemy.GetComponent<CharacterStats>()?.TakeDamage(3);

            }
            else
            {
                Debug.Log("No target selected for Slightly Boosted Card!");
            }
        }
    }

    public class CursedCard : ModifiedCard
    {
        public CursedCard(Card card) : base(card)
        {
            Name = $"Cursed {_card.Name}";
            Description = $"Backfires a bit when played by giving armor to teacher.";
        }
        public override void Play()
        {
            if (_card != null && Target != null)
            {
                _card.Play();

                GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
                enemy.GetComponent<CharacterStats>()?.AddDefense(2);
            }
            else
            {
                Debug.Log("No target selected for Cursed Card!");
            }
        }
    }

}