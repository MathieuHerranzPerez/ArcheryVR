using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuizzEditorWrap))]
public class QuizzEditor : Editor
{
    public QuizzEditorWrap current
    {
        get
        {
            return (QuizzEditorWrap) target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Save"))
            current.SaveJson();
    }
}
