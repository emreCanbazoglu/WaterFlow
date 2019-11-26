using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Grid myScript = (Grid)target;

        if (GUILayout.Button("Increase Height Level"))
        {
            myScript.IncreaseLevel(1);
        }
        if (GUILayout.Button("Decrease Height Level"))
        {
            myScript.IncreaseLevel(-1);
        }
    }
}
