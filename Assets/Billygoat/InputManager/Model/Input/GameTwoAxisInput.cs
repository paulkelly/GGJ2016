using UnityEngine;
using Billygoat.InputManager;

namespace Billygoat.InputManager
{
    public abstract class GameTwoAxisInput
    {
        [Inject]
        public IGameInputMap GameInputMap { get; set; }

        int id;

        float delay;

        public float Delay
        {
            get
            {
                return delay;
            }

            set
            {
                delay = value;
            }

        }

        public int Id
        {
            get
            {
                return id;
            }
        }

        public GameTwoAxisInput(int id)
            : this(id, 0)
        {
        }

        public GameTwoAxisInput(int id, float delay)
        {
            this.id = id;
            this.delay = delay;
        }

        [PostConstruct]
        public void RegisterWithMap()
        {
            GameInputMap.RegisterTwoAxisInput(this);
        }

        public virtual void OnUpdate(ITwoAxisControl value)
        {
        }

        public virtual void OnStay(ITwoAxisControl value)
        {
        }

        public virtual void OnEnter(ITwoAxisControl value)
        {
        }

        public virtual void OnLeave(ITwoAxisControl value)
        {
        }
    }
}