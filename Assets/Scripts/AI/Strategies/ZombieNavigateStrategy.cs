using UnityEngine;

[CreateAssetMenu(menuName = "AI/Strategies/ZombieNavigateStrategy", fileName = "ZombieNavigateStrategy", order = 0)]
public class ZombieNavigateStrategy : BrainStrategy {
  public enum TargetType {
    DOOR,
    LURE
  }
  
  private ZombieMemoryPlay m_memory;
  private Zombie m_zombie;
  private HealthSystem m_targetHealth;
  private TargetType m_targetType;
  
  
  // TODO if we target door and lure was placed - update
  
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

    // TODO simple way
    if (m_targetHealth && m_targetType == TargetType.DOOR && EntityManager.Instance.LureObjects.Count != 0) {
      if (FindClosestLure(out var targetHealth, out var targetPosition)) {
        m_targetType = TargetType.LURE;
        m_targetHealth = targetHealth;
        m_zombie.SetTarget(targetPosition, targetHealth);
      }
    }
    
    // If no target - update
    if (m_targetHealth is null || !m_targetHealth) {
      // Find the closest door (lol)
      // Find the closest lure

      if (FindClosestLure(out var targetHealth, out var targetPosition)) {
        m_targetType = TargetType.LURE;
      }
      else {
        // If no lures - go to the door
        targetHealth = GameManager.Instance.GetDoorHealth();
        targetPosition = targetHealth.transform.position;
        m_targetType = TargetType.DOOR;
      }
      
      // Update target
      m_targetHealth = targetHealth;
      m_zombie.SetTarget(targetPosition, targetHealth);
    }
  }

  public override void OnFixedUpdate() {
    base.OnFixedUpdate();
  }
  
  public bool FindClosestLure(out HealthSystem targetHealth, out Vector3? targetPosition) {
    targetHealth = null;
    targetPosition = null;
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

    return targetHealth is not null;
  }
}