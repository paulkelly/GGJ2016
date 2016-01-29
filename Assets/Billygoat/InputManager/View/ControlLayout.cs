using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Billygoat.InputManager._View
{
    public class ControlLayout : View
    {
        internal Signal loadLayout = new Signal();

        public ControlLayoutPersistance MappingSaveFile;


        protected override void OnStart()
        {
            StartCoroutine(RunInitialize());
        }

        private IEnumerator RunInitialize()
        {
#if UNITY_PSP2|| UNITY_PS4
            yield return new WaitForSeconds(2);
#else
            yield return null;
#endif
            Initialize();
        }

        private void Initialize()
        {
            loadLayout.Dispatch();
        }
    }
}