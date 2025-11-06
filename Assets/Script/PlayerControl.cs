using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerControl : MonoBehaviour

{
    public float thrust = 10f;
    public float jump_force = 300f;
    public GameObject cam_pivot;
    public int items = 0;
    public TMP_Text itemText;

    private Rigidbody rb;

    [Header("Ground Detection Settings")]
    public float checkDistance = 0.1f;
    public LayerMask groundLayer; // Assign in inspector

    private bool isGrounded;
    private Animation keyAnim;
    private bool inLevelEnd = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(InitializeAfterDelay());
       
    }

    IEnumerator InitializeAfterDelay() //coroutine pour delay le comptage sur toute la map
    {
        yield return new WaitForSeconds(0.1f); // wait 2 seconds
        UpdateItemText();
        //Debug.Log("Initialized after delay");
    }

    void FixedUpdate()
    {
        // Raycast downward from the object's position
        isGrounded = Physics.Raycast(cam_pivot.transform.position, Vector3.down, checkDistance, groundLayer);

/*         if (isGrounded)
            Debug.Log("Grounded ✅");
        else
            Debug.Log("Not grounded ❌"); */

        
        if (Input.GetKey(KeyCode.W)){
            //rb.AddForce(Vector3.forward * thrust);
            rb.AddForce(cam_pivot.transform.forward * thrust);
        }

        if (Input.GetKey(KeyCode.S)){
            //rb.AddForce(-Vector3.forward * thrust);
            rb.AddForce(-cam_pivot.transform.forward * thrust);
        }

        
        if (Input.GetKey(KeyCode.A)){
            rb.AddForce(-cam_pivot.transform.right * thrust);
        }
            
        if (Input.GetKey(KeyCode.D)){
            rb.AddForce(cam_pivot.transform.right * thrust);
        }
            
        

    }

    void Update(){

        // Jump dans update parce que dans fixed update il n'est pas assez réactif
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true){
            rb.AddForce(Vector3.up * jump_force);
            //Debug.Log("Jump");
        }      
                
        //End level
        if (inLevelEnd && Input.GetKeyDown(KeyCode.Space) && GlobalStats.Instance.collected_items == GlobalStats.Instance.max_items){ //check if u finished game + u are in end zone
            //Debug.Log(1);
        }
            
    }

    void OnTriggerEnter(Collider trigger){
        //Debug.Log(trigger.gameObject.layer);
        if (trigger.gameObject.layer == LayerMask.NameToLayer("Collectibles")){
            
            items += 1;
            GlobalStats.Instance.collected_items = items;
            UpdateItemText();

            Animation anim  = trigger.gameObject.GetComponent<Animation>();


            if(trigger.gameObject.GetComponent<KeyScript>().collected != true){ //trigger anim (and prevent double trigger)

            anim.CrossFade("KeyFade", 0.1f);
            //trigger.gameObject.animator.Play("KeyFade");

            trigger.gameObject.GetComponent<KeyScript>().collected = true;

            }


        }

        if (trigger.gameObject.layer == LayerMask.NameToLayer("LevelEnd")){

            inLevelEnd = true;
            AudioSource[] sfx = trigger.gameObject.GetComponents<AudioSource>();
            sfx[0].Play();

        }

    }

    void OnTriggerExit(Collider trigger){

       if (trigger.gameObject.layer == LayerMask.NameToLayer("LevelEnd")){

            inLevelEnd = false;
            AudioSource[] sfx = trigger.gameObject.GetComponents<AudioSource>();
            sfx[1].Play();

        }

    }

    void UpdateItemText(){

        itemText.text = "Items : " + items.ToString() + " / " + GlobalStats.Instance.max_items.ToString();

    }

}
