using Assets.Scripts.Cards.LimitingObservers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entities
{
    class TeacherStats : CharacterStats, ITeacherSubject
    {
        List<ICard> cardsToDisable = new List<ICard>();
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            double currentHealthPercentage = (float)Health / MaxHealth;
            if (currentHealthPercentage < 0.5)
            {
                DisableDefenseCards();
            }
        }
        public void DisableDefenseCards()
        {
            foreach(Card card in cardsToDisable)
            {
                card.Disable();
            }
            GameManager.Instance.DisableCards(cardsToDisable);
        }

        public void Attach(ICard ObserveringDefenseCard)
        {
            cardsToDisable.Add(ObserveringDefenseCard);
        }
        public void Detach(ICard ObserveringDefenseCard)
        {
            cardsToDisable.Remove(ObserveringDefenseCard);
        }
    }
    public interface ITeacherSubject
    {
        // Attach an observer to the subject.
        void Attach(ICard ObserveringDefenseCard);

        // Detach an observer from the subject.
        void Detach(ICard ObserveringDefenseCard);

        // Notify all observers about an event.
        void DisableDefenseCards();
    }
}
