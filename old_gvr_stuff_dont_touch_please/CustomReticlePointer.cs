using UnityEngine;
using UnityEngine.EventSystems;

// This is modified version of GvrReticlePointer from google. All due rights to them.
// Draws a circular reticle in front of any object that the user points at.
// The circle dilates if the object is clickable.
public class Customized
{
    public const float RETICLE_MIN_INNER_ANGLE = 0.0f; // In degrees.
    public const float RETICLE_MIN_OUTER_ANGLE = 0.5f; // In degrees.
    public const float RETICLE_GROWTH_ANGLE = 1.5f; // In degrees.
    public const float RETICLE_DISTANCE_MIN = 0.45f; // In meters.
    public float maxReticleDistance = 20.0f; // In meters.

    public int reticleSegments = 20; // Number of segments making the reticle circle.
    public float reticleGrowthSpeed = 8.0f; // Growth speed multiplier for the reticle.

    [Range(-32767, 32767)] public int reticleSortingOrder = 32767; // To use in the reticle's renderer.

    public Material MaterialComp { private get; set; } // Gets or sets the material used to render the reticle.
    public float ReticleInnerAngle { get; private set; } //Gets the current inner angle of the reticle (in degrees).
    public float ReticleOuterAngle { get; private set; } // Gets the current outer angle of the reticle (in degrees).
    public float ReticleDistanceInMeters { get; private set; } // Gets the current distance of the reticle (in meters).
    public float ReticleInnerDiameter { get; private set; } // Gets the current inner and outer diameters of the reticle, before distance multiplication.
    public float ReticleOuterDiameter { get; private set; } // Gets the current outer diameter of the reticle (in meters).
    public float MaxPointerDistance { get { return maxReticleDistance; } }

    public void OnPointerEnter(RaycastResult raycastResultResult, bool isInteractive)
    {
        SetPointerTarget(raycastResultResult.worldPosition, isInteractive);
    }

    public void OnPointerHover(RaycastResult raycastResultResult, bool isInteractive)
    {
        SetPointerTarget(raycastResultResult.worldPosition, isInteractive);
    }

    public void OnPointerExit(GameObject previousObject)
    {
        ReticleDistanceInMeters = maxReticleDistance;
        ReticleInnerAngle = RETICLE_MIN_INNER_ANGLE;
        ReticleOuterAngle = RETICLE_MIN_OUTER_ANGLE;
    }

    public void GetPointerRadius(out float enterRadius, out float exitRadius)
    {
        float min_inner_angle_radians = Mathf.Deg2Rad * RETICLE_MIN_INNER_ANGLE;

        float max_inner_angle_radians =
            Mathf.Deg2Rad * (RETICLE_MIN_INNER_ANGLE + RETICLE_GROWTH_ANGLE);

        enterRadius = 2.0f * Mathf.Tan(min_inner_angle_radians);
        exitRadius = 2.0f * Mathf.Tan(max_inner_angle_radians);
    }

    // Updates the material based on the reticle properties.
    public void UpdateDiameters()
    {
        ReticleDistanceInMeters =
      Mathf.Clamp(ReticleDistanceInMeters, RETICLE_DISTANCE_MIN, maxReticleDistance);

        if (ReticleInnerAngle < RETICLE_MIN_INNER_ANGLE)
        {
            ReticleInnerAngle = RETICLE_MIN_INNER_ANGLE;
        }

        if (ReticleOuterAngle < RETICLE_MIN_OUTER_ANGLE)
        {
            ReticleOuterAngle = RETICLE_MIN_OUTER_ANGLE;
        }

        float inner_half_angle_radians = Mathf.Deg2Rad * ReticleInnerAngle * 0.5f;
        float outer_half_angle_radians = Mathf.Deg2Rad * ReticleOuterAngle * 0.5f;

        float inner_diameter = 2.0f * Mathf.Tan(inner_half_angle_radians);
        float outer_diameter = 2.0f * Mathf.Tan(outer_half_angle_radians);

        ReticleInnerDiameter =
      Mathf.Lerp(ReticleInnerDiameter, inner_diameter, Time.unscaledDeltaTime * reticleGrowthSpeed);
        ReticleOuterDiameter =
      Mathf.Lerp(ReticleOuterDiameter, outer_diameter, Time.unscaledDeltaTime * reticleGrowthSpeed);

        MaterialComp.SetFloat("_InnerDiameter", ReticleInnerDiameter * ReticleDistanceInMeters);
        MaterialComp.SetFloat("_OuterDiameter", ReticleOuterDiameter * ReticleDistanceInMeters);
        MaterialComp.SetFloat("_DistanceInMeters", ReticleDistanceInMeters);
    }

    protected void Start()
    {
        base.Start();

        Renderer rendererComponent = GetComponent<Renderer>();
        rendererComponent.sortingOrder = reticleSortingOrder;

        MaterialComp = rendererComponent.material;

        CreateReticleVertices();
    }

    private void Awake()
    {
        ReticleInnerAngle = RETICLE_MIN_INNER_ANGLE;
        ReticleOuterAngle = RETICLE_MIN_OUTER_ANGLE;
    }

    private void Update()
    {
        UpdateDiameters();
    }

    // Sets the reticle pointer's target.
    private bool SetPointerTarget(Vector3 target, bool interactive)
    {
        if (PointerTransform == null)
        {
            Debug.LogWarning("Cannot operate on a null pointer transform");
            return false;
        }

        Vector3 targetLocalPosition = PointerTransform.InverseTransformPoint(target);

        ReticleDistanceInMeters = Mathf.Clamp(targetLocalPosition.z,
                                              RETICLE_DISTANCE_MIN,
                                              maxReticleDistance);
        if (interactive)
        {
            ReticleInnerAngle = RETICLE_MIN_INNER_ANGLE + RETICLE_GROWTH_ANGLE;
            ReticleOuterAngle = RETICLE_MIN_OUTER_ANGLE + RETICLE_GROWTH_ANGLE;
        }
        else
        {
            ReticleInnerAngle = RETICLE_MIN_INNER_ANGLE;
            ReticleOuterAngle = RETICLE_MIN_OUTER_ANGLE;
        }

        return true;
    }

    private void CreateReticleVertices()
    {
        Mesh mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = mesh;

        int segments_count = reticleSegments;
        int vertex_count = (segments_count + 1) * 2;

#region Vertices

        Vector3[] vertices = new Vector3[vertex_count];

        const float kTwoPi = Mathf.PI * 2.0f;
        int vi = 0;
        for (int si = 0; si <= segments_count; ++si)
        {
            // Add two vertices for every circle segment: one at the beginning of the
            // prism, and one at the end of the prism.
            float angle = (float)si / (float)segments_count * kTwoPi;

            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);

            vertices[vi++] = new Vector3(x, y, 0.0f); // Outer vertex.
            vertices[vi++] = new Vector3(x, y, 1.0f); // Inner vertex.
        }
#endregion

#region Triangles
        int indices_count = (segments_count + 1) * 3 * 2;
        int[] indices = new int[indices_count];

        int vert = 0;
        int idx = 0;
        for (int si = 0; si < segments_count; ++si)
        {
            indices[idx++] = vert + 1;
            indices[idx++] = vert;
            indices[idx++] = vert + 2;

            indices[idx++] = vert + 1;
            indices[idx++] = vert + 2;
            indices[idx++] = vert + 3;

            vert += 2;
        }
#endregion

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateBounds();
#if !UNITY_5_5_OR_NEWER
        // Optimize() is deprecated as of Unity 5.5.0p1.
        mesh.Optimize();
#endif  // !UNITY_5_5_OR_NEWER
    }
}
