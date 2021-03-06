﻿using UnityEngine;
using System.Collections.Generic;
using RootMotion.FinalIK;
using System.Linq;

namespace GGJ2016
{
    public class PigeonLookAt : MonoBehaviour
    {
        public PigeonMover Pigeon;
        private float Puffyness
        {
            get
            {
                if(Pigeon != null)
                {
                    return Pigeon.Scorer.NormalizedPuffyness();
                }

                return 0;
            }
         }

        public LookAtIK LookIK;
        public InterestingTarget Self;

        public List<InterestingTarget> Targets = new List<InterestingTarget>();

        public PigeonIKTarget lookAtTarget;

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
            }

            float nextTargetWeight = Mathf.Min((1.2f - Puffyness), _targetWeight);
            if(Pigeon._inPosition)
            {
                nextTargetWeight = 0;
            }

            LookIK.solver.IKPositionWeight = Mathf.SmoothDamp(LookIK.solver.IKPositionWeight, nextTargetWeight, ref _weightV, _weightAcc);
        }

        private float _targetWeight;
        private float _weightV;
        private const float _weightAcc = 0.3f;

        private void SetLookAtWeight(float target)
        {
            _targetWeight = target;
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

            lookAtTarget.Target = target.transform;
            LookIK.solver.target = lookAtTarget.transform;
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