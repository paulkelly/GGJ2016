using UnityEngine;
using strange.extensions.command.impl;
using Billygoat.InputManager;

namespace Billygoat.InputManager
{
    public class UpdateInputCommand : Command
    {
        [Inject]
        public IInputHandler inputHandler { get; set; }

        public override void Execute()
        {
            inputHandler.Update();
        }
    }
}