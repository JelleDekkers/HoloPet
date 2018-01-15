﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    public Vector3 sceneSize = new Vector3(5, 7, 5);
    public Vector3 centrePos = new Vector3(0, 0, 3);

    [SerializeField] private BezierSpline spline;
    [SerializeField] public PetHead walker;

    public void SetNewRandomTarget() {
        Vector3 rndPos = Common.GetRandomPositionWithinRange(sceneSize, centrePos);
        SetNewTargetPosition(rndPos);
    }

    public void SetNewTargetPosition(Vector3 pos) {
        walker.enabled = true;
        spline.endPoints[spline.Count - 1].position = walker.transform.position;
        BezierPoint p = spline.InsertNewPointAtWorldPosition(spline.Count, pos);

        Quaternion lookRot = Quaternion.LookRotation(p.position - walker.transform.position, Vector3.forward);
        Vector3 conversion = new Vector3(0, lookRot.eulerAngles.y - 90, 0);
        p.rotation = Quaternion.Euler(conversion);

        spline.RemovePointAt(0);
        if (spline.Count > 2)
            spline.RemovePointAt(0);

        walker.ResetProgress();
    }

    public void Stop() {
        walker.enabled = false;

    }
}
