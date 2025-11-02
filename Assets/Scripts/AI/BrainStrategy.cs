using UnityEngine;

public abstract class BrainStrategy : ScriptableObject {
  protected Brain m_brain;
  protected GameObject m_parent;
  
  // One-timer
  public virtual void Initialize(GameObject parent, Brain brain) {
    m_brain = brain;
    m_parent = parent;
  }
  
  // Run every time we activate or leave a strategy
  public virtual void OnStart() { }
  public virtual void OnStop() { }
  public virtual void OnUpdate() { }
  public virtual void OnFixedUpdate() { }
}