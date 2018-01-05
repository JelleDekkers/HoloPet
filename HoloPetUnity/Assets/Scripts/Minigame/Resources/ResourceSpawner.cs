using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame {

    public class ResourceSpawner : MonoBehaviour {

        private static ResourceSpawner instance;
        public static ResourceSpawner Instance { get { return instance; } }

        [SerializeField] private Resource[] resourcePrefabs;
        [SerializeField] private float timeBetweenSpawnsMin = 1, timeBetweenSpawnsMax = 2;
        [SerializeField] private float spawnDistanceFromCentre = 5;

        public List<Resource> Resources { get; private set; }

        private float spawnTimer;

        private void Awake() {
            instance = this;
            Resources = new List<Resource>();
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
            Vector3 rndSpawnLocation = transform.position;
            rndSpawnLocation.x += Random.Range(-spawnDistanceFromCentre, spawnDistanceFromCentre);
            Resource r = Instantiate(resourcePrefabs.GetRandom(), rndSpawnLocation, Random.rotation);
            Resources.Add(r);
        }

        public void RemoveResource(Resource r) {
            Resources.Remove(r);
        }

        private void OnDrawGizmos() {
            Gizmos.DrawLine(new Vector3(transform.position.x - spawnDistanceFromCentre, transform.position.y, transform.position.z),
                            new Vector3(transform.position.x + spawnDistanceFromCentre, transform.position.y, transform.position.z));
        }
    }
}