using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using strange.extensions.context.api;
using strange.extensions.context.impl;

namespace Billygoat.InputManager.Implementations.InControlImpl
{
    public class InControlInputManagerImpl : IInputManager
    {
        private const string GAMEOBJECT_NAME = "InControl";

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

        [Inject]
        public UpdateControlLayout UpdateInputsSignal { get; set; }

        private IDevice activeDevice;

        private IKeyboard keyboard;

        public InControlInputManagerImpl()
        {
        }

        private IEnumerator runSetup()
        {
            while (InControl.InputManager.ActiveDevice.Name == "Unknown Device (Empty)")
            {
                yield return new WaitForEndOfFrame();
            }
            InControlManager[] myArray = Resources.FindObjectsOfTypeAll<InControlManager>();
            if (myArray.Length > 0)
            {
                //Debug.LogError(myArray.Length);

                AddNewDevice();
                keyboard = new Keyboard();
            }
            else
            {
                GameObject go = new GameObject(GAMEOBJECT_NAME);
                go.AddComponent<InControlManager>();
                go.transform.parent = contextView.transform;

                AddNewDevice();

                keyboard = new Keyboard();
            }

            //            useLayout.Dispatch();
        }

        [PostConstruct]
        public void Setup()
        {
            contextView.GetComponent<ContextView>().StartCoroutine(runSetup());
        }

        public IDevice ActiveDevice
        {
            get { return activeDevice; }
        }

        public IKeyboard Keyboard
        {
            get { return keyboard; }
        }

        private void AddNewDevice()
        {
            currectDevice = InControl.InputManager.ActiveDevice;
            activeDevice = new InControlDevice(InControl.InputManager.ActiveDevice);
            Devices.Add(InControl.InputManager.ActiveDevice, activeDevice);
        }

        private Dictionary<InputDevice, IDevice> Devices = new Dictionary<InputDevice, IDevice>();
        private InputDevice currectDevice;
        public void RedetectActiveDevice()
        {
            if (currectDevice != InControl.InputManager.ActiveDevice)
            {
                if (Devices.ContainsKey(InControl.InputManager.ActiveDevice))
                {
                    Devices.TryGetValue(InControl.InputManager.ActiveDevice, out activeDevice);
                    currectDevice = InControl.InputManager.ActiveDevice;
                }
                else
                {
                    AddNewDevice();
                }

                UpdateInputsSignal.Dispatch();
            }
        }
    }
}