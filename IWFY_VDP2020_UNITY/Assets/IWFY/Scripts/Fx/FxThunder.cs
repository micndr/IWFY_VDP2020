using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxThunder : MonoBehaviour {

    public GameObject ThunderBranchPrefab;
    LineRenderer lineRenderer;

    public float deathTimer = 0.2f;

    public float branchProbability = 0.8f;
    public float branchLenght = 2;
    public float branchLenghtSpread = 1f;
    public float branchEndSpread = 0.5f;
    public float branchUpBias = 1f;
    public float height = 7;
    public float spread = 3;
    public float segmentSpread = 3;

    List<GameObject> shocks = new List<GameObject>();

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update() {
        if (deathTimer < Time.time) {
            DestroyThunder();
        }
    }

    void GenerateBranch (Vector3 pos, Vector3 end) {
        GameObject branch = Instantiate(ThunderBranchPrefab, transform);
        branch.transform.SetParent(transform);
        LineRenderer lr = branch.GetComponent<LineRenderer>();
        float segspread = end.magnitude * 0.05f;
        var vs = GeneratePath(pos, end.normalized, end.magnitude, new Vector3(0, 0, 0), 0.2f, 7);
        UpdateRenderer(lr, vs);
    }

    public void GenerateThunder (Vector3 pos, Vector3 heading) {
        deathTimer += Time.time;

        var vs = GeneratePath(pos, heading, height, new Vector3(spread, spread, spread), segmentSpread, 20);
        lineRenderer = GetComponent<LineRenderer>();
        UpdateRenderer(lineRenderer, vs);

        for (int i = 0; i < vs.Count; i++) {
            if (i != 0 && i < vs.Count - 1) {
                float heightbias = (float)i / vs.Count;
                if (Random.Range(0, 1.0f) < branchProbability * heightbias) {
                    int dir = Random.Range(0, 2);
                    Vector3 bespread = new Vector3(
                        Random.Range(-branchEndSpread, branchEndSpread),
                        Random.Range(-branchEndSpread, branchEndSpread),
                        Random.Range(-branchEndSpread, branchEndSpread));
                    Vector3 dst = (Vector3.Cross(heading, bespread) + heading * branchUpBias)
                        * (branchLenght + Random.Range(-branchLenghtSpread, branchLenghtSpread))
                        * heightbias;
                    GenerateBranch(vs[i], dst);
                }
            }
        }
    }

    List<Vector3> GeneratePath (Vector3 start, Vector3 head, float h, Vector3 targetSpread, float segmentSpread, int segments) {
        Vector3 targetPos = start + new Vector3(
            Random.Range(-targetSpread.x, targetSpread.x),
            Random.Range(-targetSpread.y, targetSpread.y),
            Random.Range(-targetSpread.z, targetSpread.z));

        List<Vector3> vs = new List<Vector3>();
        for (int i = 0; i < segments; i++) {
            Vector3 spread = new Vector3(
                Random.Range(-segmentSpread, segmentSpread),
                Random.Range(-segmentSpread, segmentSpread),
                Random.Range(-segmentSpread, segmentSpread));
            if (spread.magnitude < 0.5 * segmentSpread) spread = Vector3.zero;
            if (i == 0 || i == segments - 1) spread = Vector3.zero;
            vs.Add(targetPos + (head/segments)* h * i + spread);
        }
        return vs;
    }

    void UpdateRenderer (LineRenderer lr, List<Vector3> vs) {
        Vector3[] lp = new Vector3[vs.Count];
        for (int i = 0; i < vs.Count; i++) {
            lp[i] = new Vector3(vs[i].x, vs[i].y, vs[i].z);
        }
        lr.positionCount = vs.Count;
        lr.SetPositions(lp);
    }

    public void DestroyThunder () {
        lineRenderer.positionCount = 0;
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }
}
