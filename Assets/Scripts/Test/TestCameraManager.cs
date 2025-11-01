using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(TestCameraManager))]
public class TestCameraManagerEditor : Editor {
  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    if (GUILayout.Button("Move to garage")) {
      CameraManager.Instance.MoveToLocation(RoomType.GARAGE);
    }
    
    if (GUILayout.Button("Move to living room")) {
      CameraManager.Instance.MoveToLocation(RoomType.LIVING_ROOM);
    }
  }
}

#endif

public class TestCameraManager : MonoBehaviour {
}
