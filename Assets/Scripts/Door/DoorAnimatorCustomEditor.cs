using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(DoorAnimator))]
public class DoorAnimatorCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DoorAnimator animator = (DoorAnimator)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Open"))
        {
            animator.OpenDoorAnimation();
        }
    }
}

#endif