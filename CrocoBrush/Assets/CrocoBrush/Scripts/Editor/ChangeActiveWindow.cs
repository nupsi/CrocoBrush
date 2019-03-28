using CrocoBrush.UI.Menu;
using UnityEditor;
using UnityEngine;

namespace CrocoBrush.Editors
{
    public class ChangeActiveViewer : EditorWindow
    {
        private ChangeActive[] m_components;
        private Vector2 m_scroll;

        [MenuItem("CrocoBrush/ChangeActiveViewer")]
        public static void ShowWindow()
        {
            GetWindow<ChangeActiveViewer>("Change Active Viewer");
        }

        private void OnEnable()
        {
            m_scroll = new Vector2();
            UpdateComponents();
        }

        private void OnGUI()
        {
            if(m_components == null)
            {
                UpdateComponents();
            }

            if(GUILayout.Button("Update Components"))
            {
                UpdateComponents();
            }

            if(m_components.Length == 0)
            {
                EditorGUILayout.LabelField("No Components", EditorStyles.boldLabel);
                return;
            }
            m_scroll = EditorGUILayout.BeginScrollView(m_scroll);
            foreach(var component in m_components)
            {
                if(component == null)
                {
                    UpdateComponents();
                    break;
                }

                EditorGUILayout.Space();
                EditorGUIUtility.labelWidth = 150;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(GetFullPath(component.gameObject), EditorStyles.boldLabel);
                EditorGUILayout.ObjectField(component.gameObject, typeof(GameObject), true);
                EditorGUIUtility.labelWidth = 10;
                EditorGUILayout.LabelField("");
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.labelWidth = 50;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Changes From:");
                EditorGUILayout.ObjectField(component.ObjectToDeactive, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Changes To:");
                EditorGUILayout.ObjectField(component.ObjectToActive, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField(
                    string.Format("'{0}' under '{1}' changes from '{2}' to '{3}'",
                        component.name,
                        component.transform.parent.name,
                        component.ObjectToDeactive.name,
                        component.ObjectToActive.name));
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndScrollView();
        }

        private string GetFullPath(GameObject gameObject)
        {
            var path = "/" + gameObject.name;
            while(gameObject.transform.parent != null)
            {
                gameObject = gameObject.transform.parent.gameObject;
                path = "/" + gameObject.name + path;
            }
            return path;
        }

        private void UpdateComponents()
        {
            m_components = Resources.FindObjectsOfTypeAll<ChangeActive>();
        }
    }
}