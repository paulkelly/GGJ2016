using UnityEngine;
using System.Collections;
using System.Linq;

public class ClickCountUtil
{
    //For Touch a time of 1s is more appropriate
    private const float DefaultDoubleClickTime = 0.5f;
    private float _timeSinceLastClick = 0;

    private int _clickCount = 0;
    private int _lastMouseDown = -1;
    private int _nowMouseDown = -1;

    public int ClickCount
    {
        get
        {
            return _clickCount;
        }

        set
        {
            _clickCount = value;
        }
    }

    public float DoubleClickTime { get; set; }

    public ClickCountUtil()
        : this(DefaultDoubleClickTime)
    {
    }

    public ClickCountUtil(float doubleClickTime)
    {
        DoubleClickTime = doubleClickTime;
    }

    public void Update()
    {
        if (WasClick())
        {
            if (Input.GetMouseButtonDown(0))
            {
                _nowMouseDown = 0;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _nowMouseDown = 1;
            }
            else if (Input.GetMouseButtonDown(2))
            {
                _nowMouseDown = 2;
            }
            else if (Input.touchCount > 0)
            {
                _nowMouseDown = Input.touches.Count();
            }

            if (_clickCount > 0)
            {
                if (_nowMouseDown != _lastMouseDown)
                {
                    _clickCount = 0;
                }
            }

            _lastMouseDown = _nowMouseDown;
            _clickCount++;
            _timeSinceLastClick = 0;
        }
        else if (_clickCount > 0)
        {
            _timeSinceLastClick += Time.unscaledDeltaTime;
            if (_timeSinceLastClick > DoubleClickTime)
            {
                _timeSinceLastClick = 0;
                _clickCount = 0;
            }
        }
    }

    private bool WasClick()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) ||
               WasTouch();
    }

    private bool WasTouch()
    {
        if (Input.touchCount > 0)
        {
            foreach (var touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
