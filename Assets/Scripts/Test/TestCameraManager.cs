using System;
using UnityEditor;
using UnityEngine;

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

public class TestCameraManager : MonoBehaviour {
}
