using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CrocoBrush.Editors
{
    /// <summary>
    /// Custom inspector for direction input base class to draw a reorderable list for the inputs.
    /// </summary>
    [CustomEditor(typeof(DirectionInputReader), true)]
    public class DirectionInputReaderInspector : Editor
    {
        /// <summary>
        /// Current reorderable list.
        /// </summary>
        private ReorderableList m_list;

        protected void OnEnable()
        {
            m_list = new ReorderableList(serializedObject, serializedObject.FindProperty("m_input"), true, true, true, true)
            {
                drawHeaderCallback = (Rect rect) => DrawHeaderCallback(rect, "Inputs"),
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused)
                    => DrawElementCallback(rect, serializedObject.FindProperty("m_input").GetArrayElementAtIndex(index)),
                elementHeight = EditorGUIUtility.singleLineHeight * 2.5f
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            m_list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draw header for reorderable list at given position with the given text.
        /// </summary>
        /// <param name="rect">Header position.</param>
        /// <param name="text">Header text.</param>
        private void DrawHeaderCallback(Rect rect, string text)
        {
            EditorGUI.LabelField(rect, text);
        }

        /// <summary>
        /// Draw Direction input.
        /// </summary>
        /// <param name="rect">Current position.</param>
        /// <param name="property">Current property.</param>
        private void DrawElementCallback(Rect rect, SerializedProperty property)
        {
            rect.y += 2;
            rect.height = EditorGUIUtility.singleLineHeight;
            var keybind = property.FindPropertyRelative("Keybind");
            var direction = property.FindPropertyRelative("Direction");
            EditorGUI.PropertyField(rect, keybind, new GUIContent("Keybind"));
            rect.y += EditorGUIUtility.singleLineHeight + 2;
            EditorGUI.PropertyField(rect, direction, new GUIContent("Direction"));
        }
    }
}