using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;

namespace Projecthandler.Abstract_classes_and_interfaces
{
    interface ICustomObservable
    {
        void NotifyObservers();
        void Subscribe(ICustomObserver observer);
        void UnSubcribe(ICustomObserver observer);
    }
}
