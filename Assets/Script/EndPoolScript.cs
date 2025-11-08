using UnityEngine;


// DEPRECATED : USING PAUSE MENU NOW

public class EndPoolScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape)){

            QuitGame();
            //Debug.Log(4);
        }

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop Play Mode
        #else
        Application.Quit();
        #endif
    }

}
