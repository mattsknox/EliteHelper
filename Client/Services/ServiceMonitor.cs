using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHelper.Services
{
    public static class ServiceMonitor
    {
        static Dictionary<string, List<IObserver<ServiceOperation>>> Observers;
        static List<ServiceOperation> ServiceOperations
            = new List<ServiceOperation>();

        static void UpdateOperationStatus(ServiceStatus status, string operationName)
        {
            ServiceOperation targetOp = null;
            foreach(var operation in ServiceOperations)
            {
                if (operation.Name == operationName)
                {
                    operation.Status = status;
                    operation.UpdatedAt = DateTime.Now;
                    targetOp = operation;
                    break;
                }
                if (targetOp != null
                    && Observers.ContainsKey(operationName))
                {
                    foreach (var observer in Observers[operationName])
                    {
                        observer.OnNext(targetOp);
                    }
                }
            }
        }

        #region Observable
        public static IDisposable Subscribe(string operationName, IObserver<ServiceOperation> observer)
        {
            if (Observers.ContainsKey(operationName) 
                && !Observers[operationName].Contains(observer))
            {
                Observers[operationName].Add(observer);
            }

            return new Unsubscriber(Observers[operationName], observer);
        }

        class Unsubscriber : IDisposable
        {
            List<IObserver<ServiceOperation>> observers;
            IObserver<ServiceOperation> observer;
            public Unsubscriber(List<IObserver<ServiceOperation>> observers,
                IObserver<ServiceOperation> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observer != null
                    && observers.Contains(observer))
                    observers.Remove(observer);
            }
        }
        #endregion
    }
}
