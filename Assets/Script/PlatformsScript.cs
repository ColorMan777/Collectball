using UnityEngine;

public class PlatformsScript : MonoBehaviour
{

    public GameObject pos1;
    public GameObject pos2;
    public float wait_time = 1f;
    public float speed = 1f;

    private bool toPos2 = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = pos1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (toPos2){

            move(pos2, Time.deltaTime);

        }
        else
        {
            move(pos1, Time.deltaTime);
        }
        
    }

    void move(GameObject target, float time){
        
        if (Vector3.Distance(transform.position, target.transform.position) >= 0.1f){

            Vector3 direction = (target.transform.position - transform.position).normalized;
            
            transform.position += direction * time * speed;

        }
        else{
            invertMove();
            
        }
        
    }

    void invertMove(){
        toPos2 = !toPos2;
    }

}
