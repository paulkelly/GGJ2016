using UnityEngine;
using System.Collections;
using System;

public class PointerAction<T> : IPointerAction, ICloneable where T : PointerEventArgs
{
    public Action<T> OnClick = (args) => { };
    public Action<T> OnDown = (args) => { };
    public Action<T> OnUp = (args) => { };
    public Action<T> OnEnter = (args) => { };
    public Action<T> OnLeave = (args) => { };
    public Action<T> OnOver = (args) => { };
    public Action<MoveEventArgs> OnMove = (args) => { };

    public void HandlePointerClick(PointerEventArgs pointerData)
    {
        OnClick(pointerData as T);
    }

    public void HandlePointerDown(PointerEventArgs pointerData)
    {
        OnDown(pointerData as T);
    }

    public void HandlePointerUp(PointerEventArgs pointerData)
    {
        OnUp(pointerData as T);
    }

    public void HandlePointerEnter(PointerEventArgs pointerData)
    {
        OnEnter(pointerData as T);
    }

    public void HandlePointerLeave(PointerEventArgs pointerData)
    {
        OnLeave(pointerData as T);
    }

    public void HandlePointerOver(PointerEventArgs pointerData)
    {
        OnOver(pointerData as T);
    }

    public void HandlePointerMove(PointerEventArgs pointerData)
    {
        OnMove(pointerData as MoveEventArgs);
    }

    public object Clone()
    {
        PointerAction<T> clone = new PointerAction<T>();

        clone.OnClick = OnClick;
        clone.OnDown = OnDown;
        clone.OnUp = OnUp;
        clone.OnEnter = OnEnter;
        clone.OnLeave = OnLeave;
        clone.OnOver = OnOver;
        clone.OnMove = OnMove;

        return clone;
    }
}
