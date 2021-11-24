using UnityEditor;
using UnityEngine;

public class CreateGrid : EditorWindow
{
    public GameObject squarePrefab;
    public GameObject squaresParent;
    public float size = 12f;
    public float spacing = 1f;
    public int tileCount = 10;

    [MenuItem("Window/Grid/Create Grid")]
    public static void ShowWindow()
    {
        CreateGrid grid = GetWindow<CreateGrid>();
        grid.squaresParent = GameObject.Find("Squares");
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Tile Prefab");
        squarePrefab = (GameObject)EditorGUILayout.ObjectField(squarePrefab, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Tiles Parent");
        squaresParent = (GameObject)EditorGUILayout.ObjectField(squaresParent, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        size = EditorGUILayout.FloatField("Grid Size", size);
        spacing = EditorGUILayout.FloatField("Spacing Between Squares", spacing);
        tileCount = EditorGUILayout.IntField("Tiles Per Side", tileCount);

        if (GUILayout.Button("Create Grid"))
            Create();

        if (GUILayout.Button("Destroy Grid"))
            Destroy();
    }

    void Create()
    {
        Destroy();

        Transform t = squaresParent.transform;
        if (t.childCount != 0) return;

        float spacePerTile = size / (tileCount + spacing);

        for (int x = 0; x < tileCount; x++)
        {
            for (int y = 0; y < tileCount; y++)
            {
                GameObject square = Instantiate(squarePrefab, t);
                square.transform.position = new Vector3(
                    spacing + x * spacePerTile - size * 0.5f,
                    spacing + y * spacePerTile - size * 0.5f, 0);
            }
        }
    }

    void Destroy()
    {
        while (squaresParent.transform.childCount > 0)
            DestroyImmediate(squaresParent.transform.GetChild(0).gameObject);
    }
}
