using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Billygoat.MultiplayerInput;
using System.Collections.Generic;
using Billygoat.Audio;

namespace GGJ2016
{
    public class FemalePigeon : View
    {
        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        [Inject]
        public MultiInputSignals InputSignals { get; set; }

        [Inject]
        public AudioSignals TheAudioSignals { get; set; }

        public AudioClip EndTheme;

        private const float turnSpeed = 500f;

        public SFXView Caw;
        public SFXView Flap;

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

        [PostConstruct]
        public void OnConstruct()
        {
            ThePigeonSignals.SetPigeonScore.AddListener(ListenScores);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ThePigeonSignals.SetPigeonScore.RemoveListener(ListenScores);
        }

        protected override void OnStart()
        {
            _animatorDriver = new PigeonAnimatorDriver(FindAnimator());
            _collector = GetComponentInChildren<FemaleCollector>(true);
            _rigidbody = GetComponent<Rigidbody>();
            _lookTarget = GetComponentInChildren<PigeonIKTarget>(true);

            DirectionVector = transform.forward;

            ThePigeonSignals.PigeonSpawned.Dispatch(transform);

            Annoyed = 0;
        }

        private float Speed;
        private float TargetSpeed;
        private float NextTargetSpeed;
        private float Acc;

        private const float SpeedMulti = 1;
        private const float AccTime = 0.3f;

        private bool _keepCourse = false;
        private void OnCollisionStay(Collision collision)
        {
            UpdateScores();

            if (_mating)
            {
                return;
            }

            if (collision.transform.tag != "Floor")
            {

                if (!_keepCourse)
                {
                    Vector3 contactPoint = new Vector3(collision.contacts[0].point.x, 0, collision.contacts[0].point.z);
                    Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
                    DirectionVector = (position - contactPoint).normalized;
                }
                Annoyed = 1;

                PigeonMover otherPigeon = collision.transform.GetComponent<PigeonMover>();
                if (otherPigeon != null)
                {
                    if (otherPigeon.CanWin() && otherPigeon.Player.id == WinningPlayer)
                    {
                        Annoyed = 0.5f;

                        StartMating(otherPigeon);
                    }
                    else
                    {
                        Flap.Play();
                        otherPigeon.LosePoints(Time.deltaTime * 10);
                    }
                }
                
                if (collision.transform.tag == "Wall")
                {
                    distractionTime = 0;
                    endDistractionTim = 3;
                    _keepCourse = true;
                }
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        private void Update()
        {
            if(_inPosition)
            {
                _animatorDriver.Speed = 0;
                return;
            }

            if (_keepCourse)
            {
                Annoyed = 1;
            }

            if (distractionTime > endDistractionTim)
            {
                distractionTime = 0;
                _keepCourse = false;
                endDistractionTim = Random.Range(minDistractionTime, maxDistractionTime);

                PickDistractedDirection();
            }
            else
            {
                distractionTime += Time.deltaTime;
            }

            TargetSpeed = Mathf.Max(NextTargetSpeed, Annoyed*2);

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
        float maxDistractionTime = 2f;
        private void PickDistractedDirection()
        {
            Caw.Play();

            if (Random.Range(0, 100) > 10)
            {
                NextTargetSpeed = Random.Range(0.7f, 1.2f);
            }
            else
            {
                NextTargetSpeed = 0f;
            }

            if (transform.position.magnitude > 3)
            {
                Vector3 random = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

                DirectionVector = (-transform.position + random).normalized;
            }
            else
            {
                DirectionVector = ConvertToWorldVector(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized);
            }
        }

        private void OnAnimatorMove()
        {
            if (_animator != null)
            {

                if (!_inPosition && !_mating)
                {
                    Vector3 newPos = _animator.rootPosition;
                    _rigidbody.MovePosition(newPos);

                    _animator.rootPosition = _rigidbody.position;
                }
                else
                {
                    _rigidbody.isKinematic = true;
                    //transform.position = _animator.rootPosition;
                   // transform.rotation = _animator.rootRotation;
                }
            }
        }

        private bool _mating;
        private bool _inPosition;
        private Vector3 _initalPosition;
        private Vector3 _finalPosition;
        private Quaternion _initalRotation;
        private Quaternion _finalRotation;
        private float _matingLerpTime;
        private float _totalMatingTime;
        void LateUpdate()
        {
            if (_mating && !_inPosition)
            {
                Vector3 contactPoint = new Vector3(Winner.transform.position.x, 0, Winner.transform.position.z);
                Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
                DirectionVector = (position - contactPoint).normalized;

                if(Quaternion.Angle(transform.rotation, Quaternion.LookRotation(DirectionVector)) < 10)
                {
                    _inPosition = true;
                    _totalMatingTime = Mathf.Max(Vector3.Distance(_initalPosition, Winner.transform.position));

                    Winner.StartMating(MountPoint, _totalMatingTime);

                    ThePigeonSignals.PlayerWins.Dispatch(Winner.Player);
                    InputSignals.EndGame.Dispatch();
                }
            }
        }

        public Transform MountPoint;
        private PigeonMover Winner;
        private void StartMating(PigeonMover Male)
        {
            _mating = true;
            Winner = Male;
            _initalPosition = transform.position;
            _finalPosition = transform.position;

            _initalRotation = transform.rotation;
            _finalRotation = transform.rotation;

            _matingLerpTime = 0;
            _totalMatingTime = Mathf.Max(Vector3.Distance(_initalPosition, Male.transform.position));

            Male._waitForMate = true;
            //Male.StartMating(MountPoint, _totalMatingTime);

            AudioPlaylistType music = new AudioPlaylistType();
            music.Clips.Add(EndTheme);
            ThePigeonSignals.TurnOffSleezeMusic.Dispatch();
            TheAudioSignals.PlayMusicTrack.Dispatch(music);
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

            float TurnSpeedMulti = Mathf.Max(0.3f, Annoyed);
            if (dir.magnitude > 0.1f && _animatorDriver.Speed > 0.3f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir),
                    turnSpeed * Time.deltaTime * TurnSpeedMulti);
            }
        }


        private float MaxScore = 0;
        private int WinningPlayer = 1;
        private Dictionary<int, float> scores = new Dictionary<int, float>();

        public Color[] Colors;
        public ParticleSystem particles;
        public Material ParticalMat;

        private void UpdateScores()
        {
            MaxScore = 0;
            foreach (KeyValuePair<int, float> v in scores)
            {
                if(v.Value >= MaxScore)
                {
                    WinningPlayer = v.Key;
                    MaxScore = v.Value;
                }
            }

            particles.emissionRate = (MaxScore / 100) * 3;
            particles.startColor = Color.Lerp(particles.startColor, Colors[WinningPlayer-1], Time.deltaTime * 4);
            //ParticalMat.SetColor("_TintColor", Color.Lerp(ParticalMat.GetColor("_TintColor"), Colors[WinningPlayer], Time.deltaTime*4));
        }

        private void ListenScores(ScoreData data)
        {
            int key = data.Player.id;
            if (scores.ContainsKey(key))
            {
                scores[key] = data.Score;
            }
            else
            {
                scores.Add(key, data.Score);
            }
        }
    }
}