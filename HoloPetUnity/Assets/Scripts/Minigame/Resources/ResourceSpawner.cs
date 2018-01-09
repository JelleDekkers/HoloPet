using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame {

    public class ResourceSpawner : MonoBehaviour {

        private static ResourceSpawner instance;
        public static ResourceSpawner Instance { get { return instance; } }

        [SerializeField] private Resource[] resourcePrefabs;
        [SerializeField] private float timeBetweenSpawnsMin = 1, timeBetweenSpawnsMax = 2;

        public Vector3 range = new Vector3(3, 3, 3);

        private float spawnTimer;

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
            Vector3 rndSpawnLocation = Common.GetRandomPositionWithinRange(range, transform.position);
            Resource r = Instantiate(resourcePrefabs.GetRandom(), rndSpawnLocation, Random.rotation);
            Resources.Add(r);
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireCube(transform.position, range);
        }
    }
}