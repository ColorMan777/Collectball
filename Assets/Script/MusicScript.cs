using UnityEngine;
//using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GlobalStats.Instance != null){

            GlobalStats.Instance.musicPlayer = gameObject;

        }
        
    
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalStats.Instance != null){

            if (GlobalStats.Instance.musicPlayer == null){
                
                GlobalStats.Instance.musicPlayer = gameObject;
            }


        }
    }

}