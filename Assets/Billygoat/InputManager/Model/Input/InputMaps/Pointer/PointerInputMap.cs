using System.Collections.Generic;
using System.Diagnostics;

namespace Billygoat.InputManager
{
    public class PointerInputMap<T> : IPointerInputMap<T> where T : PointerEventArgs
    {
        private readonly Dictionary<System.Type, IPointerAction> PointerInputs =
            new Dictionary<System.Type, IPointerAction>();

        private IPointerAction DefaultAction;

        public void AddAction<U>(IPointerAction action) where U : T
        {
           // IPointerAction<T> theAction = action as IPointerAction<T>;
            if (action == null)
            {
                action = new PointerAction<T>();
            }
            PointerInputs.Add(typeof(U), action);
        }

        public void SetDefaultAction(IPointerAction action)
        {
            if (action == null)
            {
                action = new PointerAction<T>();
            }

            DefaultAction = action;
        }

        public bool HandlePointerClick(T pointerData)
        {
            if (PointerInputs.ContainsKey(pointerData.GetType()))
            {
                IPointerAction action;
                PointerInputs.TryGetValue(pointerData.GetType(), out action);
                if (action != null) action.HandlePointerClick(pointerData);
            }
            else if (DefaultAction != null)
            {
                DefaultAction.HandlePointerClick(pointerData);
            }

            return pointerData.handled;
        }

        public bool HandlePointerDown(T pointerData)
        {
            if (PointerInputs.ContainsKey(pointerData.GetType()))
            {
                IPointerAction action;
                PointerInputs.TryGetValue(pointerData.GetType(), out action);
                if (action != null) action.HandlePointerDown(pointerData);
            }
            else if (DefaultAction != null)
            {
                DefaultAction.HandlePointerDown(pointerData);
            }

            return pointerData.handled;
        }

        public bool HandlePointerUp(T pointerData)
        {
            if (PointerInputs.ContainsKey(pointerData.GetType()))
            {
                IPointerAction action;
                PointerInputs.TryGetValue(pointerData.GetType(), out action);
                if (action != null) action.HandlePointerUp(pointerData);
            }
            else if (DefaultAction != null)
            {
                DefaultAction.HandlePointerUp(pointerData);
            }

            return pointerData.handled;
        }

        public bool HandlePointerEnter(T pointerData)
        {
            if (PointerInputs.ContainsKey(pointerData.GetType()))
            {
                IPointerAction action;
                PointerInputs.TryGetValue(pointerData.GetType(), out action);
                if (action != null) action.HandlePointerEnter(pointerData);
            }
            else if (DefaultAction != null)
            {
                DefaultAction.HandlePointerEnter(pointerData);
            }

            return pointerData.handled;
        }

        public bool HandlePointerLeave(T pointerData)
        {
            if (PointerInputs.ContainsKey(pointerData.GetType()))
            {
                IPointerAction action;
                PointerInputs.TryGetValue(pointerData.GetType(), out action);
                if (action != null) action.HandlePointerLeave(pointerData);
            } 
            else if (DefaultAction != null)
            {
                DefaultAction.HandlePointerLeave(pointerData);
            }

            return pointerData.handled;
        }

        public bool HandlePointerOver(T pointerData)
        {
            if (PointerInputs.ContainsKey(pointerData.GetType()))
            {
                IPointerAction action;
                PointerInputs.TryGetValue(pointerData.GetType(), out action);
                if (action != null) action.HandlePointerOver(pointerData);
            }
            else if (DefaultAction != null)
            {
                DefaultAction.HandlePointerOver(pointerData);
            }

            return pointerData.handled;
        }

        public bool HandlePointerMove(T pointerData)
        {
            if (PointerInputs.ContainsKey(pointerData.GetType()))
            {
                IPointerAction action;
                PointerInputs.TryGetValue(pointerData.GetType(), out action);
                if (action != null) action.HandlePointerMove(pointerData);
            }
            else if (DefaultAction != null)
            {
                DefaultAction.HandlePointerMove(pointerData);
            }

            return pointerData.handled;
        }
    }
}
