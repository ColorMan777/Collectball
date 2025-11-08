using UnityEngine;

public class CamControl : MonoBehaviour
{

    public GameObject player;
    public GameObject playerCamera;

    public float mouseSensitivity = 150f;
    public float max_down = -50f;
    public float max_up = 20f;

    private float xRotation = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lock the cursor to the center of the Game window
        Cursor.lockState = CursorLockMode.Locked;
        // Hide the cursor
        Cursor.visible = false;

        CopyPos();
    }

    // Update is called once per frame
    void Update()
    {
        //CopyPos(); // some staggering when using lerp to copy position -> moved in fixed update

        if (Input.GetKey(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void FixedUpdate(){

        CopyPos();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, max_down, max_up); // Limite du regard vertical

        //playerCamera.localRotation = Quaternion.Euler(0f, 0f , xRotation);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        transform.Rotate(Vector3.up * mouseX * 4f);
    }

    void CopyPos(){

        //transform.position = player.transform.position;
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.3f);

    }

}

