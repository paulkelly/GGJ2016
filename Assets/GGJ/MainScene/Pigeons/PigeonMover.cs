using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace GGJ2016
{
    public class PigeonMover : View
    {
        [Inject]
        public PigeonScorer Scorer { get; set; }

        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        private const float turnSpeed = 1000f;

        private PigeonAnimatorDriver _animatorDriver;
        private FemaleCollector _collector;

        [PostConstruct]
        public void OnConstruct()
        {
            Scorer.Male = transform;
        }

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

        protected override void OnStart()
        {
            _animatorDriver = new PigeonAnimatorDriver(FindAnimator());
            _collector = GetComponentInChildren<FemaleCollector>(true);

            DirectionVector = transform.forward;

            ThePigeonSignals.PigeonSpawned.Dispatch(transform);
        }

        private float Speed;
        private float TargetSpeed;
        private float Acc;

        private const float SpeedMulti = 10;
        private const float AccTime = 0.2f;

        private void Update()
        {
            Scorer.Update();

            Rotate(DirectionVector);

            Speed = Mathf.SmoothDamp(Speed, TargetSpeed * SpeedMulti, ref Acc, AccTime);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        

        private Vector3 MoveVector;
        private Vector3 DirectionVector;
        private Vector3 _velocity;

        public void Move(Vector2 value)
        {
            TargetSpeed = value.magnitude;
            if (TargetSpeed < 0.3)
            {
                DirectionVector = transform.forward;
                TargetSpeed = 0;
            }
            else
            {
                Vector3 worldVector = (new Vector3(value.x, 0, value.y));
                worldVector = Camera.main.transform.TransformDirection(worldVector);
                worldVector = new Vector3(worldVector.x, 0, worldVector.z);
                DirectionVector = worldVector.normalized * TargetSpeed;
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

        public void PuffUp()
        {
            Scorer.AddPuffyness();
        }

        public void OnSwoopDown()
        {

        }

        public void OnSwoopUp()
        {

        }

        public void Caw()
        {

        }

        public void Wings()
        {

        }
    }
}