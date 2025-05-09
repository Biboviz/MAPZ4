using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Cards.LimitingObservers
{
    interface IObserver
    {
        void Update(ISubject subject);
    }
    interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
    public class Subject : ISubject
    {


        public void Notify()
        {
            throw new NotImplementedException();
        }

        void ISubject.Attach(IObserver observer)
        {
            throw new NotImplementedException();
        }

        void ISubject.Detach(IObserver observer)
        {
            throw new NotImplementedException();
        }
    }
}
