using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class Event<T>
    {
        private List<Action<T>> actions = new List<Action<T>>();

        public void InvokeEvent(T item)
        {
            actions.ForEach(act => act.Invoke(item));
        }
        
        public void Subscribe(Action<T> action)
        {
            actions.Add(action);
        }

        public void UnSubscribe(Action<T> action)
        {
            actions.Remove(action);
        }
        
    }

    public class Event<T, TT>
    {
        private List<Action<T, TT>> actions = new List<Action<T,TT>>();

        public void InvokeEvent(T item1, TT item2)
        {
            actions.ForEach(act => act.Invoke(item1,item2));
        }
        
        public void Subscribe(Action<T,TT> action)
        {
            actions.Add(action);
        }
    }
    
    public class Event<T, TT, TTt>
    {
        private List<Action<T, TT, TTt>> actions = new List<Action<T,TT,TTt>>();

        public void InvokeEvent(T item1, TT item2, TTt item3)
        {
            actions.ForEach(act => act.Invoke(item1,item2, item3));
        }
        
        public void Subscribe(Action<T,TT,TTt> action)
        {
            actions.Add(action);
        }
    }
    
}