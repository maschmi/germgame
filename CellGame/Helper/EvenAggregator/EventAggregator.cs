using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CellGame.Helper
{
    internal sealed class EventAggregator : IEventAggregator
    {
        private ConcurrentDictionary<Type, List<WeakReference<object>>> _register = 
            new ConcurrentDictionary<Type, List<WeakReference<object>>>();

        public async Task Publish<TEvent>(TEvent message) where TEvent : IMessage
        {
            if(_register.TryGetValue(message.GetType(), out List<WeakReference<object>> invocationList))
                foreach (var listener in invocationList)
                {
                    if (listener.TryGetTarget(out object handler))
                    {
                        if(handler is IListener<TEvent> concreteHandler)
                            await concreteHandler.ProcessMessageAsync(message);
                    }
                    else
                        invocationList.Remove(listener);
                }
        }

        public void Subscribe<TEvent>(IListener<TEvent> listener) where TEvent : IMessage
        {
            var listenerRef = new WeakReference<object>(listener);

            List<WeakReference<object>> AddFunction(Type arg) =>
                new List<WeakReference<object>> { listenerRef };
            
            List<WeakReference<object>> UpdateFunction(Type arg1, List<WeakReference<object>> arg2)
            {
                arg2.Add(listenerRef);
                return arg2;
            } 

            _register.AddOrUpdate(typeof(TEvent), AddFunction, UpdateFunction);
        }
    }
}
