using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tower)), CanEditMultipleObjects]
public class TowerEditor : Editor
{
    void OnSceneGUI()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            if (!go.TryGetComponent(out Tower tower))
                continue;

            Handles.color = tower.canShoot ? Color.green : Color.red;
            Handles.DrawWireDisc(go.transform.position + (go.transform.forward * tower.range), go.transform.forward, tower.range);
        }
    }
}