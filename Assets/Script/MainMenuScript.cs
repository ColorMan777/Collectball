using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject musicPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame(){
        DontDestroyOnLoad(musicPlayer);
        SceneManager.LoadSceneAsync("Level01");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop Play Mode
        #else
        Application.Quit();
        #endif
    }

}
