﻿using UnityEditor;
using UnityEngine;

namespace Tilemaps {
    [CustomEditor(typeof(TiledImporter))]
    public class TiledImporterEditor : Editor {
        
        SerializedProperty overwriteTilesetAssets;
        SerializedProperty overwriteLevelGrid;
 
        void OnEnable()
        {
            overwriteTilesetAssets = serializedObject.FindProperty("overwriteTilesetAssets");
            overwriteLevelGrid = serializedObject.FindProperty("overwriteLevelGrid");
        }
        public override void OnInspectorGUI() {
            // Allows for default fields to be drawn
            DrawDefaultInspector();
            
            TiledImporter importer = (TiledImporter) target;
            GUIStyle style = new GUIStyle {richText = true};
            
            DrawLineAndHeader($"<b>Tiled Tilemap JSON Processing - {importer.Json}.json</b>", style);
            
            EditorGUILayout.PropertyField(overwriteLevelGrid, new GUIContent("Overwrite Grid"));
            EditorGUILayout.PropertyField(overwriteTilesetAssets, new GUIContent("Overwrite Tiles"));
            
            if (GUILayout.Button("Process JSON")) {
                importer.ProcessJsonThenBuild();
            }
            
            DrawLineAndHeader("<b>Individual Imports</b>", 
                              $"<b>Tiled Tilemap JSON Processing - {importer.Json}.json</b>", 
                              style);
            
            if (GUILayout.Button("Process JSON")) {
                importer.ProcessJSON();
            }
            
            EditorGUILayout.PropertyField(overwriteLevelGrid, new GUIContent("Overwrite Grid"));
            
            if (GUILayout.Button("Build Tileset")) {
                importer.BuildTileset();
            }
            
            DrawLineAndHeader("<b>Prefab and Asset Creation</b>", style);
            
            EditorGUILayout.PropertyField(overwriteTilesetAssets, new GUIContent("Overwrite"));
            
            if (GUILayout.Button("Process Sprites From Spritesheets")) {
                importer.ProcessSpritesFromSheetIfNecessary();
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private static void DrawLineAndHeader(string header, GUIStyle style) {
            DrawUILine(Color.grey);
            EditorGUILayout.LabelField(header, style);
        }
        
        private static void DrawLineAndHeader(string header1, string header2, GUIStyle style) {
            DrawUILine(Color.grey);
            EditorGUILayout.LabelField(header1, style);
            EditorGUILayout.LabelField(header2, style);
        }
        
        private static void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
            r.height = thickness;
            r.y+=padding/2;
            r.x-=2;
            r.width +=6;
            EditorGUI.DrawRect(r, color);
        }
    }
}
