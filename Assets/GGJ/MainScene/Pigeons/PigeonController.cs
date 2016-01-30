using UnityEngine;
using Billygoat.MultiplayerInput;

namespace GGJ2016
{
    public class PigeonController : MonoBehaviour
    {
        public PlayerData Player;

        private PigeonMover _pigeonMover;

        void Awake()
        {
            _pigeonMover = GetComponent<PigeonMover>();
        }

        void Update()
        {
            if(Player != null && _pigeonMover != null)
            {
                _pigeonMover.Move(Player.InControlDevice.Direction.Value);

                if(Player.InControlDevice.Action1.WasPressed)
                {
                    _pigeonMover.OnSwoopDown();
                }
                if(Player.InControlDevice.Action1.WasReleased)
                {
                    _pigeonMover.OnSwoopUp();
                }

                if (Player.InControlDevice.Action3.WasReleased)
                {
                    _pigeonMover.PuffUp();
                }

                if (Player.InControlDevice.Action4.WasReleased)
                {
                    _pigeonMover.Caw();
                }

                if (Player.InControlDevice.Action2.WasReleased)
                {
                    _pigeonMover.Wings();
                }
            }
        }
    }
}