using UnityEditor;

namespace CrocoBrush.Editors
{
    [CustomEditor(typeof(Activator))]
    public class ActivatorInspector : Editor
    {
        private Activator m_target;

        private void OnEnable()
        {
            m_target = Target;
        }

        public override void OnInspectorGUI()
        {
            var separate = serializedObject.FindProperty("m_separate").boolValue;
            serializedObject
                .FindProperty("m_separate")
                .boolValue = EditorGUILayout.Toggle("Separate Targets", separate);

            if(separate)
            {
                TextField("Target Canvas", "m_canvas");
                TextField("Target Position", "m_position");
            }
            else
            {
                TextField("Target", "m_name");
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void TextField(string label, string target)
        {
            var text = serializedObject.FindProperty(target).stringValue;
            serializedObject
                .FindProperty(target)
                .stringValue = EditorGUILayout.TextField(label, text);
        }

        private Activator Target => (Activator)target;
    }
}