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

        private Rigidbody _rigidbody;

        private Animator _animator;
        private Animator FindAnimator()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                _animator = GetComponentInChildren<Animator>();
            }
            if (_animator == null)
            {
                Debug.LogError("Could not find animator for: " + name);
            }

            return _animator;
        }

        protected override void OnStart()
        {
            _animatorDriver = new PigeonAnimatorDriver(FindAnimator());
            _collector = GetComponentInChildren<FemaleCollector>(true);
            _rigidbody = GetComponent<Rigidbody>();

            DirectionVector = transform.forward;

            ThePigeonSignals.PigeonSpawned.Dispatch(transform);
        }

        private float Speed;
        private float TargetSpeed;
        private float Acc;

        private const float SpeedMulti = 1;
        private const float AccTime = 0.2f;

        private void Update()
        {
            Scorer.Update();

            Rotate(DirectionVector);

            //Speed = Mathf.SmoothDamp(Speed, TargetSpeed * SpeedMulti, ref Acc, AccTime);
            _animatorDriver.Speed = Mathf.SmoothDamp(_animatorDriver.Speed, TargetSpeed * SpeedMulti, ref Acc, AccTime);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        private void OnAnimatorMove()
        {
            if (_animator != null)
            {
                Vector3 newPos = _animator.rootPosition;
                _rigidbody.MovePosition(newPos);

                _animator.rootPosition = _rigidbody.position;
            }
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
            Scorer.AddScore(2 * _collector.GetScoreMulti());
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