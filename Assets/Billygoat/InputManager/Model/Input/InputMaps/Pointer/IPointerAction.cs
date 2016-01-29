using UnityEngine;
using System.Collections;

public interface IPointerAction
{
    void HandlePointerClick(PointerEventArgs pointerData);

    void HandlePointerDown(PointerEventArgs pointerData);

    void HandlePointerUp(PointerEventArgs pointerData);

    void HandlePointerEnter(PointerEventArgs pointerData);

    void HandlePointerLeave(PointerEventArgs pointerData);

    void HandlePointerOver(PointerEventArgs pointerData);

    void HandlePointerMove(PointerEventArgs pointerData);
}
