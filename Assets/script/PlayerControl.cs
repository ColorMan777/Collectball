using UnityEngine;

public class PlayerControl : MonoBehaviour

{
    public float thrust = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward * thrust);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-transform.forward * thrust);
    }
}
