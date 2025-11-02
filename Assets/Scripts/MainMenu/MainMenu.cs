using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    public bool MuteMusic;
    public bool MuteAudio;

    void OnEnable()
    {
        DontDestroyOnLoad(audio.gameObject);
        DontDestroyOnLoad(gameObject);
        MuteAudio = false;
        MuteMusic = false;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToggleMusic()
    {
        MuteMusic = !MuteMusic;
        audio.mute = MuteMusic;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
