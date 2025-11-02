using UnityEngine;

public class Health : MonoBehaviour {
  public ObservableValue<int> health = new(1);
  public ObservableValue<int> maxHealth = new(1);
}