using UnityEngine;

public class OrbitVisualizer : MonoBehaviour
{
    LineRenderer lineRenderer;

    public int segments;
    public float orbitSize;
    public float yOffset;

    private void Start()
    {
        //Get linerenderer
        lineRenderer = GetComponent<LineRenderer>();

        //set orbit size to the distance of the sun
        orbitSize = transform.position.z * 1;
    }

    private void Update()
    {
        SetVisualOrbit();
    }

    void SetVisualOrbit()
    {
        //create a array that will contain all points that will make up the circle
        Vector3[] points = new Vector3[segments + 1];
        //set the points around the the calculated cirle based on how many segments you have.
        //So if you have 100 segments there will be placed 100 segments across the calculated circle
        for (int i = 0; i < segments; i++)
        {
            float angle = ((float)i / segments) * 360 * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * orbitSize;
            float z = Mathf.Sin(angle) * orbitSize;

            points[i] = new Vector3(x, -yOffset, z);
        }
        points[segments] = points[0];

        //set the positionCount to the amount of segments
        lineRenderer.positionCount = segments + 1;
        //set the points
        lineRenderer.SetPositions(points);
    }
}
