using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame {

    public class ResourceSpawner : MonoBehaviour {

        [SerializeField] private Resource[] resourcePrefabs;
        [SerializeField] private float timeBetweenSpawnsMin = 1, timeBetweenSpawnsMax = 2;
        [SerializeField] private float spawnDistanceFromCentre = 5;

        private float spawnTimer;

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
            Vector3 rndSpawnLocation = transform.position;
            rndSpawnLocation.x += Random.Range(-spawnDistanceFromCentre, spawnDistanceFromCentre);
            Resource r = Instantiate(resourcePrefabs.GetRandom(), rndSpawnLocation, Random.rotation);
        }

        private void OnDrawGizmos() {
            Gizmos.DrawLine(new Vector3(transform.position.x - spawnDistanceFromCentre, transform.position.y, transform.position.z),
                            new Vector3(transform.position.x + spawnDistanceFromCentre, transform.position.y, transform.position.z));
        }
    }
}