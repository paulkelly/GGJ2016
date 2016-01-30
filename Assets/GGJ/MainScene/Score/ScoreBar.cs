using UnityEngine;
using System.Collections;

public class ScoreBar : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetScore(float value)
    {
        if (rectTransform != null)
        {
            rectTransform.localScale = new Vector3(rectTransform.localScale.x, Mathf.Clamp(value / 100, 0, 1), rectTransform.localScale.z);
        }
    }
}
