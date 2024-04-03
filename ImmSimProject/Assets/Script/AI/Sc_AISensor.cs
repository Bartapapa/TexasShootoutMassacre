//Found on TheKiwiCoder
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Sc_AISensor : MonoBehaviour
{
    [SerializeField] private float _distance = 10.0f;
    [SerializeField] private float _angle = 30.0f;
    [SerializeField] private float _height = 1.0f;
    [SerializeField] int _scanFrequency = 30;
    [SerializeField] LayerMask _sensorLayers;
    [SerializeField] LayerMask _visibilityOcclusionLayers;
    [SerializeField] private Color _meshColor = Color.red;

    public Collider[] _nearbyColliders = new Collider[50]; // Setup an array to store results of Raycast
    private Mesh _mesh;
    private int _count;
    private float _scanInterval;
    private float _scanTime;
    public List<GameObject> objectsFound = new List<GameObject>();

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2; // 4 tris per segment on far + 2 for top/bottom + 2 for left/right
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices]; //ignoring index in here, hence using numVertices

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * Vector3.forward * _distance;
        Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * Vector3.forward * _distance;

        Vector3 topCenter = bottomCenter + Vector3.up * _height;
        Vector3 topLeft = bottomLeft + Vector3.up * _height;
        Vector3 topRight = bottomRight + Vector3.up * _height;


        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;


        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;


        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -_angle;
        float deltaAngle = (_angle * 2) / segments;

        //Create rounded far side and adjust other sides accordingly
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * _distance;

            topLeft = bottomLeft + Vector3.up * _height;
            topRight = bottomRight + Vector3.up * _height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;


            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void Scan()
    {
        _count = Physics.OverlapSphereNonAlloc(transform.position, _distance, _nearbyColliders, _sensorLayers, QueryTriggerInteraction.Collide);
        objectsFound.Clear();
        for (int i = 0; i < _count; i++)
        {
            GameObject obj = _nearbyColliders[i].gameObject;
            if (IsInSight(obj))
            {
                objectsFound.Add(obj);
            }
        }
    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 destination = obj.transform.position;
        Vector3 direction = destination - origin;

        // Check if is above or below sight
        if (direction.y < 0.0f || direction.y > _height)
        {
            return false;
        }

        // Check if is in angle of sight
        direction.y = 0.0f;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > _angle)
        {
            return false;
        }

        // Check if nothing is blocking lie of sight
        origin.y += _height / 2; // offset origin and destination by half height for the cast
        destination.y = origin.y;
        if (Physics.Linecast(origin, destination, _visibilityOcclusionLayers))
        {
            return false;
        }

        return true;
    }

    private void Start()
    {
        _scanInterval = 1.0f / _scanFrequency;
    }
    private void Update()
    {
        _scanTime -= Time.deltaTime;
        if (_scanTime < 0.0f)
        {
            _scanTime = _scanInterval;
            Scan();
        }
    }
    private void OnValidate()
    {
        _mesh = CreateWedgeMesh();
        _scanInterval = 1.0f / _scanFrequency;
    }

    private void OnDrawGizmos()
    {
        if (_mesh)
        {
            Gizmos.color = _meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
        }

        // Draw near sensor
        Gizmos.DrawWireSphere(transform.position, _distance);
        for (int i = 0; i < _count; i++)
        {
            // Draw found colliders in near sensor
            if (_nearbyColliders[i] != null)
            {
                Gizmos.DrawSphere(_nearbyColliders[i].transform.position, 0.2f);
            }
        }


        Gizmos.color = Color.green;
        foreach (var obj in objectsFound)
        {
            // Draw found colliders in near sensor
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }
}
