using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public string next_scene_name;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(){
        DontDestroyOnLoad(GlobalStats.Instance.musicPlayer);
        SceneManager.LoadScene(next_scene_name);
    }
}
