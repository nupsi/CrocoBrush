using CrocoBrush.Managers;
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

namespace CrocoBrush.Editors
{
    public class FakeSceneWindow : EditorWindow
    {
        private FakeScene[] m_scenes;
        private Vector2 m_scroll;

        [MenuItem("CrocoBrush/FakeSceneViewer")]
        public static void ShowWindow()
        {
            GetWindow<FakeSceneWindow>("Fake Scene Viewer");
        }

        private void OnEnable()
        {
            m_scroll = new Vector2();
        }

        private void OnGUI()
        {
            if(m_scenes == null)
            {
                UpdateScenes();
            }
            if(GUILayout.Button("Find Fake Scenes"))
            {
                UpdateScenes();
            }
            if(m_scenes.Length == 0)
            {
                EditorGUILayout.LabelField("No Fake Scenes", EditorStyles.boldLabel);
                return;
            }
            m_scroll = EditorGUILayout.BeginScrollView(m_scroll);
            foreach(var scene in m_scenes)
            {
                var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
                var type = scene.GetType();
                var sceneName = type.GetField("m_name", flags).GetValue(scene);
                var behavious = type.GetField("m_behaviour", flags).GetValue(scene) as Behaviour[];
                var gameObjects = type.GetField("m_gameObjects", flags).GetValue(scene) as GameObject[];
                EditorGUILayout.ObjectField(scene.gameObject, typeof(GameObject), true);
                EditorGUILayout.LabelField($"Scene Name: {sceneName}");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Behavious: {behavious.Length}");
                EditorGUILayout.LabelField($"Game Objects: {gameObjects.Length}");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField($"Child Count: {scene.gameObject.transform.childCount}");
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndScrollView();
        }

        private void UpdateScenes()
        {
            m_scenes = Resources.FindObjectsOfTypeAll<FakeScene>();
        }
    }
}