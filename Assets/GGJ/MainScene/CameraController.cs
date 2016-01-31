using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

namespace GGJ2016
{
    public class CameraController : View
    {
        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        public List<Transform> chrs = new List<Transform>();

        private Vector3 OrigianlPosition;
        private Vector3 OriginalRotation;

        private Vector3 lookAt;
        private Vector3 vel;

        private float cameraDistance = 2;
        private float distV;

        private float lerpTime = 1.2f;

        [PostConstruct]
        public void OnConstruct()
        {
            ThePigeonSignals.PigeonSpawned.AddListener(AddPigeon);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ThePigeonSignals.PigeonSpawned.RemoveListener(AddPigeon);
        }

        private void AddPigeon(Transform transform)
        {
            chrs.Add(transform);
        }

        protected override void OnStart()
        {
            base.OnStart();

            OrigianlPosition = transform.position;
            OriginalRotation = transform.eulerAngles;
        }

        void Update()
        {
            Vector3 midPoint = Vector3.zero;
            float avDist = 0;
            int count = 0;
            foreach (var chr in chrs)
            {
                midPoint += chr.position;
                count++;
            }
            if (count < 1)
            {
                return;
            }
            midPoint = midPoint / count;
            foreach (var chr in chrs)
            {
                avDist += Vector3.Distance(midPoint, chr.position);
            }
            avDist = avDist / count;

            if (lookAt != Vector3.zero)
            {
                lookAt = Vector3.SmoothDamp(lookAt, midPoint, ref vel, lerpTime);
                cameraDistance = Mathf.SmoothDamp(cameraDistance, Mathf.Clamp(avDist, 0f, 5), ref distV, lerpTime);
            }
            else
            {
                lookAt = midPoint;
            }

            Vector3 camZoomVector = OrigianlPosition - midPoint;
            if (cameraDistance > 0)
            {
                //transform.position = new Vector3(Mathf.Clamp(lookAt.x, -6, 6), OrigianlPosition.y + (cameraDistance/10),
                //    OrigianlPosition.z - (camZoomVector.z / cameraDistance));

                transform.position = new Vector3(Mathf.Clamp(lookAt.x, -8, 8), OrigianlPosition.y + cameraDistance,
                        lookAt.z - (3 + cameraDistance));
            }

            //transform.LookAt(lookAt);
            //transform.eulerAngles = new Vector3(OriginalRotation.x, transform.eulerAngles.y, OriginalRotation.z);
            //transform.position = Vector3.Lerp(midPoint, OrigianlPosition, cameraDistance);
        }
    }
}