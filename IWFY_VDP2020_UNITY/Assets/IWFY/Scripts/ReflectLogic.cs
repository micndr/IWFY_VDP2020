using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectLogic : MonoBehaviour {

    public float maxRange = 200;
    public int maxDepth = 20;
    public GameObject ReflectRayPrefab;

    int depth = 0;

    public void Start() {
        Shoot(transform.position, transform.forward);
    }

    public void Shoot (Vector3 start, Vector3 heading) {
        depth = 0;
        ShootRec(start, heading);
    }

    public void ShootRec (Vector3 start, Vector3 heading) {
        // recursive function, main loop of shoot
        if (depth > maxDepth) { return; }
        RaycastHit hit;
        Ray ray = new Ray(start, heading);
        //LayerMask layerMask = 1 << LayerMask.NameToLayer("IgnoreRaycast");
        LayerMask layerMask = LayerMask.GetMask("IgnoreRaycast");
        if (Physics.Raycast(ray, out hit, maxRange)) {
            Vector3 reflect = Vector3.Reflect((hit.point-start).normalized, hit.normal);
            depth++; // keep track of depth of recursion
            constrRay(start, hit.point);
            if (hit.transform.gameObject.tag == "Reflect") {
                ShootRec(hit.point, reflect);
            }
            if (hit.transform.gameObject.tag == "ReflectEnd") {
                Triggerer trigger = hit.transform.gameObject.GetComponent<Triggerer>();
                if (trigger) trigger.Trigger();
            }
        } else {
            Vector3 oth = start + heading * maxRange;
            constrRay(start, oth);
        }
    }

    public void constrRay (Vector3 p0, Vector3 p1) {
        // construct ray
        GameObject obj = Instantiate(ReflectRayPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.parent = transform;
        LineRenderer line = obj.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, p0);
        line.SetPosition(1, p1);
    }

    void Update() {
        foreach (Transform child in transform) { Destroy(child.gameObject); }
        Shoot(transform.position, transform.forward);
    }
}
