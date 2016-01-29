using System.Collections.Generic;
using UnityEngine;

namespace Billygoat.InputManager.Model.Impl
{
    public class ContextInputHandlerImpl : IInputHandler
    {
        private readonly Dictionary<int, IControl> buttonInputs = new Dictionary<int, IControl>();
        private readonly Dictionary<int, ITwoAxisControl> twoAxisInputs = new Dictionary<int, ITwoAxisControl>();

        [Inject]
        public IContextManager contextManager { get; set; }



        #region IInput implementation
        public void Update()
        {
            //MappedInputs inputsThisFrame = new MappedInputs (buttonInputs, twoAxisInputs);
            //allInputsThisFrame.Dispatch (new MappedInputs(buttonInputs, twoAxisInputs));
            contextManager.Update();

            foreach (var button in buttonInputs)
            {
                contextManager.HandleInput(button.Key, button.Value);
            }
            foreach (var twoAxis in twoAxisInputs)
            {
                contextManager.HandleInput(twoAxis.Key, twoAxis.Value);
            }

            contextManager.LateUpdate();
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
                IControl toMerge;
                buttonInputs.TryGetValue(key, out toMerge);
                toMerge.Merge(input.GetInput());
            }
            else
            {
                IControl mergeableControl = new BaseControl();
                mergeableControl.Merge(input.GetInput());
                buttonInputs.Add(key, mergeableControl);
            }
        }

        public void AddTwoAxisInput(int key, TwoAxisInputCommand input)
        {
            if (twoAxisInputs.ContainsKey(key))
            {
                ITwoAxisControl toMerge;
                twoAxisInputs.TryGetValue(key, out toMerge);
                toMerge.Merge(input.GetInput());
            }
            else
            {
                twoAxisInputs.Add(key, input.GetInput());
            }
        }
        #endregion
    }
}