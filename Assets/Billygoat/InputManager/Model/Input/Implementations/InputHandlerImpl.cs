using UnityEngine;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class InputHandlerImpl : IInputHandler
    {
        private Dictionary<int, ButtonInputCommand> buttonInputs = new Dictionary<int, ButtonInputCommand>();
        private Dictionary<int, TwoAxisInputCommand> twoAxisInputs = new Dictionary<int, TwoAxisInputCommand>();

        #region IInput implementation
        public void Update()
        {
            foreach (ButtonInputCommand input in buttonInputs.Values)
            {
                input.Update();
            }

            foreach (TwoAxisInputCommand input in twoAxisInputs.Values)
            {
                input.Update();
            }
        }

        public void Clear()
        {
            buttonInputs.Clear();
            twoAxisInputs.Clear();
        }

        public void AddButtonInput(int key, ButtonInputCommand input)
        {
            if (buttonInputs.ContainsKey(key))
            {
                ButtonInputCommand toMerge;
                buttonInputs.TryGetValue(key, out toMerge);
                toMerge.Merge(input);
            }
            else
            {
                buttonInputs.Add(key, input);
            }
        }

        public void AddTwoAxisInput(int key, TwoAxisInputCommand input)
        {
            if (twoAxisInputs.ContainsKey(key))
            {
                TwoAxisInputCommand toMerge;
                twoAxisInputs.TryGetValue(key, out toMerge);
                toMerge.Merge(input);
            }
            else
            {
                twoAxisInputs.Add(key, input);
            }
        }
        #endregion
    }
}