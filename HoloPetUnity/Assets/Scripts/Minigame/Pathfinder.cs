using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Minigame;

public class Pathfinder : MonoBehaviour {

    public BezierSpline spline;
    public PetHead walker;

    private void Update() {
        if(Input.GetMouseButtonDown(0))
            SetNewTargetPosition(GetMousePositionInScene());
    }

    public void SetNewTargetPosition(Vector3 pos) {
        spline.endPoints[spline.Count - 1].position = Pet.Instance.Head.transform.position;
        spline.InsertNewPointAtWorldPosition(spline.Count, pos);
        spline.RemovePointAt(0);
        walker.ResetProgress();
        //CreateVisualMesh();
    }

    public void SetNewTargetAnticipatingObjectPosition(Transform target, Vector3 direction, float speed) {
        float travelTime = spline.Length / walker.movementSpeed;
        Vector3 futurePosition = target.transform.position + (direction * (speed * travelTime));
        SetNewTargetPosition(futurePosition);
    }

    private void CreateVisualMesh(Vector3 pos) {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pos.y = -0.3f;
        g.GetComponent<Renderer>().material.color = Color.black;
        g.transform.position = pos;
        g.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(g.GetComponent<Collider>());
    }

    public const float MAX_RAY_DISTANCE = 100000f;

    public Vector3 GetMousePositionInScene(float y = 0) {
        Plane plane = new Plane(Vector3.up, y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance = MAX_RAY_DISTANCE;
        if (plane.Raycast(ray, out rayDistance)) {
            Vector3 hitPoint = ray.GetPoint(rayDistance);
            return hitPoint;
        }
        return Vector3.zero;
    }
}
