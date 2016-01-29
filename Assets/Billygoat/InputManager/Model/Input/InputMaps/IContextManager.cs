using System;
using UnityEngine;
using System.Collections;

namespace Billygoat.InputManager
{
    public interface IContextManager
    {
        void Add(IInputContext context);

        void Remove(IInputContext context);

        void Update();

        void LateUpdate();

        void Sort();

        void DoSort();

        void HandleInput(int id, IControl buttonInput);

        void HandleInput(int id, ITwoAxisControl twoAxisInput);
    }
}