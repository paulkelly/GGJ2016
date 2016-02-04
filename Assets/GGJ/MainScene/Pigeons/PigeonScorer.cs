using UnityEngine;
using Billygoat.MultiplayerInput;
using System.Collections;

namespace GGJ2016
{
    public class PigeonScorer
    {
        [Inject ("FemalePigeon")]
        public GameObject Female { get; set; }

        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        private PlayerData player;

        private Transform _male;
        public Transform Male
        {
            get
            {
                return _male;
            }

            set
            {
                _male = value;
                PigeonController controller = value.GetComponent<PigeonController>();

                if(controller != null)
                {
                    player = controller.Player;
                }
            }
        }

        public float NormalizedPuffyness()
        {
            return (Puffyness - MinPuffyness) / (MaxPuffyness - MinPuffyness);
        }

        public const float MaxPuffyness = 6f;
        public const float MinPuffyness = 1;
        public const float PuffynessIncreaseRate = 1f;
        public const float PuffynessDecayRate = 0.5f;
        public float PuffynessDecayMulti = 1f;

        private float PuffynessTarget = 1;
        private float PuffynessV;
        private const float PuffynessDampTime = 0.2f;

        public float Score = 0;

        public float Puffyness
        {
            get; private set;
        }

        public void AddPuffyness()
        {
            PuffynessTarget = Mathf.Min(MaxPuffyness, Puffyness + PuffynessIncreaseRate);
        }

        public void AddScore(float amount)
        {
            Score += amount * Puffyness;
        }

        public void LoseScore(float amount)
        {
            Score = Mathf.Max(Score - amount, 0);
        }

        public void Update()
        {
            PuffynessTarget = Mathf.Max(MinPuffyness, PuffynessTarget - (PuffynessDecayRate * Time.deltaTime));
            Puffyness = Mathf.SmoothDamp(Puffyness, PuffynessTarget, ref PuffynessV, PuffynessDampTime);
            Score = Mathf.Max(0, Score - (GetScoreDecayRate() * PuffynessDecayMulti));

            if(player != null)
            {
                ScoreData data = new ScoreData()
                {
                    Player = player,
                    Score = this.Score
                };
                ThePigeonSignals.SetPigeonScore.Dispatch(data);
            }
        }

        private float GetScoreDecayRate()
        {
            float dist = Vector3.Distance(Male.position, Female.transform.position);

            //return Time.deltaTime * Mathf.Log(dist + 1, 2.718f);
            return Time.deltaTime * Mathf.Log(dist + 1, 3) * 3f;
        }

    }
}