using UnityEngine;
using System.Collections;

namespace Billygoat.Extensions
{
    public static class RectTransformExtensions
    {
        public static Rect GetScreenRect(this RectTransform rectTransform)
        {
            Vector3[] corners = { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
            rectTransform.GetWorldCorners(corners);

            float xMin = float.PositiveInfinity, xMax = float.NegativeInfinity, yMin = float.PositiveInfinity, yMax = float.NegativeInfinity;
            for (int i = 0; i < 4; ++i)
            {
                Vector3 screenCoord = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
                if (screenCoord.x < xMin) xMin = screenCoord.x;
                if (screenCoord.x > xMax) xMax = screenCoord.x;
                if (screenCoord.y < yMin) yMin = screenCoord.y;
                if (screenCoord.y > yMax) yMax = screenCoord.y;
                corners[i] = screenCoord;
            }
            Rect result = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
            return result;
        }

        public static void ZeroOutLocals(this RectTransform rectTransform)
        {
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localRotation = Vector3.zero.ToQuaternion();
            rectTransform.localScale = Vector3.one;
        }
    }
}