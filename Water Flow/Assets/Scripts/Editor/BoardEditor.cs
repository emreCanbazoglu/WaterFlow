﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Board myScript = (Board)target;

        if (GUILayout.Button("Create Board"))
        {
            myScript.CreateBoard();
        }
    }
}
