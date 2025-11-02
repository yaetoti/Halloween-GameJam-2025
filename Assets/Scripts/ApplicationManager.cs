public class ApplicationManager : GlobalSingleton<ApplicationManager> {
  private AppState m_appState;

  protected override void OnInitializeSingleton() {
    base.OnInitializeSingleton();

    // TODO start in menu
    m_appState = AppState.GAME;
  }

  public AppState GetAppState() {
    return m_appState;
  }

  public void SetAppState(AppState state) {
    // TODO logic
    
    m_appState = state;
  }
}