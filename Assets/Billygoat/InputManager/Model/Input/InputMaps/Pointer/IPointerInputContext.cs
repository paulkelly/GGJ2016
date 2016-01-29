namespace Billygoat.InputManager
{
    public interface IPointerInputContext<T>
    {
        int Priority { get; }

        void Update();

        void LateUpdate();

        bool HandleInput(int id, IControl buttonInput);

        bool HandleInput(int id, ITwoAxisControl twoAxisInput);

        bool HandlePointerClick(T pointerData);

        bool HandlePointerDown(T pointerData);

        bool HandlePointerUp(T pointerData);

        bool HandlePointerEnter(T pointerData);

        bool HandlePointerLeave(T pointerData);

        bool HandlePointerOver(T pointerData);

        bool HandlePointerMove(T pointerData);
    }
}