using UnityEngine;
using strange.extensions.command.impl;
using Billygoat.InputManager;
using Billygoat.InputManager._View;

namespace Billygoat.InputManager
{
    public class SetControlLayoutCommand : Command
    {
        [Inject]
        public IInputManager inputManager { get; set; }

        [Inject]
        public IInputHandler inputHandler { get; set; }

        [Inject]
        public ControlLayout newLayout { get; set; }

        [Inject]
        public IGameInputMap inputMap { get; set; }

        public override void Execute()
        {
            inputHandler.Clear();

            foreach (GamepadButtonInputMapping buttonMapping in newLayout.MappingSaveFile.ButtonMap)
            {
                GameButtonInput input = inputMap.GetButtonInput((int)buttonMapping.gameInput);

                if (input != null)
                {
                    GamepadButtonInputCommand inputCommand = new GamepadButtonInputCommand(inputManager, buttonMapping.billygoatInput, input.Delay);

                    inputCommand.OnDown = input.OnDown;
                    inputCommand.OnUp = input.OnUp;
                    inputCommand.OnStay = input.OnStay;

                    inputHandler.AddButtonInput(buttonMapping.gameInput, inputCommand);
                }
                else
                {
                    Debug.LogWarning(buttonMapping.gameInput + " did not have a ButtonInput.");
                }

            }

            foreach (JoystickInputMapping joystickMapping in newLayout.MappingSaveFile.JoystickMap)
            {
                GameTwoAxisInput input = inputMap.GetTwoAxisInput((int)joystickMapping.gameInput);

                if (input != null)
                {
                    TwoAxisInputCommand inputCommand = new TwoAxisInputCommand(inputManager, joystickMapping.billygoatInput, input.Delay);

                    inputCommand.OnUpdate = input.OnUpdate;
                    inputCommand.OnEnter = input.OnEnter;
                    inputCommand.OnLeave = input.OnLeave;
                    inputCommand.OnStay = input.OnStay;

                    inputHandler.AddTwoAxisInput(joystickMapping.gameInput, inputCommand);
                }
                else
                {
                    Debug.LogWarning(joystickMapping.gameInput + " did not have a ButtonInput.");
                }
            }

            foreach (KeyboardInputMapping keyboardMapping in newLayout.MappingSaveFile.KeyboardMap)
            {

                KeyboardInputCommand inputCommand = new KeyboardInputCommand(inputManager, keyboardMapping.keyboardButton);

                GameButtonInput input = inputMap.GetButtonInput((int)keyboardMapping.gameInput);
                if (input != null)
                {
                    inputCommand.OnDown = input.OnDown;
                    inputCommand.OnUp = input.OnUp;
                    inputCommand.OnStay = input.OnStay;

                    inputHandler.AddButtonInput(keyboardMapping.gameInput, inputCommand);
                }
                else
                {
                    Debug.LogWarning(keyboardMapping.gameInput + " did not have a ButtonInput.");
                }
            }
        }
    }
}