using UnityEngine;

[CreateAssetMenu(menuName = "AI/Strategies/ZombieMenuStrategy", fileName = "ZombieMenuStrategy", order = 0)]
public class ZombieMenuStrategy : BrainStrategy {
  private Zombie m_zombie;
  private HealthSystem m_targetHealth;
  
  // TODO if we target door and lure was placed - update
  
  public override void Initialize(GameObject parent, Brain brain) {
    base.Initialize(parent, brain);
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
      if (targetPosition.HasValue) {
        // Update target
        m_targetHealth = targetHealth;
        m_zombie.SetTarget(targetPosition, targetHealth);
      }
    }
  }

  public override void OnFixedUpdate() {
    base.OnFixedUpdate();
  }
}