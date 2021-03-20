using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHelper.Services
{
    public static class ServiceMonitor
    {
        public static Dictionary<string, List<IObserver<ServiceOperation>>> Observers
            = new Dictionary<string, List<IObserver<ServiceOperation>>>();
        public static List<ServiceOperation> ServiceOperations
            = new List<ServiceOperation>();

        public static void UpdateOperationStatus(ServiceStatus status, string operationName)
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

        public static void UpdateOperationStatus(ServiceOperation operation)
        {
            UpdateOperationStatus(operation.Status, operation.Name);
        }

        public static void RegisterOperation(string operationName)
        {
            var targetOperation = GetTargetOperation(operationName);
            targetOperation.Status = ServiceStatus.Working;
            ServiceOperations.Add(targetOperation);
            UpdateOperationStatus(targetOperation);
        }

        public static void CompleteOperation(string operationName)
        {
            var targetOperation = GetTargetOperation(operationName);
            targetOperation.Status = ServiceStatus.Complete;
            UpdateOperationStatus(targetOperation);
        }

        public static void PropagateOperationMessage(string operationName, string message)
        {
            var targetOperation = GetTargetOperation(operationName);
            targetOperation.Message = message;
            UpdateOperationStatus(targetOperation);
        }
        public static void PropagateOperationError(string operationName, string message)
        {
            var targetOperation = GetTargetOperation(operationName);
            targetOperation.Status = ServiceStatus.Error;
            targetOperation.Message = message;
            UpdateOperationStatus(targetOperation);
        }

        static ServiceOperation GetTargetOperation(string operationName)
        {
            ServiceOperation targetOperation = null;
            foreach (var operation in ServiceOperations)
            {
                if (operation.Name == operationName)
                {
                    targetOperation = operation;
                    break;
                }
            }
            if (targetOperation == null)
            {
                targetOperation = new ServiceOperation();
                targetOperation.Name = operationName;
            }
            return targetOperation;
        }

        #region Observable
        public static IDisposable Subscribe(string operationName, IObserver<ServiceOperation> observer)
        {
            if (!Observers.ContainsKey(operationName))
            {
                Observers[operationName] = new List<IObserver<ServiceOperation>>();
            }

            if (!Observers[operationName].Contains(observer))
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
