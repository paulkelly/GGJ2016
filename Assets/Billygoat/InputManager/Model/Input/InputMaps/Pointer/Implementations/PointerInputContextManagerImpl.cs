using System.Collections.Generic;
using System.Diagnostics;

namespace Billygoat.InputManager
{
    public class PointerInputContextManagerImpl<T> : IPointerInputContextManager<T>
    {
        private readonly List<IPointerInputContext<T>> ContextList = new List<IPointerInputContext<T>>();
        private bool needsSort;

        public void Add(IPointerInputContext<T> context)
        {
            ContextList.Add(context);
            Sort();
        }

        public void Remove(IPointerInputContext<T> context)
        {
            ContextList.Remove(context);
        }

        public void Update()
        {
            DoSort();
            foreach (var context in ContextList)
            {
                context.Update();
            }
        }

        public void LateUpdate()
        {
            foreach (var context in ContextList)
            {
                context.LateUpdate();
            }
        }

        public void Sort()
        {
            needsSort = true;
        }

        public void DoSort()
        {
            if (needsSort)
            {
                needsSort = false;
                ContextList.Sort();
            }
        }

        public void HandleInput(int id, IControl buttonInput)
        {
            foreach (var context in ContextList)
            {
                if (context.HandleInput(id, buttonInput))
                {
                    break;
                }
            }
            DoSort();
        }

        public void HandleInput(int id, ITwoAxisControl twoAxisInput)
        {
            foreach (var context in ContextList)
            {
                if (context.HandleInput(id, twoAxisInput))
                {
                    break;
                }
            }
            DoSort();
        }

        public void HandlePointerClick(T pointerData)
        {
            foreach (var context in ContextList)
            {
                if (context.HandlePointerClick(pointerData))
                {
                    break;
                }
            }

            DoSort();
        }

        public void HandlePointerDown(T pointerData)
        {
            foreach (var context in ContextList)
            {
                if (context.HandlePointerDown(pointerData))
                {
                    break;
                }
            }

            DoSort();
        }

        public void HandlePointerUp(T pointerData)
        {
            foreach (var context in ContextList)
            {
                if (context.HandlePointerUp(pointerData))
                {
                    break;
                }
            }

            DoSort();
        }

        public void HandlePointerEnter(T pointerData)
        {
            foreach (var context in ContextList)
            {
                if (context.HandlePointerEnter(pointerData))
                {
                    break;
                }
            }

            DoSort();
        }

        public void HandlePointerLeave(T pointerData)
        {
            foreach (var context in ContextList)
            {
                if (context.HandlePointerLeave(pointerData))
                {
                    break;
                }
            }

            DoSort();
        }

        public void HandlePointerOver(T pointerData)
        {
            foreach (var context in ContextList)
            {
                if (context.HandlePointerOver(pointerData))
                {
                    break;
                }
            }

            DoSort();
        }

        public void HandlePointerMove(T pointerData)
        {
            foreach (var context in ContextList)
            {
                if (context.HandlePointerMove(pointerData))
                {
                    break;
                }
            }

            DoSort();
        }
    }
}
