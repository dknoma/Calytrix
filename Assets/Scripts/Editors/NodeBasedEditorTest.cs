using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editors {
    public class NodeBasedEditorTest : EditorWindow {
        private IList<Node> nodes;
        private GUIStyle nodeStyle;

        private void OnEnable() {
            nodeStyle = new GUIStyle {
                normal = {
                    background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D
                },
                border = new RectOffset(12, 12, 12, 12)
            };
        }

        [MenuItem("Window/Node Based Editor")]
        private static void OpenWindow() {
            NodeBasedEditorTest window = GetWindow<NodeBasedEditorTest>();
            window.titleContent = new GUIContent("Node Based Editor");
        }

        private void OnGUI() {
            DrawNodes();

            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);

            if(GUI.changed) Repaint();
        }

        private void DrawNodes() {
            if(nodes != null) {
                for(int i = 0; i < nodes.Count; i++) {
                    nodes[i].Draw();
                }
            }
        }

        private void ProcessEvents(Event e) {
            switch(e.type) {
                case EventType.MouseDown:
                    if(e.button == 1) {
                        ProcessContextMenu(e.mousePosition);
                    }

                    break;
            }
        }

        private void ProcessNodeEvents(Event e) {
            if(nodes != null) {
                for(int i = nodes.Count - 1; i >= 0; i--) {
                    bool guiChanged = nodes[i].ProcessEvents(e);

                    if(guiChanged) {
                        GUI.changed = true;
                    }
                }
            }
        }

        private void ProcessContextMenu(Vector2 mousePosition) {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
            genericMenu.ShowAsContext();
        }

        private void OnClickAddNode(Vector2 mousePosition) {
            if(nodes == null) {
                nodes = new List<Node>();
            }

            nodes.Add(new Node(mousePosition, 200, 50, nodeStyle));
        }
    }
}