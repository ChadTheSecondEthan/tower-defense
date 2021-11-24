using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public LevelData toSave;
    public LevelData toLoad;

    public override void OnInspectorGUI()
    {
        Grid grid = (Grid)target;
        if (grid.tileColors == null || grid.tileColors.Length == 0)
            grid.tileColors = new Color[]
            {
                Color.black, Color.grey, Color.yellow, Color.cyan, Color.red
            };

        EditorGUILayout.BeginHorizontal();
        toSave = (LevelData)EditorGUILayout.ObjectField("", toSave, typeof(LevelData), true);
        if (GUILayout.Button("Save Grid Data"))
            grid.SaveGridData(toSave.gridData);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        toLoad = (LevelData) EditorGUILayout.ObjectField("", toLoad, typeof(LevelData), true);
        if (GUILayout.Button("Load Grid Data"))
            grid.LoadGridData(toLoad.gridData);
        EditorGUILayout.EndHorizontal();

        // Tiles
        Tile.TileType[] tileTypes = new Tile.TileType[]
        {
            Tile.TileType.Normal,
            Tile.TileType.Spawn,
            Tile.TileType.Disabled,
            Tile.TileType.Base,
            Tile.TileType.Path,
        };

        Tile[] tiles = ((Grid)target).GetComponentsInChildren<Tile>();
        for (int i = 0; i < tileTypes.Length; i++)
        {
            grid.tileColors[i] = ShowRow(tileTypes[i], grid.tileColors[i]);
            foreach (Tile tile in tiles)
            {
                if (tile.type == tileTypes[i])
                    tile.GetComponent<SpriteRenderer>().color = grid.tileColors[i];
            }
        }

        if (GUILayout.Button("Select All Tiles"))
        {
            List<Object> list = new List<Object>();
            foreach (Tile tile in tiles)
                list.Add(tile.gameObject);
            Selection.objects = list.ToArray();
        }
    }

    Color ShowRow(Tile.TileType type, Color color)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(type.ToString());
        Color tileColor = EditorGUILayout.ColorField(color);
        if (GUILayout.Button("Select All"))
            SelectTiles(type);

        EditorGUILayout.EndHorizontal();

        return tileColor;
    }

    void SelectTiles(Tile.TileType type)
    {
        List<Object> tiles = new List<Object>();
        foreach (Tile tile in FindObjectsOfType<Tile>())
            if (tile.type == type)
                tiles.Add(tile.gameObject);

        Selection.objects = tiles.ToArray();
    }
}