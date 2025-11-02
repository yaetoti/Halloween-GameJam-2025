using UnityEngine;

[CreateAssetMenu(menuName = "AI/Strategies/ZombieNavigateStrategy", fileName = "ZombieNavigateStrategy", order = 0)]
public class ZombieNavigateStrategy : BrainStrategy {
  private ZombieMemoryPlay m_memory;
  private Zombie m_zombie;
  private HealthSystem m_targetHealth;
  
  // TODO algorithm
  // Go to the closest target
  // If target does not exist anymore - change target
  
  // TODO if we target door and lure was placed - update
  
  // Priorities
  // Lure (any)
  // Door
  
  // SpawnManager
  
  public override void Initialize(GameObject parent, Brain brain) {
    base.Initialize(parent, brain);
    m_memory = parent.GetComponent<ZombieMemoryPlay>();
    m_zombie = parent.GetComponent<Zombie>();
  }

  public override void OnStart() {
    base.OnStart();
  }

  public override void OnStop() {
    base.OnStop();
  }

  public override void OnUpdate() {
    base.OnUpdate();

    // If no target - update
    if (m_targetHealth is null || !m_targetHealth) {
      // Find the closest door (lol)
      // Find the closest lure

      HealthSystem targetHealth = null;
      Vector3? targetPosition = null;
      float closestDistance = float.PositiveInfinity;

      // Find closest lure
      foreach (var lure in EntityManager.Instance.LureObjects) {
        //Debug.Log("vibe check");
        var lureHealth = lure.transform.GetChild(0).GetComponent<HealthSystem>();
        var lurePosition = lure.transform.position;
        float lureDistance = Vector3.Distance(m_zombie.transform.position, lurePosition);
        
        if (lureDistance < closestDistance) {
          //Debug.Log("check passed");
          closestDistance = lureDistance;
          targetHealth = lureHealth;
          targetPosition = lurePosition;
        }
      }
      
      // If no lures - go to the door
      if (!targetPosition.HasValue) {
        // Door
        targetHealth = GameManager.Instance.GetDoorHealth();
        targetPosition = targetHealth.transform.position;
      }
      
      // Update target
      m_targetHealth = targetHealth;
      m_zombie.SetTarget(targetPosition, targetHealth);
    }
  }

  public override void OnFixedUpdate() {
    base.OnFixedUpdate();
  }
}