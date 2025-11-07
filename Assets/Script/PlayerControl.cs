using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

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

    public GameObject endZone;

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

        
        // MOUVEMENTS

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
            AudioSource[] sfx = gameObject.GetComponents<AudioSource>();
            sfx[1].Play();
        }      
                
        //End level
        if (inLevelEnd && Input.GetKeyDown(KeyCode.Space) && GlobalStats.Instance.collected_items == GlobalStats.Instance.max_items){ //check if u finished game + u are in end zone = WINNNNN
            //Debug.Log(1);
            if (endZone.GetComponent<EndLevelScript>().next_scene_name != null){ // check is there is another level after
                AudioSource[] sfx = GetComponents<AudioSource>(); // get audio sources
                sfx[0].Play();

                endZone.GetComponent<Animation>().CrossFade("ExitLevelAnimation", 0.1f); // next level after animation
                rb.AddForce(Vector3.up * jump_force * 3); // go UP
                GetComponent<Animation>().Play("PlayerAnimation"); // dissolve
            }
        }

        if(rb.linearVelocity.magnitude > 0f && isGrounded){ // rolling audio with volume linked to velocity
            //Debug.Log(rb.linearVelocity.magnitude);

            AudioSource[] sfx = gameObject.GetComponents<AudioSource>();
            float vol = mapValue(rb.linearVelocity.magnitude, 0.1f, 10f, 0f, 1f);
            sfx[2].volume = vol;

        }
        else{
            AudioSource[] sfx = gameObject.GetComponents<AudioSource>();
            sfx[2].volume = Mathf.Lerp(sfx[2].volume, 0f, 0.1f);
        }

            
    }

    void OnTriggerEnter(Collider trigger){
        //Debug.Log(trigger.gameObject.layer);
        if (trigger.gameObject.layer == LayerMask.NameToLayer("Collectibles")){

            Animation anim  = trigger.gameObject.GetComponent<Animation>();


            if(trigger.gameObject.GetComponent<KeyScript>().collected != true){ //trigger anim (and prevent double trigger)

            anim.CrossFade("KeyFade", 0.1f);
            //trigger.gameObject.animator.Play("KeyFade");

            trigger.gameObject.GetComponent<KeyScript>().collected = true;
            

            items += 1;
            GlobalStats.Instance.collected_items = items;
            UpdateItemText();


            }


        }

        if (trigger.gameObject.layer == LayerMask.NameToLayer("LevelEnd")){

            endZone = trigger.gameObject;
            inLevelEnd = true;
            AudioSource[] sfx = trigger.gameObject.GetComponents<AudioSource>();
            sfx[0].Play();

        }

        if (trigger.gameObject.layer == LayerMask.NameToLayer("Limits")){ //invisible limit down the map

            RestartScene();
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

        if (GlobalStats.Instance.max_items == 1){
            itemText.text = "Key : " + items.ToString() + " / " + GlobalStats.Instance.max_items.ToString();

        }
        else {

            itemText.text = "Keys : " + items.ToString() + " / " + GlobalStats.Instance.max_items.ToString();
        }

    }

    void RestartScene()
    {
    // Get the currently active scene
    Scene currentScene = SceneManager.GetActiveScene();

    // Reload it
    SceneManager.LoadScene(currentScene.name);
    }

    float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }

}


