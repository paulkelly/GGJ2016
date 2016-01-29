using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public abstract class BasePointerInputContext<T> : IPointerInputContext<T>, IComparable
    {
        [Inject]
        public IPointerInputContextManager<T> contextManager { get; set; }

        [PostConstruct]
        public void SetupWithContext()
        {
            ConstructMap();

            contextManager.Add(this);
        }

        private int _priority;
        public int Priority
        {
            get { return _priority; }

            protected set
            {
                _priority = value;
                contextManager.Sort();
            }
        }

        public int DefaultPriority
        {
            get;

            protected set;
        }

        protected virtual void ConstructMap()
        {
        }

        public abstract void Update();

        public abstract void LateUpdate();

        public abstract bool HandleInput(int id, IControl button);

        public abstract bool HandleInput(int id, ITwoAxisControl twoAxis);

        public abstract bool HandlePointerClick(T pointerData);

        public abstract bool HandlePointerDown(T pointerData);

        public abstract bool HandlePointerUp(T pointerData);

        public abstract bool HandlePointerEnter(T pointerData);

        public abstract bool HandlePointerLeave(T pointerData);

        public abstract bool HandlePointerOver(T pointerData);

        public abstract bool HandlePointerMove(T pointerData);

        public int CompareTo(System.Object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException("Trying to compare a null reference");
            }

            IPointerInputContext<T> otherContext = obj as IPointerInputContext<T>;
            if (otherContext != null)
            {
                return Priority - otherContext.Priority;
            }
            else
            {
                throw new ArgumentException("Object is not an IPointerInputContext");
            }
        }
    }
}