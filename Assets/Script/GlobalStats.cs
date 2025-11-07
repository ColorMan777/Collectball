using UnityEngine;

public class GlobalStats : MonoBehaviour


{
    
    public static GlobalStats Instance { get; private set; }

    public int max_items = 0;
    public int collected_items = 0;
    public GameObject musicPlayer;

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
             Destroy(this); 
         } 
        else 
        { 
            Instance = this; 
        } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
