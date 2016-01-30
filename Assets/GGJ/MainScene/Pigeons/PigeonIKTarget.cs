using UnityEngine;
using System.Collections;

public class PigeonIKTarget : MonoBehaviour
{
    public Transform Target;

    public Vector3 position;
    private Vector3 vel;
    private const float SmoothDampTime = 0.1f;

    void Awake()
    {
        position = transform.position;
    }

    void Update()
    {
        if (Target != null)
        {
            position = Vector3.SmoothDamp(position, Target.position, ref vel, SmoothDampTime);
        }
        transform.position = position;
    }

    void LateUpdate()
    {
        transform.position = position;
    }
}
