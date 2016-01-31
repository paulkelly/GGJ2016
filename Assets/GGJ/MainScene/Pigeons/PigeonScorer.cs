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

        public const float MaxPuffyness = 2f;
        public const float MinPuffyness = 1;
        public const float PuffynessIncreaseRate = 1;
        public const float PuffynessDecayRate = 0.5f;

        public float Score;

        public float Puffyness
        {
            get; private set;
        }

        public void AddPuffyness()
        {
            Puffyness = Mathf.Min(MaxPuffyness, Puffyness + PuffynessIncreaseRate);
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
            Puffyness = Mathf.Max(MinPuffyness, Puffyness - (PuffynessDecayRate * Time.deltaTime));
            Score = Mathf.Max(0, Score - GetScoreDecayRate());

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
            return Time.deltaTime * Mathf.Log(dist + 1, 3) * 2f;
        }

    }
}