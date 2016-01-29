using UnityEngine;

namespace Billygoat.InputManager
{
    public abstract class GameButtonInput
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

        public GameButtonInput(int id)
            : this(id, 0)
        {
        }

        public GameButtonInput(int id, float delay)
        {
            this.delay = delay;
            this.id = id;
        }

        [PostConstruct]
        public void RegisterWithMap()
        {
            GameInputMap.RegisterButtonInput(this);
        }

        public virtual void OnDown()
        {
        }

        public virtual void OnUp()
        {
        }

        public virtual void OnStay()
        {
        }
    }
}