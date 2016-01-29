namespace Billygoat.InputManager
{
    public interface IPointerInputContextManager<T>
    {
        void Add(IPointerInputContext<T> context);

        void Remove(IPointerInputContext<T> context);

        void Update();

        void LateUpdate();

        void Sort();

        void DoSort();

        void HandleInput(int id, IControl buttonInput);

        void HandleInput(int id, ITwoAxisControl twoAxisInput);

        void HandlePointerClick(T pointerData);

        void HandlePointerDown(T pointerData);

        void HandlePointerUp(T pointerData);

        void HandlePointerEnter(T pointerData);

        void HandlePointerLeave(T pointerData);

        void HandlePointerOver(T pointerData);

        void HandlePointerMove(T pointerData);
    }
}