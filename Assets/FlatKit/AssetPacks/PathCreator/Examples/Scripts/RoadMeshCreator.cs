using UnityEngine;

namespace PathCreation.Examples {
    public class RoadMeshCreator : PathSceneTool
    {
        [SerializeField] private bool _isWhithoutMesh = false;
        [Header ("Road settings")]
        [SerializeField] private float _roadWidth = 4f;
        [SerializeField] private float _thickness = 10f;
        [SerializeField] private bool _flattenSurface;
        [Header ("Material settings")]
        [SerializeField] private Material _roadMaterial;
        [SerializeField] private Material _undersideMaterial;
        [SerializeField] private float _textureTiling = 8;

        [SerializeField] private Transform _parentOfRoadsMesh;
        [SerializeField] private GameObject _meshHolder;

        private Mesh _mesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        public float RoadWidth => _roadWidth;

        protected override void PathUpdated()
        {
            if (pathCreator != null && _isWhithoutMesh == false)
            {
                AssignMeshComponents();
                AssignMaterials();
                CreateRoadMesh();
                AssignMeshCollider();
            }
        }

        void CreateRoadMesh()
        {
            Vector3[] verts = new Vector3[path.NumPoints * 8];
            Vector2[] uvs = new Vector2[verts.Length];
            Vector3[] normals = new Vector3[verts.Length];

            int numTris = 2 * (path.NumPoints - 1) + ((path.isClosedLoop) ? 2 : 0);
            int[] roadTriangles = new int[numTris * 3];
            int[] underRoadTriangles = new int[numTris * 3];
            int[] sideOfRoadTriangles = new int[numTris * 2 * 3];

            int vertIndex = 0;
            int triIndex = 0;

            // Vertices for the top of the road are layed out:
            // 0  1
            // 8  9
            // and so on... So the triangle map 0,8,1 for example, defines a triangle from top left to bottom left to bottom right.
            int[] triangleMap = { 0, 8, 1, 1, 8, 9 };
            int[] sidesTriangleMap = { 4, 6, 14, 12, 4, 14, 5, 15, 7, 13, 15, 5 };

            bool usePathNormals = !(path.space == PathSpace.xyz && _flattenSurface);

            for (int i = 0; i < path.NumPoints; i++)
            {
                Vector3 localUp = (usePathNormals) ? Vector3.Cross(path.GetTangent(i), path.GetNormal(i)) : path.up;
                Vector3 localRight = (usePathNormals) ? path.GetNormal(i) : Vector3.Cross(localUp, path.GetTangent(i));

                // Find position to left and right of current path vertex
                Vector3 vertSideA = path.GetPoint(i) - localRight * Mathf.Abs(_roadWidth);
                Vector3 vertSideB = path.GetPoint(i) + localRight * Mathf.Abs(_roadWidth);

                // Add top of road vertices
                verts[vertIndex + 0] = vertSideA;
                verts[vertIndex + 1] = vertSideB;
                // Add bottom of road vertices
                verts[vertIndex + 2] = vertSideA - localUp * _thickness;
                verts[vertIndex + 3] = vertSideB - localUp * _thickness;

                // Duplicate vertices to get flat shading for sides of road
                verts[vertIndex + 4] = verts[vertIndex + 0];
                verts[vertIndex + 5] = verts[vertIndex + 1];
                verts[vertIndex + 6] = verts[vertIndex + 2];
                verts[vertIndex + 7] = verts[vertIndex + 3];

                // Set uv on y axis to path time (0 at start of path, up to 1 at end of path)
                uvs[vertIndex + 0] = new Vector2(0, path.times[i]);
                uvs[vertIndex + 1] = new Vector2(1, path.times[i]);

                // Top of road normals
                normals[vertIndex + 0] = localUp;
                normals[vertIndex + 1] = localUp;
                // Bottom of road normals
                normals[vertIndex + 2] = -localUp;
                normals[vertIndex + 3] = -localUp;
                // Sides of road normals
                normals[vertIndex + 4] = -localRight;
                normals[vertIndex + 5] = localRight;
                normals[vertIndex + 6] = -localRight;
                normals[vertIndex + 7] = localRight;

                // Set triangle indices
                if (i < path.NumPoints - 1 || path.isClosedLoop)
                {
                    for (int j = 0; j < triangleMap.Length; j++)
                    {
                        roadTriangles[triIndex + j] = (vertIndex + triangleMap[j]) % verts.Length;
                        // reverse triangle map for under road so that triangles wind the other way and are visible from underneath
                        underRoadTriangles[triIndex + j] = (vertIndex + triangleMap[triangleMap.Length - 1 - j] + 2) % verts.Length;
                    }
                    for (int j = 0; j < sidesTriangleMap.Length; j++)
                    {
                        sideOfRoadTriangles[triIndex * 2 + j] = (vertIndex + sidesTriangleMap[j]) % verts.Length;
                    }

                }

                vertIndex += 8;
                triIndex += 6;
            }

            int[] frontSide = new[] { 0, 1, 2, 3, 2, 1 };
            int[] sides = new int[12];

            for (int i = 0; i < frontSide.Length; i++)
            {
                sides[i] = frontSide[i];
                sides[i + 6] = verts.Length - frontSide[5 - i] - 1;
            }

            _mesh.Clear();
            _mesh.vertices = verts;
            _mesh.uv = uvs;
            _mesh.normals = normals;
            _mesh.subMeshCount = 4;
            _mesh.SetTriangles(roadTriangles, 0);
            _mesh.SetTriangles(underRoadTriangles, 1);
            _mesh.SetTriangles(sideOfRoadTriangles, 2);
            _mesh.SetTriangles(sides, 3);
            _mesh.RecalculateBounds();
        }

