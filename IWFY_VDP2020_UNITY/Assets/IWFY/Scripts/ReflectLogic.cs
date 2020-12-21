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
        if (depth > maxDepth) { return; }
        RaycastHit hit;
        Ray ray = new Ray(start, heading);
        LayerMask layerMask = 1 << LayerMask.NameToLayer("pause");
        if (Physics.Raycast(ray, out hit, maxRange, ~layerMask)) {
            if (hit.transform.gameObject.tag == "Reflect") {
                Vector3 reflect = Vector3.Reflect(start.normalized, hit.normal);
                depth++;
                constrRay(start, hit.point);
                ShootRec(hit.point, reflect);
            }
            if (hit.transform.gameObject.tag == "ReflectEnd") {
                Debug.Log("Win");
                Triggerer trigger = hit.transform.gameObject.GetComponent<Triggerer>();
                if (trigger) trigger.Trigger();
            }
        } else {
            Vector3 oth = start + heading * maxRange;
            constrRay(start, oth);
        }
    }

    public void constrRay (Vector3 p0, Vector3 p1) {
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
