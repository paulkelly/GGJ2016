using UnityEngine;

namespace Billygoat.InputManager
{
    public interface IInputHandler
    {
        void Update();

        void Clear();

        void AddButtonInput(int key, ButtonInputCommand input);

        void AddTwoAxisInput(int key, TwoAxisInputCommand input);
    }
}