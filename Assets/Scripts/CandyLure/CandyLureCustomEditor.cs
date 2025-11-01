using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(CandyLure))]
public class CandyLureCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CandyLure candyLure = (CandyLure)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Deal damage"))
        {
            candyLure.Damage();
        }
    }
}

#endif