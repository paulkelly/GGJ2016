using UnityEngine;
using System.Collections;

namespace GGJ2016
{
    public class PigeonScorer
    {
        [Inject ("FemalePigeon")]
        public GameObject Female { get; set; }

        public Transform Male;

        public const float MaxPuffyness = 3;
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

        public void Update()
        {
            Puffyness = Mathf.Max(MinPuffyness, Puffyness - (PuffynessDecayRate * Time.deltaTime));
            Score = Mathf.Max(0, Score - GetScoreDecayRate());
        }

        private float GetScoreDecayRate()
        {
            float dist = Vector3.Distance(Male.position, Female.transform.position);
            //return Time.deltaTime * Mathf.Log(dist + 1, 2);
            return Time.deltaTime * Mathf.Log(dist + 1, 2.718f);
        }

    }
}