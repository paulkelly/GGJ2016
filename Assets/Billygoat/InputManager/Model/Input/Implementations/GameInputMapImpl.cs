using UnityEngine;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class GameInputMapImpl : IGameInputMap
    {
        private Dictionary<int, GameButtonInput> buttonInputs = new Dictionary<int, GameButtonInput>();

        private Dictionary<int, GameTwoAxisInput> twoAxisInputs = new Dictionary<int, GameTwoAxisInput>();

        #region IGameInputMap implementation

        public void RegisterButtonInput(GameButtonInput buttonInput)
        {
            buttonInputs.Add(buttonInput.Id, buttonInput);
        }

        public GameButtonInput GetButtonInput(int key)
        {
            GameButtonInput buttonInput;
            buttonInputs.TryGetValue(key, out buttonInput);
            return buttonInput;
        }

        public void RegisterTwoAxisInput(GameTwoAxisInput twoAxisInput)
        {
            twoAxisInputs.Add(twoAxisInput.Id, twoAxisInput);
        }

        public GameTwoAxisInput GetTwoAxisInput(int key)
        {
            GameTwoAxisInput twoAxisInput;
            twoAxisInputs.TryGetValue(key, out twoAxisInput);
            return twoAxisInput;
        }

        #endregion
    }
}