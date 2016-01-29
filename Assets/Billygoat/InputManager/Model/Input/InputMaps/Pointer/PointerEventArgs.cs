using Billygoat.Extensions;
using UnityEngine;
using Billygoat.InputManager;
using UnityEngine.EventSystems;

public class PointerEventArgs
{
    public int Id;
    public int ClickNumber;
    public MouseButton MouseButton;
    public Vector3 Position;
    public Vector3 ScreenPosition;

    public bool handled = false;

    public PointerEventArgs()
    {
    }

    public PointerEventArgs(int id)
    {
        this.Id = id;
    }

    public PointerEventArgs(int id, Vector3 position)
        : this(id)
    {
        this.Position = position;
        this.ScreenPosition = position;
    }

    public PointerEventArgs(int id, Vector3 position, Vector3 screenPosition) : this(id)
    {
        this.Position = position;
        this.ScreenPosition = screenPosition;
    }

    public PointerEventArgs(MouseButton mouseButton, Vector3 position, Vector3 screenPosition)
        : this(0)
    {
        this.MouseButton = mouseButton;
        this.Position = position;
        this.ScreenPosition = screenPosition;
    }

    public PointerEventArgs(Vector3 position, Vector3 screenPosition)
        : this(0, position, screenPosition)
    {
    }

    public PointerEventArgs(Vector3 position)
        : this(0, position)
    {
    }

    public PointerEventArgs(PointerEventArgs e)
    {
        Id = e.Id;
        ClickNumber = e.ClickNumber;
        MouseButton = e.MouseButton;
        Position = e.Position;
    }

    public bool GetMouseButton(MouseButton button)
    {
        return MouseButton.HasFlag(button);
    }
}

public class MoveEventArgs : PointerEventArgs
{
    public Vector2 Movement;

    public MoveEventArgs(PointerEventArgs e, Vector2 movement) : base(e)
    {
        Movement = movement;
    }
}

public class RaycastHitPointerArgs : PointerEventArgs
{
    public RaycastHit HitInfo;

    public RaycastHitPointerArgs(int id, int clickNumber, RaycastHit hitInfo, Camera cam)
    {
        this.Id = id;
        this.ClickNumber = clickNumber;
        HitInfo = hitInfo;
        Position = HitInfo.point;
        this.ScreenPosition = cam.WorldToScreenPoint(Position);
    }

    public RaycastHitPointerArgs(MouseButton mouseButton, int clickNumber, RaycastHit hitInfo, Camera cam)
    {
        Id = 0;
        this.ClickNumber = clickNumber;
        this.MouseButton = mouseButton;
        HitInfo = hitInfo;
        Position = HitInfo.point;
        this.ScreenPosition = cam.WorldToScreenPoint(Position);
    }
}

public class TouchPointerArgs : PointerEventArgs
{
    public Touch touch;

    public TouchPointerArgs(Touch touch)
    {
        this.touch = touch;
        this.Id = touch.fingerId;
        Position = touch.position;
    }
}

public class UnityPointerEventArgs : PointerEventArgs
{
    public PointerEventData data;

    public UnityPointerEventArgs(PointerEventData data)
    {
        if (data != null)
        {
            Id = data.pointerId;
            Position = data.position;
            ClickNumber = data.clickCount;
            if (data.pointerId < 0)
            {
                if (data.button == PointerEventData.InputButton.Left)
				{
                    MouseButton = MouseButton.Left;
				}
                else if (data.button == PointerEventData.InputButton.Right)
				{
                    MouseButton = MouseButton.Right;
				}
                else if (data.button == PointerEventData.InputButton.Middle)
				{
                    MouseButton = MouseButton.Middle;
				}
            }
        }
    }
}