

using UnityEngine;
using strange.extensions.mediation.impl;
using System.Collections.Generic;

namespace Billygoat.MultiplayerInput
{
    public class CharacterSpawner : View
    {

        public GameObject CharacterPrefab;
        public bool AllowDebugSpawning;

        public List<Transform> SpawnPoints = new List<Transform>();

        [Inject]
        public IMultiInputManager InputManager { get; set; }

        [Inject]
        public MultiInputSignals InputSignals { get; set; }



        [PostConstruct]
        public void OnConstruct()
        {
            foreach (var player in InputManager.GetPlayers())
            {
                Spawn(player);
            }

            if (AllowDebugSpawning)
            {
                InputSignals.PlayerJoined.AddListener(Spawn);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (AllowDebugSpawning)
            {
                InputSignals.PlayerJoined.RemoveListener(Spawn);
            }
        }

        private void Spawn(PlayerDevice player)
        {
            int spawnPoint = Random.Range(0, SpawnPoints.Count);

            GameObject newPlayer = (GameObject)Instantiate(CharacterPrefab, SpawnPoints[spawnPoint].position, Quaternion.identity);
            if (!AllowDebugSpawning)
            {
                SpawnPoints.RemoveAt(spawnPoint);
            }

            newPlayer.transform.parent = transform;
            CharacterController controller = newPlayer.GetComponent<CharacterController>();

            if (controller != null)
            {
                newPlayer.name = "Player " + player.id;
                controller.Player = player;
            }
            else
            {
                Debug.LogError("Prefab does not have a character controller");
            }
        }


    }
}