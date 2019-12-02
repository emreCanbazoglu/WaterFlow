using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridLevelController))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridLevelController myScript = (GridLevelController)target;

        if (GUILayout.Button("Increase Height Level"))
        {
            myScript.TryIncreaseLevel(1);
        }
        if (GUILayout.Button("Decrease Height Level"))
        {
            myScript.TryIncreaseLevel(-1);
        }
    }
}
