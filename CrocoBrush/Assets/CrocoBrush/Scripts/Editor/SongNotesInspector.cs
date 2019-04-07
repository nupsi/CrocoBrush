using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CrocoBrush.Editors
{
    [CustomEditor(typeof(SongNotes))]
    public class SongNotesInspector : Editor
    {
        private SongNotes m_target;
        private ReorderableList m_list;
        private bool m_default;
        private Rect m_enumRect;
        private Rect m_textRect;
        private float m_enumWidth;
        private float m_textWidth;
        private int m_current;
        private int m_shown = 30;

        private void OnEnable()
        {
            m_target = Target;
            m_list = new ReorderableList(serializedObject, serializedObject.FindProperty("Nodes"), false, true, false, false)
            {
                drawHeaderCallback = (Rect rect) => EditorGUI.LabelField(rect, "Notes"),
                drawElementCallback = DrawElementCallback,
                elementHeightCallback = ElementHeightCallback
            };
        }

        private float ElementHeightCallback(int index)
        {
            return (index >= (m_current * m_shown) && index <= (m_current * m_shown) + m_shown)
                ? (UseSingleLine)
                    ? EditorGUIUtility.singleLineHeight * 1.2f
                    : EditorGUIUtility.singleLineHeight * 2.3f
                : 0;
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            if(index >= (m_current * m_shown) && index <= (m_current * m_shown) + m_shown)
            {
                rect.y += 3;
                var textRect = TextRect(rect);
                EditorGUIUtility.labelWidth = EnumRect(rect).width / 3;
                var content = new GUIContent("Direction");
                var direction = m_target.Nodes[index].Direction;
                EditorGUI.LabelField(rect, $"{index}");
                m_target.Nodes[index].Direction = (Direction)EditorGUI.EnumPopup(EnumRect(rect), content, direction);
                EditorGUI.LabelField(textRect, new GUIContent($"Time {m_target.Nodes[index].Time.ToString("n3")}"));
                textRect.x += textRect.width;
                EditorGUI.LabelField(textRect, new GUIContent($"Delay {m_target.Nodes[index].Delay}"));
            }
        }

        private Rect EnumRect(Rect rect)
        {
            if(rect.width != m_enumWidth)
            {
                var padding = 30;
                m_enumRect = new Rect
                {
                    x = rect.x + padding,
                    y = rect.y,
                    width = (UseSingleLine ? rect.width / 2f : rect.width) - padding,
                    height = rect.height
                };

                m_enumWidth = rect.width;
            }
            else
            {
                m_enumRect.y = rect.y;
            }
            return m_enumRect;
        }

        private Rect TextRect(Rect rect)
        {
            if(rect.width != m_textWidth)
            {
                m_textRect = new Rect
                {
                    x = UseSingleLine ? rect.x + (rect.width / 2) : rect.x,
                    y = UseSingleLine ? rect.y : rect.y + EditorGUIUtility.singleLineHeight,
                    width = UseSingleLine ? rect.width / 4 : rect.width / 2,
                    height = rect.height
                };
                m_textWidth = rect.width;
            }
            else
            {
                m_textRect.y = UseSingleLine ? rect.y : rect.y + EditorGUIUtility.singleLineHeight;
            }
            return m_textRect;
        }

        private AnimationCurve CreateCurve()
        {
            var curve = new AnimationCurve();
            var nodes = m_target.Nodes;
            for(int i = 0; i < nodes.Count; i++)
            {
                curve.AddKey(new Keyframe(i, nodes[i].Delay, 0, 0, 0, 0));
            }
            return curve;
        }

        public override void OnInspectorGUI()
        {
            m_default = EditorGUILayout.Toggle("Draw Defaul Inspector", m_default);
            if(m_default)
            {
                DrawDefaultInspector();
                return;
            }

            if(GUILayout.Button("Analyze"))
            {
                Analyze();
            }
            EditorGUILayout.CurveField(CreateCurve());
            EditorGUILayout.LabelField("Song Notes", EditorStyles.boldLabel);
            if(GUILayout.Button("Calculate Delays"))
            {
                m_target.CalculateDelays();
                EditorUtility.SetDirty(target);
            }
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Generate Note Directions"))
            {
                GenerateNoteDirections();
                EditorUtility.SetDirty(target);
            }
            if(GUILayout.Button("Reset"))
            {
                ResetNoteDirections();
                EditorUtility.SetDirty(target);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("<"))
            {
                if(m_current > 0)
                {
                    m_current--;
                }
            }
            var max = Mathf.Ceil(m_target.Nodes.Count / m_shown);
            GUILayout.Label($"{m_current}/{max}");
            if(GUILayout.Button(">"))
            {
                if(m_current < max)
                {
                    m_current++;
                }
            }
            EditorGUILayout.EndHorizontal();
            m_list.DoLayoutList();
            if(GUILayout.Button("Clear Nodes"))
            {
                m_target.Nodes.Clear();
                m_current = 0;
                EditorUtility.SetDirty(target);
                Repaint();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void Analyze()
        {
            var total = 0f;
            var nodes = m_target.Nodes;
            for(int i = 0; i < nodes.Count; i++)
            {
                var left = nodes.Count - i;
                total += nodes[i].Delay;
                Debug.Log($"Node {i}. Nodes left {left}");

                if(left > 8)
                {
                    var sum = 0f;
                    for(int j = 0; j < 8; j++)
                    {
                        sum += nodes[i + j].Delay;
                    }
                    Debug.Log($"\tNext 8 -> Sum: {sum} Avg. {sum / 8}");
                }

                if(left > 4)
                {
                    var sum = 0f;
                    for(int j = 0; j < 4; j++)
                    {
                        sum += nodes[i + j].Delay;
                    }
                    Debug.Log($"\tNext 4 -> Sum: {sum} Avg. {sum / 4}");
                }
            }

            Debug.Log($"Total Stats: Delay(Sum: {total} Avg: {total / nodes.Count})");
        }

        private void GenerateNoteDirections()
        {
            if(m_target.Nodes == null || m_target.Nodes.Count == 0)
            {
                return;
            }
            var nodes = m_target.Nodes;
            var currentDirection = Direction.None;
            for(int i = 0; i < nodes.Count; i++)
            {
                var current = nodes[i];
                if(current.Direction == Direction.None)
                {
                    if(currentDirection == Direction.None)
                    {
                        currentDirection = RandomDirection;
                    }

                    if(current.Direction == Direction.None)
                    {
                        current.Direction = currentDirection;
                        do
                        {
                            currentDirection = RandomDirection;
                        }
                        while(current.Direction == currentDirection);
                    }

                    var previous = i > 0 ? nodes[i - 1] : new SongNode(Direction.None, -1);
                    var next = i < nodes.Count - 1 ? nodes[i + 1] : new SongNode(Direction.None, -1);

                    if(next.Delay > 0 && next.Delay < 0.3f)
                    {
                        next.Direction = current.Direction;
                    }
                }
            }
        }

        private Direction RandomDirection
        {
            get
            {
                switch(Random.Range(0, 4))
                {
                    case 0:
                        return Direction.Up;

                    case 1:
                        return Direction.Down;

                    case 2:
                        return Direction.Left;

                    case 3:
                        return Direction.Right;

                    default:
                        return Direction.None;
                }
            }
        }

        private void ResetNoteDirections()
        {
            m_target.Nodes.ForEach((node) => node.Direction = Direction.None);
        }

        private SongNotes Target => (SongNotes)target;
        private bool UseSingleLine => !(EditorGUIUtility.currentViewWidth < 365);
    }
}