        // Add MeshRenderer and MeshFilter components to this gameobject if not already attached
        void AssignMeshComponents()
        {
            if (_meshHolder == null)
            {
                _meshHolder = new GameObject("Road Mesh Holder");
                _meshHolder.transform.parent = _parentOfRoadsMesh;
                _meshHolder.AddComponent<RoadTrigger>();
            }

            _meshHolder.transform.rotation = Quaternion.identity;
            _meshHolder.transform.position = Vector3.zero;
            _meshHolder.transform.localScale = Vector3.one;

            // Ensure mesh renderer and filter components are assigned
            if (!_meshHolder.gameObject.GetComponent<MeshFilter>())
            {
                _meshHolder.gameObject.AddComponent<MeshFilter>();
            }
            if (!_meshHolder.GetComponent<MeshRenderer>())
            {
                _meshHolder.gameObject.AddComponent<MeshRenderer>();
            }

            _meshRenderer = _meshHolder.GetComponent<MeshRenderer>();
            _meshFilter = _meshHolder.GetComponent<MeshFilter>();
            if (_mesh == null)
            {
                _mesh = new Mesh();
            }
            _meshFilter.sharedMesh = _mesh;
        }

        void AssignMeshCollider()
        {

            _meshHolder.transform.rotation = Quaternion.identity;
            _meshHolder.transform.position = Vector3.zero;
            _meshHolder.transform.localScale = Vector3.one;

            if (_meshHolder.TryGetComponent(out MeshCollider _) == false)
            {
                _meshHolder.AddComponent<MeshCollider>();
            }
        }

        void AssignMaterials()
        {
            if (_roadMaterial != null && _undersideMaterial != null)
            {
                Material newMaterial = new Material(_roadMaterial);
                _meshRenderer.sharedMaterials = new Material[] { newMaterial, _undersideMaterial, _undersideMaterial, _undersideMaterial };
                _meshRenderer.sharedMaterials[0].mainTextureScale = new Vector3(1, pathCreator.path.length / _textureTiling);
            }
        }

    }
}
