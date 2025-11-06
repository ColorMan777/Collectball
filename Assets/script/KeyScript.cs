using UnityEngine;

public class KeyScript : MonoBehaviour
{

    void OnEnable()
    {
        GlobalStats.Instance.max_items += 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GlobalStats.Instance.max_items += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete(){

        Destroy(gameObject);

    }
}
