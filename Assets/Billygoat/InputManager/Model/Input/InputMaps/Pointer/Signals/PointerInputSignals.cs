using strange.extensions.signal.impl;

public class PointerInputSignals<T>
{
    public Signal<T> PointerClickInput = new Signal<T>();

    public Signal<T> PointerDownInput = new Signal<T>();

    public Signal<T> PointerUpInput = new Signal<T>();

    public Signal<T> PointerEnterInput = new Signal<T>();

    public Signal<T> PointerLeaveInput = new Signal<T>();

    public Signal<T> PointerOverInput = new Signal<T>();

    public Signal<T> PointerMoveInput = new Signal<T>();
}
