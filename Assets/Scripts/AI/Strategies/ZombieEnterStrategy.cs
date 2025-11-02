using UnityEngine;

[CreateAssetMenu(menuName = "AI/Strategies/ZombieEnterStrategy", fileName = "ZombieEnterStrategy", order = 0)]
public class ZombieEnterStrategy : BrainStrategy {
  private ZombieMemoryPlay m_memory;
  private Zombie m_zombie;
  private Vector3 m_targetPos;
  
  public override void Initialize(GameObject parent, Brain brain) {
    base.Initialize(parent, brain);
    m_memory = parent.GetComponent<ZombieMemoryPlay>();
    m_zombie = parent.GetComponent<Zombie>();
  }

  public override void OnStart() {
    base.OnStart();
    m_targetPos = new(m_memory.enterMarkerX.position.x, m_parent.transform.position.y, m_parent.transform.position.z);
    // X-Only movement
    m_zombie.state.AddListener(OnZombieStateChange);
    m_zombie.SetTarget(m_targetPos);
  }

  private void OnZombieStateChange(Zombie.State state) {
    if (state != Zombie.State.PURSUING) {
      m_brain.SetStrategy("navigate");
    }
  }

  public override void OnStop() {
    base.OnStop();
    m_zombie.state.RemoveListener(OnZombieStateChange);
    m_zombie.SetTarget();
  }
}