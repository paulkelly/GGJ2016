using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Billygoat.InputManager.Model.Impl
{
    public class InputTypeModelImpl : IInputTypeModel
    {

        private Signal<InputType> _onInputTypeChanged = new Signal<InputType>();

        public InputTypeModelImpl()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            //InputType = InputType.KeyboardAndMouse;
            InputType = InputType.Gamepad;
#else
			InputType = InputType.TouchDevice;
#endif
        }

        private InputType _inputType;

        public InputType InputType
        {
            get
            {
                return _inputType;
            }
            set
            {
                _inputType = value;
                OnInputTypeChanged.Dispatch(_inputType);
            }
        }

        public Signal<InputType> OnInputTypeChanged
        {
            get { return _onInputTypeChanged; } 
        }
    }
}