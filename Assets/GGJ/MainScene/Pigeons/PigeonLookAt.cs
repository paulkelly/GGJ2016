using UnityEngine;
using System.Collections.Generic;
using RootMotion.FinalIK;
using System.Linq;

namespace GGJ2016
{
    public class PigeonLookAt : MonoBehaviour
    {
        public LookAtIK LookIK;
        public InterestingTarget Self;

        public List<InterestingTarget> Targets = new List<InterestingTarget>();

        private float lookTime;
        private float maxLookTime;

        void Update()
        {
            if(Targets.Count > 0)
            {
                if(lookTime > maxLookTime)
                {
                    PickTarget(GetRandomTarget());
                    SetLookAtWeight(1);
                }
                else
                {
                    lookTime += Time.deltaTime;
                }
            }
            else
            {
                lookTime = 0;
                SetLookAtWeight(0);
                LookIK.solver.target = null;
            }

            if(LookIK.solver.IKPositionWeight != _targetWeight)
            {
                _weightLerpTime += Time.deltaTime;
                float percComplete = _weightLerpTime / _maxWeightLerpTime;
                if(percComplete < 1)
                {
                    LookIK.solver.IKPositionWeight = Mathf.Lerp(_initalWeight, _targetWeight, percComplete);
                }
                else
                {
                    LookIK.solver.IKPositionWeight = _targetWeight;
                }
            }
        }

        private float _initalWeight;
        private float _targetWeight;
        private float _weightLerpTime;
        private float _maxWeightLerpTime = 0.2f;

        private void SetLookAtWeight(float target)
        {
            _initalWeight = LookIK.solver.IKPositionWeight;
            _targetWeight = target;
            _weightLerpTime = 0;
        }

        private InterestingTarget GetRandomTarget()
        {
            InterestingTarget result = Targets[0];

            foreach(var tar in Targets)
            {
                result = tar;

                if (Random.Range(0, 100) > Mathf.Min(20 + Targets.Count*5, 70))
                {
                    break;
                }
            }

            return result;
        }

        private void PickTarget(InterestingTarget target)
        {
            lookTime = 0;
            maxLookTime = Random.Range(0.5f, 3);

            LookIK.solver.target = target.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            InterestingTarget col = other.GetComponent<InterestingTarget>();

            if (col != null && col != Self)
            {
                Targets.Add(col);
                Targets.Sort((x, y) => x.Priority.CompareTo(y.Priority));

                PickTarget(col);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            InterestingTarget col = other.GetComponent<InterestingTarget>();

            if (col != null)
            {
                Targets.Remove(col);
            }
        }
    }
}