using UnityEngine;
//using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{

    private static MusicScript instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // There’s already one — destroy this duplicate
            Destroy(gameObject);
        }
    }
    
    
    void Start()
    {
        if (GlobalStats.Instance != null){ //useless because there's no player in main meny and GlobalSTats is on player

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