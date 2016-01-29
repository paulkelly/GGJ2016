using System.Collections.Generic;
using System.Diagnostics;

namespace Billygoat.InputManager
{
    public class InputContextManager : IContextManager
    {
        private readonly List<IInputContext> ContextList = new List<IInputContext>();
        private bool needsSort;

        public void Add(IInputContext context)
        {
            ContextList.Add(context);
            ContextList.Sort();
        }

        public void Remove(IInputContext context)
        {
            ContextList.Remove(context);
        }

        public void Update()
        {
            foreach (var context in ContextList)
            {
                //UnityEngine.Debug.Log(context.GetType().ToString() + ": " + context.Priority);
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

        public void DoSort()
        {
            if (needsSort)
            {
                needsSort = false;
                ContextList.Sort();
            }
        }
    }
}