﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldGenerator))]
[CanEditMultipleObjects]
public class WorldGeneratorEditor : Editor
{
    WorldGenerator cont;

    //SerializedProperty divisions;

    void OnEnable()
    {
        cont = target as WorldGenerator;
        //divisions = serializedObject.FindProperty("divisions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        //EditorGUILayout.PropertyField(divisions);
        if (GUILayout.Button("Generate Hex Grid")) cont.GenerateSubdividedHex();
        if (GUILayout.Button("Generate Quad")) cont.GenerateQuad();
        if (GUILayout.Button("Generate Tile")) cont.GenerateTile();
        if (GUILayout.Button("Generate Tile At Cursor")) cont.GenerateTileAtCursor();

        EditorGUILayout.Space();
        if (GUILayout.Button("Remove Random Edge")) cont.RemoveRandomEdge();
        if (GUILayout.Button("Remove Edges")) cont.RemoveEdges();
        if (GUILayout.Button("Subdivide")) cont.Subdivide();
        if (GUILayout.Button("Squarify Quads")) cont.SquarifyQuads();
        if (GUILayout.Button("Compute Skin")) cont.ComputeSkin();
        if (GUILayout.Button("Compute Raycast Mesh")) cont.ComputeRaycastMesh();

        EditorGUILayout.Space();
        if (GUILayout.Button("Compute Wfc Grid")) cont.ComputeWfcGrid();
        if (GUILayout.Button("Compute Exclusion Classes")) cont.ComputeExclusionClasses();

        EditorGUILayout.Space();
        if (GUILayout.Button("Validate Tile")) cont.ValidateTile();
        if (GUILayout.Button("Clear")) cont.Clear();
        if (GUILayout.Button("Run Tests")) cont.Test();
        serializedObject.ApplyModifiedProperties();
    }
}
