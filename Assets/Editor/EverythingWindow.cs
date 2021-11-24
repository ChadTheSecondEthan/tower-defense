using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TowerWindow : EditorWindow
{
    [MenuItem("Window/Everything Window")]
    public static void ShowWindow()
    {
        GetWindow<TowerWindow>();
    }

    public void OnGUI()
    {
        GUILayout.Label("Towers");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Destroy selected"))
        {
            foreach (GameObject go in Selection.gameObjects)
                if (go.TryGetComponent(out Tower _))
                    DestroyImmediate(go);
        }
        if (GUILayout.Button("Destroy all"))
        {
            foreach (Tower tower in FindObjectsOfType<Tower>())
                DestroyImmediate(tower.gameObject);
        }
        if (GUILayout.Button("Select all"))
        {
            List<Tower> towers = new List<Tower>(FindObjectsOfType<Tower>());
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (Tower tower in towers)
                gameObjects.Add(tower.gameObject);
            Selection.objects = gameObjects.ToArray();
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Enemies");
        if (GUILayout.Button("Select all enemies"))
        {
            List<Enemy> enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (Enemy enemy in enemies)
                gameObjects.Add(enemy.gameObject);
            Selection.objects = gameObjects.ToArray();
        }
    }
}