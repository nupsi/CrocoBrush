using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace CrocoBrush
{
    [CustomEditor(typeof(SongNotes))]
    public class SongNotesInspector : Editor
    {
        private SongNotes m_target;
        private ReorderableList m_list;
        private bool m_default;

        private void OnEnable()
        {
            m_target = Target;
            m_list = new ReorderableList(serializedObject, serializedObject.FindProperty("Nodes"), true, true, true, false)
            {
                drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Notes"),
                drawElementCallback = DrawElementCallback
            };
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.y += 2;
            rect.width /= 2;
            EditorGUIUtility.labelWidth = 75f;
            var content = new GUIContent("Direction");
            var direction = m_target.Nodes[index].Direction;
            m_target.Nodes[index].Direction = (Direction)EditorGUI.EnumPopup(rect, content, direction);
            rect.x += rect.width;
            rect.width /= 2;
            EditorGUI.LabelField(rect, new GUIContent($"Time {m_target.Nodes[index].Time.ToString("n3")}"));
            rect.x += rect.width;
            EditorGUI.LabelField(rect, new GUIContent($"Delay {m_target.Nodes[index].Delay}"));
        }

        public override void OnInspectorGUI()
        {
            m_default = EditorGUILayout.Toggle("Draw Defaul Inspector", m_default);
            if(m_default)
            {
                DrawDefaultInspector();
                return;
            }

            EditorGUILayout.LabelField("Song Notes", EditorStyles.boldLabel);
            if (GUILayout.Button("Calculate Delays"))
            {
                m_target.CalculateDelays();
                EditorUtility.SetDirty(target);
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate Note Directions"))
            {
                GenerateNoteDirections();
                EditorUtility.SetDirty(target);
            }
            if (GUILayout.Button("Reset"))
            {
                ResetNoteDirections();
                EditorUtility.SetDirty(target);
            }
            EditorGUILayout.EndHorizontal();
            m_list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void GenerateNoteDirections()
        {
            var vertical = Direction.None;
            var horizontal = Direction.None;
            m_target.Nodes.ForEach((node) =>
            {
                node.Direction = (node.Delay < 0.4f)
                    ? (horizontal == Direction.Up)
                        ? (horizontal = Direction.Down)
                        : (horizontal = Direction.Up)
                    : (vertical == Direction.Left)
                        ? (vertical = Direction.Right)
                        : (vertical = Direction.Left);
            });
        }

        private void ResetNoteDirections()
        {
            m_target.Nodes.ForEach((node) => node.Direction = Direction.None);
        }

        private SongNotes Target => (SongNotes) target;
    }
}
