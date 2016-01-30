using UnityEngine;
using System.Collections;

namespace GGJ2016
{
    public class PigeonMover : MonoBehaviour
    {
        private const float turnSpeed = 1000f;

        private PigeonAnimatorDriver _animatorDriver;

        private Animator FindAnimator()
        {
            Animator animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            if (animator == null)
            {
                Debug.LogError("Could not find animator for: " + name);
            }

            return animator;
        }

        private void Start()
        {
            _animatorDriver = new PigeonAnimatorDriver(FindAnimator());
            DirectionVector = transform.forward;
        }

        private void Update()
        {
            Rotate(DirectionVector);
        }

        private Vector3 MoveVector;
        private Vector3 DirectionVector;
        private Vector3 _velocity;

        public void Move(Vector2 value)
        {
            float magnitude = value.magnitude;
            if (magnitude < 0.3)
            {
                DirectionVector = transform.forward;
            }
            else
            {
                Vector3 worldVector = (new Vector3(value.x, 0, value.y));
                worldVector = Camera.main.transform.TransformDirection(worldVector);
                worldVector = new Vector3(worldVector.x, 0, worldVector.z);
                DirectionVector = worldVector.normalized * magnitude;
            }
        }

        private void Rotate(Vector3 dir)
        {
            if (dir.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir),
                    turnSpeed * Time.deltaTime);
            }
        }
    }
}