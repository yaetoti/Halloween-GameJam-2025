using System.Collections.Generic;

// I HEV NO BREIN. I WILL MEIK ONE
public class Brain {
  public abstract class BrainStrategy {
    public virtual void Update() { }
  }

  private Dictionary<string, BrainStrategy> m_strategies = new();
  private BrainStrategy m_activeStrategy;

  public Brain() {
  }

  public void AddStrategy(string name, BrainStrategy strategy) {
    m_strategies.Add(name, strategy);
  }

  public void SetStrategy(string name) {
    m_activeStrategy = m_strategies[name];
  }

  public void Update() {
    m_activeStrategy.Update();
  }
}