using Casuak.Extension._01_UnityComponent;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace TinyCeleste._03_Editor.Editor._01_Inspector
{
    [CustomEditor(typeof(Transform))]
    public class FuckTransform : UnityEditor.Editor
    {
        private Transform m_Transform;
        private UnityEditor.Editor m_Editor;

        private void OnEnable()
        {
            m_Editor = CreateEditor(target,
                Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.TransformInspector", true));
        }

        public override void OnInspectorGUI()
        {
            m_Transform = target as Transform;
            if (m_Transform == null) return;
            m_Editor.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("顺时针"))
            {
                Undo.RecordObject(m_Transform, "fuck");
                m_Transform.Rotate2D(-90);
            }
            if (GUILayout.Button("逆时针"))
            {
                Undo.RecordObject(m_Transform, "fuck");
                m_Transform.Rotate2D(90);
            }

            if (GUILayout.Button("清空子"))
            {
                GameObject[] tmp = new GameObject[m_Transform.childCount];
                for (int i = 0; i < m_Transform.childCount; i++)
                {
                    tmp[i] = m_Transform.GetChild(i).gameObject;
                }

                foreach (var gameObject in tmp)
                {
                    Undo.DestroyObjectImmediate(gameObject);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
