using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(HealthSystem))]
public class HealthCustomEditor : Editor {
  public override void OnInspectorGUI() {
    base.OnInspectorGUI();
    var health = (HealthSystem)target;

    if (GUILayout.Button("Damage")) {
      health.Health -= 1;
    }
    
    if (GUILayout.Button("Heal")) {
      health.Health += 1;
    }
  }
}

#endif