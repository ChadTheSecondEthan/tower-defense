using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tile), true), CanEditMultipleObjects]
public class TileEditor : Editor
{
    public Tower tower;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!EachSelectionIsOfType<Tile>()) return;

        GUILayout.BeginHorizontal();

            if (GUILayout.Button("Make Normal"))
                ChangeTilesTo<Tile>("Tile").ForEach(tile => tile.type = Tile.TileType.Normal);

            if (GUILayout.Button("Make Spawn"))
                ChangeTilesTo<Tile>("Spawn Tile").ForEach(tile => tile.type = Tile.TileType.Spawn);

            if (GUILayout.Button("Make Base"))
                ChangeTilesTo<Tile>("Base Tile").ForEach(tile => tile.type = Tile.TileType.Base);

            if (GUILayout.Button("Make Path"))
                ChangeTilesTo<Tile>("Path Tile").ForEach(tile => tile.type = Tile.TileType.Path);

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

            /*if (Selection.gameObjects.Length == 1 && Selection.gameObjects[0].TryGetComponent(out Tile t))
            {
                if (GUILayout.Button("Select neighbours"))
                {
                    Grid grid = FindObjectOfType<Grid>();
                    Selection.objects = new List<Tile>(grid.GetNeighbours(t)).ConvertAll(tile => tile.gameObject).ToArray();
                }
            }*/

            if (GUILayout.Button("Select towers"))
            {
                List<GameObject> towerGameObjects = new List<GameObject>();
                foreach (GameObject go in Selection.gameObjects)
                {
                    if (go.TryGetComponent(out Tile tile) && tile.tower)
                        towerGameObjects.Add(tile.tower.gameObject);
                }
                Selection.objects = towerGameObjects.ToArray();
            }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        tower = (Tower)EditorGUILayout.ObjectField(tower, typeof(Tower), false);
        if (GUILayout.Button("Add tower to tiles"))
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                if (go.TryGetComponent(out Tile tile) && tile.type == Tile.TileType.Normal)
                {
                    if (tile.tower)
                        tile.DeleteTower();

                    tile.SetTower(tower);
                }
            }
        }
        GUILayout.EndHorizontal();
    }

    bool EachSelectionIsOfType<T>() where T : Component
    {
        foreach (GameObject go in Selection.gameObjects)
            if (!go.TryGetComponent(out T _))
                return false;
        return true;
    }

    List<T> ChangeTilesTo<T>(string name) where T : Tile
    {
        List<T> tiles = new List<T>();

        foreach (GameObject go in Selection.gameObjects)
        {
            Tile tile = go.GetComponent<Tile>();
            go.name = name;
            int x = tile.x;
            int y = tile.y;
            T newTile = go.AddComponent<T>();
            newTile.x = x;
            newTile.y = y;
            DestroyImmediate(tile);
            tiles.Add(newTile);
        }
        return tiles;
    }
}