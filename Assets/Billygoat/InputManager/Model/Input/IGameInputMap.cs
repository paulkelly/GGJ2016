using UnityEngine;

namespace Billygoat.InputManager
{
    public interface IGameInputMap
    {
        void RegisterButtonInput(GameButtonInput buttonInput);

        GameButtonInput GetButtonInput(int key);

        void RegisterTwoAxisInput(GameTwoAxisInput twoAxisInput);

        GameTwoAxisInput GetTwoAxisInput(int key);

    }
}