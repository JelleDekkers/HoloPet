using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Minigame {

    public static class Resources {

        private static List<Resource> allResources = new List<Resource>();
        public static List<Resource> AllResources {
            get { return allResources; }
            private set { allResources = value; }
        }

        public static void Add(Resource r) {
            AllResources.Add(r);
        }

        public static void Remove(Resource r) {
            AllResources.Remove(r);
        }

        public static void Clear() {
            AllResources = null;
        }

        public static Resource GetNearestResource(Transform t) {
            float closestDist = Mathf.Infinity;
            Resource closestResource = null;
            foreach (Resource r in allResources) {
                float dist = Vector3.Distance(r.transform.position, t.position);
                if (dist < closestDist) {
                    closestDist = dist;
                    closestResource = r;
                }
            }
            return closestResource;
        }

        public static Resource GetNearestResourceWithColor(EmotionColor color, Vector3 position) {
            Emotion correspondingEmotion = (Emotion)((int)color);
            List<Resource> resourcesWithCorrespondingColor = AllResources.Where(x => x.emotion == correspondingEmotion).ToList();

            float closestDist = Mathf.Infinity;
            Resource closestResource = null;
            foreach (Resource r in resourcesWithCorrespondingColor) {
                float dist = Vector3.Distance(r.transform.position, position);
                if (dist < closestDist) {
                    closestDist = dist;
                    closestResource = r;
                }
            }

            return closestResource;
        }

        public static Resource GetNearestResourceHorizontal(Transform t, bool getRight) {
            List<Resource> correspondingResources = AllResources.Where(x => Common.IsRightFromObject(t, x.transform) == getRight).ToList();
            float closestDist = Mathf.Infinity;
            Resource closestResource = null;
            foreach (Resource r in correspondingResources) {
                //if ((getRight && !Common.IsRightFromObject(t, r.transform)) || (!getRight && Common.IsRightFromObject(t, r.transform)))
                //    continue;

                float dist = Vector3.Distance(r.transform.position, t.position);
                if (dist < closestDist) {
                    closestDist = dist;
                    closestResource = r;
                }
            }
            return closestResource;
        }
    }
}