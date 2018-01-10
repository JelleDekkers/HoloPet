using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Minigame {

    public class ResourceSpawner : MonoBehaviour {

        private static ResourceSpawner instance;
        public static ResourceSpawner Instance { get { return instance; } }

        [SerializeField] private Resource[] resourcePrefabs;
        [SerializeField] private float timeBetweenSpawnsMin = 1, timeBetweenSpawnsMax = 2;

        public Vector3 range = new Vector3(3, 3, 3);

        private float spawnTimer;
        private Emotion lastSpawned;

        private void OnGUI() {
            GUI.Label(new Rect(10, 10, 1000, 20), "last: " + lastSpawned);
        }

        private void Awake() {
            instance = this;
        }

        private void Update() {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer < 0) {
                SpawnResourceAtRandomPosition();
                ResetSpawnTimer();
            }
        }

        private void ResetSpawnTimer() {
            spawnTimer = Random.Range(timeBetweenSpawnsMin, timeBetweenSpawnsMax);
        }

        private void SpawnResourceAtRandomPosition() {
            List<Resource> resourcesEligibleForSpawn = new List<Resource>();
            foreach (Resource p in resourcePrefabs) {
                if(p.emotion != lastSpawned)
                    resourcesEligibleForSpawn.Add(p);
            }
            Vector3 rndSpawnLocation = Common.GetRandomPositionWithinRange(range, transform.position);
            Resource r = Instantiate(resourcesEligibleForSpawn.GetRandom(), rndSpawnLocation, Random.rotation);
            lastSpawned = r.emotion;
            Resources.Add(r);
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireCube(transform.position, range);
        }
    }
}