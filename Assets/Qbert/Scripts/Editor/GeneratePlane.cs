using UnityEngine;
using System.Collections;
using UnityEditor;

public class GeneratePlane
{
    private static float width = 10.0f;
    private static float height = 10.0f;

    [MenuItem("Assets/Generate plane")]
    public static void Generate()
    {
        Mesh mesh = new Mesh();

        var vertices = new Vector3[4];
        var uvRect = new Rect(0, 0, 1, 1);
        var wOver2 = (width / 2f);
        var hOver2 = (height / 2f);
        vertices[0] = new Vector3(-wOver2, -hOver2);
        vertices[1] = new Vector3(-wOver2, hOver2);
        vertices[2] = new Vector3(wOver2, hOver2);
        vertices[3] = new Vector3(wOver2, -hOver2);

        var triangles = new[] { 0, 1, 2, 0, 2, 3 };
        var meshUVs = new[] {
                new Vector2(uvRect.xMin, uvRect.yMin),
                new Vector2(uvRect.xMin, uvRect.yMax),
                new Vector2(uvRect.xMax, uvRect.yMax),
                new Vector2(uvRect.xMax, uvRect.yMin),
            };
        mesh.vertices = vertices;
        mesh.uv = meshUVs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        AssetDatabase.CreateAsset(mesh, "Assets/Mesh.asset");
    }
}
