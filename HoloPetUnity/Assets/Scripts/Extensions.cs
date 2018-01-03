using UnityEngine;

public static class Extensions {

    public static T GetRandom<T>(this T[] array) {
        int rnd = Random.Range(0, array.Length);
        return array[rnd];
    }
}
