public interface IPointerInputMap<T>
{
    bool HandlePointerClick(T pointerData);

    bool HandlePointerDown(T pointerData);

    bool HandlePointerUp(T pointerData);

    bool HandlePointerEnter(T pointerData);

    bool HandlePointerLeave(T pointerData);
}
