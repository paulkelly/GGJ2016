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
            }
        }
    }
}