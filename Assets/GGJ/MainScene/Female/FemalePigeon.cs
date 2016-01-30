﻿using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace GGJ2016
{
    public class FemalePigeon : View
    {

        private const float turnSpeed = 400f;

        private PigeonAnimatorDriver _animatorDriver;
        private FemaleCollector _collector;

        private PigeonIKTarget _lookTarget;

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

            Annoyed = 0;
        }

        private float Speed;
        private float TargetSpeed;
        private float Acc;

        private const float SpeedMulti = 1;
        private const float AccTime = 0.2f;


        private void OnCollisionStay(Collision collision)
        {
            if (collision.transform.tag != "Floor")
            {
                Vector3 contactPoint = new Vector3(collision.contacts[0].point.x, 0, collision.contacts[0].point.z);
                Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
                DirectionVector = (position - contactPoint).normalized;

                Annoyed = 1;
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        private void Update()
        {

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

            TargetSpeed = Mathf.Max(Random.RandomRange(0f, 1.2f), Annoyed*2);

            Annoyed = Mathf.Lerp(Annoyed, 0, Time.deltaTime);

            Rotate(DirectionVector);

            //Speed = Mathf.SmoothDamp(Speed, TargetSpeed * SpeedMulti, ref Acc, AccTime);
            _animatorDriver.Speed = Mathf.SmoothDamp(_animatorDriver.Speed, TargetSpeed * SpeedMulti, ref Acc, AccTime);
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        private bool Distrated = false;
        private Vector3 AvoidVector;

        public float Annoyed = 0;

        private float distractionTime = 0;
        private float endDistractionTim = 0;

        float minDistractionTime = 0.6f;
        float maxDistractionTime = 3f;
        private void PickDistractedDirection()
        {
            DirectionVector = ConvertToWorldVector(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized);
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

        private Vector3 ConvertToWorldVector(Vector3 input)
        {
            Vector3 worldVector = (new Vector3(input.x, 0, input.y));
            worldVector = Camera.main.transform.TransformDirection(worldVector);
            worldVector = new Vector3(worldVector.x, 0, worldVector.z);
            return worldVector.normalized * TargetSpeed;
        }



        private void Rotate(Vector3 dir)
        {

            dir = Vector3.Lerp(DirectionVector, dir, Annoyed);

            float TurnSpeedMulti = Mathf.Max(0.4f, Annoyed);
            if (dir.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir),
                    turnSpeed * Time.deltaTime * TurnSpeedMulti);
            }
        }
    }
}