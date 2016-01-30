using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GGJ2016
{
    public class FemaleCollector : MonoBehaviour
    {
        public List<PigeonScoreCollider> ScoreColliders = new List<PigeonScoreCollider>();

        public float GetScoreMulti()
        {
            float max = 0.2f;

            foreach(var collider in ScoreColliders)
            {
                max = Mathf.Max(max, collider.ScoreMulti);
            }

            return max;
        }

        private void OnTriggerEnter(Collider other)
        {
            PigeonScoreCollider col = other.GetComponent<PigeonScoreCollider>();

            if(col != null)
            {
                ScoreColliders.Add(col);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PigeonScoreCollider col = other.GetComponent<PigeonScoreCollider>();

            if (col != null)
            {
                ScoreColliders.Remove(col);
            }
        }
    }
}