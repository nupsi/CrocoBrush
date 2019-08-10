using CrocoBrush.Managers;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CrocoBrush.Editors
{
    public class FakeSceneWindow : EditorWindow
    {
        private readonly BindingFlags m_flags = BindingFlags.Instance
            | BindingFlags.NonPublic
            | BindingFlags.Static;

        private FakeScene[] m_scenes;
        private Activator[] m_activators;
        private Vector2 m_scroll;
        private bool m_showScenes;
        private bool m_showActivators;

        [MenuItem("CrocoBrush/FakeSceneViewer")]
        public static void ShowWindow()
        {
            GetWindow<FakeSceneWindow>("Fake Scene Viewer");
        }

        protected void OnEnable()
        {
            m_scroll = new Vector2();
        }

        protected void OnGUI()
        {
            Check();
            m_scroll = EditorGUILayout.BeginScrollView(m_scroll);
            Button("Find Fake Scenes", UpdateScenes);
            Button("Find Activaros", UpdateActivators);
            DrawSceneInfo();
            DrawActivatorInfo();
            EditorGUILayout.EndScrollView();
        }

        private void Check()
        {
            if(m_scenes == null)
            {
                UpdateScenes();
            }

            if(m_activators == null)
            {
                UpdateActivators();
            }
        }

        private void DrawSceneInfo()
        {
            if(m_scenes.Length == 0)
            {
                EditorGUILayout.LabelField("No Fake Scenes", EditorStyles.boldLabel);
            }
            else
            {
                DrawFakeSceneList();
            }
        }

        private void DrawFakeSceneList()
        {
            m_showScenes = EditorGUILayout.Foldout(m_showScenes, "Scenes");
            if(!m_showScenes)
            {
                return;
            }

            foreach(var scene in m_scenes)
            {
                var type = scene.GetType();
                var sceneName = type.GetField("m_name", m_flags).GetValue(scene);
                var behavious = type.GetField("m_behaviour", m_flags).GetValue(scene) as Behaviour[];
                var gameObjects = type.GetField("m_gameObjects", m_flags).GetValue(scene) as GameObject[];
                EditorGUILayout.ObjectField(scene.gameObject, typeof(GameObject), true);
                EditorGUILayout.LabelField($"Scene Name: {sceneName}");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Behavious: {behavious.Length}");
                EditorGUILayout.LabelField($"Game Objects: {gameObjects.Length}");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField($"Child Count: {scene.gameObject.transform.childCount}");
                EditorGUILayout.Space();
            }
        }

        private void DrawActivatorInfo()
        {
            if(m_activators.Length == 0)
            {
                EditorGUILayout.LabelField("No Activaros in Scene", EditorStyles.boldLabel);
            }
            else
            {
                DrawActivatorList();
            }
        }

        private void DrawActivatorList()
        {
            m_showActivators = EditorGUILayout.Foldout(m_showActivators, "Activators");
            if(!m_showActivators)
            {
                return;
            }

            foreach(var activator in m_activators)
            {
                var type = activator.GetType();
                var separate = (bool)type.GetField("m_separate", m_flags).GetValue(activator);
                var name = (string)type.GetField("m_name", m_flags).GetValue(activator);
                var canvas = separate ? (string)type.GetField("m_canvas", m_flags).GetValue(activator) : name;
                var position = separate ? (string)type.GetField("m_position", m_flags).GetValue(activator) : name;
                var text = name.Length + canvas.Length + position.Length == 0
                    ? "'Back'"
                    : name == canvas && name == position
                        ? $"{name}"
                        : string.Format("{0} ({1}, {2})",
                            name.Length == 0 ? "-" : name,
                            canvas.Length == 0 ? "-" : canvas,
                            position.Length == 0 ? "-" : position);
                Button(text, () => Selection.activeGameObject = activator.gameObject);
            }
        }

        private void UpdateScenes() => m_scenes = Resources.FindObjectsOfTypeAll<FakeScene>();

        private void UpdateActivators() => m_activators = Resources.FindObjectsOfTypeAll<Activator>();

        private void Button(string text, Action action)
        {
            if(GUILayout.Button(text))
            {
                action.Invoke();
            }
        }
    }
}