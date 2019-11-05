using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class TransformInspector : Editor
{
    private Transform m_Target;

    public override void OnInspectorGUI()
    {
        if(!m_Target) { m_Target = (Transform)target; }

        EditorGUI.indentLevel = 0;

        Vector3 position;
        Vector3 eulerAngles;
        Vector3 scale;

        EditorGUILayout.BeginHorizontal();
        {
            position = EditorGUILayout.Vector3Field("Position", m_Target.localPosition);
            if(GUILayout.Button("X")) { position = Vector3.zero; }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            eulerAngles = EditorGUILayout.Vector3Field("Rotation", m_Target.localEulerAngles);
            if(GUILayout.Button("X")) { eulerAngles = Vector3.zero; }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            scale = EditorGUILayout.Vector3Field("Scale", m_Target.localScale);
            if(GUILayout.Button("X")) { scale = Vector3.one; }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if(GUILayout.Button("Reset Transform"))
        {
                Undo.RegisterCompleteObjectUndo(m_Target, "Reset Transforms " + m_Target.name);
                
                position = Vector3.zero;
                eulerAngles = Vector3.zero;
                scale = Vector3.one;
        }

        if(GUI.changed)
        {
            Undo.RegisterCompleteObjectUndo(m_Target,"Transform Change");
            
            m_Target.localPosition = FixIfNaN(position);
            m_Target.localEulerAngles = FixIfNaN(eulerAngles);
            m_Target.localScale = FixIfNaN(scale);
            
            EditorUtility.SetDirty(m_Target);
        }
    }
    
    private Vector3 FixIfNaN(Vector3 v)
    {
        if(float.IsNaN(v.x)) { v.x = 0.0f; }
        if(float.IsNaN(v.y)) { v.y = 0.0f; }
        if(float.IsNaN(v.z)) { v.z = 0.0f; }
        
        return v;
    }
}