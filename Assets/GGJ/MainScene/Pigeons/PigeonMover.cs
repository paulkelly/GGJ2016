using UnityEngine;
using System.Collections;
using Billygoat.MultiplayerInput;
using strange.extensions.mediation.impl;

namespace GGJ2016
{
    public class PigeonMover : View
    {
        [Inject]
        public PigeonScorer Scorer { get; set; }

        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        private const float turnSpeed = 600f;

        private PigeonAnimatorDriver _animatorDriver;
        private FemaleCollector _collector;

        private PigeonIKTarget _lookTarget;

        public PlayerData Player
        {
            get
            {
                return GetComponent<PigeonController>().Player;
            }
        }

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
            _lookTarget = GetComponentInChildren<PigeonIKTarget>(true);

            DirectionVector = transform.forward;

            ThePigeonSignals.PigeonSpawned.Dispatch(transform);
        }

        private float Speed;
        private float TargetSpeed;
        private float Acc;

        private const float SpeedMulti = 1;
        private const float AccTime = 0.2f;

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void LosePoints(float value)
        {
            Scorer.LoseScore(value);
        }

        public bool CanWin()
        {
            return Scorer.Score > 80;
        }

        private float NextTargetControl = 1;
        private float TargetControl;
        private float ControlV;

        private void Update()
        {
            if (Swooping)
            {
                Scorer.AddScore(5 * Time.deltaTime * _collector.GetScoreMulti());
            }
            else
            {
                Scorer.AddScore(Time.deltaTime * _collector.GetScoreMulti());
            }

            if (Swooping)
            {
                TargetControl = 0.3f;
            }
            else
            {
                TargetControl = NextTargetControl;
            }

            Control = Mathf.SmoothDamp(Control, TargetControl, ref ControlV, 0.2f);

            if (distractionTime > endDistractionTim)
            {
                distractionTime = 0;
                endDistractionTim = Random.Range(minDistractionTime, maxDistractionTime);

                PickDistractedDirection();
            }
            else
            {
                distractionTime += Time.deltaTime;
            }

            _animatorDriver.Swooping = Swooping;

            Scorer.Update();

            Rotate(DirectionVector);

            //Speed = Mathf.SmoothDamp(Speed, TargetSpeed * SpeedMulti, ref Acc, AccTime);
            _animatorDriver.Speed = Mathf.SmoothDamp(_animatorDriver.Speed, TargetSpeed * SpeedMulti, ref Acc, AccTime);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        private bool Distrated = false;
        private Vector3 DistrationVector;

        public float Control = 1;

        private float distractionTime = 0;
        private float endDistractionTim = 0;

        float minDistractionTime = 0.2f;
        float maxDistractionTime = 0.4f;
        private void PickDistractedDirection()
        {

            NextTargetControl = Random.Range(0.7f, 1f);

            if (_lookTarget != null)
            {
                //DistrationVector = transform.position - _lookTarget.position;

                DistrationVector = ConvertToWorldVector(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized);
            }
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

            if (Swooping)
            {
                DirectionVector = transform.forward;
                TargetSpeed = 2;
                return;
            }

                TargetSpeed = value.magnitude;
            if (TargetSpeed < 0.3)
            {
                DirectionVector = transform.forward;
                TargetSpeed = 0;
            }
            else
            {
                DirectionVector = ConvertToWorldVector(value);
            }
        }

        private Vector3 ConvertToWorldVector(Vector3 input)
        {
            Vector3 worldVector = (new Vector3(input.x, 0, input.y));
            worldVector = Camera.main.transform.TransformDirection(worldVector);
            worldVector = new Vector3(worldVector.x, 0, worldVector.z);
            return worldVector.normalized * TargetSpeed;
        }

        private void Rotate(Vector3 dir)
        {
            dir = Vector3.Lerp(DistrationVector, dir, Control);

            //float TurnSpeedMulti = 1 / Mathf.Max(_animatorDriver.Speed, 1);
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir));
            float TurnSpeedMulti = Mathf.Max(0.3f, 1 * (angle / 180));
            if (dir.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir),
                    dir.magnitude * turnSpeed * Time.deltaTime * TurnSpeedMulti);
            }
        }

        public void PuffUp()
        {
            Scorer.AddPuffyness();
        }

        private bool Swooping = false;
        public void OnSwoopDown()
        {
            Swooping = true;
        }

        public void OnSwoopUp()
        {
            Swooping = false;
        }

        public void Caw()
        {

        }

        public void Wings()
        {

        }
    }
}