using UnityEngine;
using Billygoat.MultiplayerInput;
using strange.extensions.mediation.impl;
using System.Collections.Generic;

namespace GGJ2016
{
    public class PigeonSpawnerView : View
    {

        public GameObject PigeonPrefab;
        public bool AllowDebugSpawning;

        public List<Transform> SpawnPoints = new List<Transform>();

        [Inject]
        public IMultiInputManager InputManager { get; set; }

        [Inject]
        public MultiInputSignals InputSignals { get; set; }



        [PostConstruct]
        public void OnConstruct()
        {
            foreach(var player in InputManager.GetPlayers())
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

        private void Spawn(PlayerData player)
        {
            int spawnPoint = Random.Range(0, SpawnPoints.Count);

            GameObject newPigeon = (GameObject) Instantiate(PigeonPrefab, SpawnPoints[spawnPoint].position, Quaternion.identity);
            SpawnPoints.RemoveAt(spawnPoint);

            newPigeon.transform.parent = transform;
            PigeonController controller = newPigeon.GetComponent<PigeonController>();

            if(controller != null)
            {
                newPigeon.name = "Pigeon " + player.id;
                controller.Player = player;
            }
            else
            {
                Debug.LogError("Prefab does not have a pigeon controller");
            }
        }


    }
}