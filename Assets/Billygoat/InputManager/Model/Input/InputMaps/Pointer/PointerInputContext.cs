namespace Billygoat.InputManager
{
    public abstract class PointerInputContext<T> : BasePointerInputContext<T> where T : PointerEventArgs
    {
        protected InputMap buttonInputMap = new InputMap();

        public InputMap ButtonInputMap
        {
            get { return buttonInputMap; }
        }

        protected PointerInputMap<T> pointerInputMap = new PointerInputMap<T>();

        public PointerInputMap<T> PointerInputMap
        {
            get { return pointerInputMap; }
        }

        public override void Update()
        {
            buttonInputMap.Update();
        }

        public override void LateUpdate()
        {
            buttonInputMap.LateUpdate();
        }

        public override bool HandleInput(int id, IControl button)
        {
            return buttonInputMap.HandleButtonInput(id, button);
        }

        public override bool HandleInput(int id, ITwoAxisControl twoAxis)
        {
            return buttonInputMap.HandleTwoAxisInput(id, twoAxis);
        }

        public override bool HandlePointerClick(T pointerData)
        {
            return pointerInputMap.HandlePointerClick(pointerData);
        }

        public override bool HandlePointerDown(T pointerData)
        {
            return pointerInputMap.HandlePointerDown(pointerData);
        }

        public override bool HandlePointerUp(T pointerData)
        {
            return pointerInputMap.HandlePointerUp(pointerData);
        }

        public override bool HandlePointerEnter(T pointerData)
        {
            return pointerInputMap.HandlePointerEnter(pointerData);
        }

        public override bool HandlePointerLeave(T pointerData)
        {
            return pointerInputMap.HandlePointerLeave(pointerData);
        }

        public override bool HandlePointerOver(T pointerData)
        {
            return pointerInputMap.HandlePointerOver(pointerData);
        }

        public override bool HandlePointerMove(T pointerData)
        {
            return pointerInputMap.HandlePointerMove(pointerData);
        }
    }
}
