using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public bool isPaused; // Flag to track pause state
    public string main_menu_name;
    public GameObject pauseMenu;

    void Start(){

        pauseMenu.SetActive(false);
    }

    void Update()
    {

        if (isPaused){

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            // Toggle pause state on Escape key press
            isPaused = !isPaused;
            if (isPaused)
            {

                PauseGame();

            }
            else
            {
                ResumeGame();

            }
        }


    }

    void PauseGame()
    {
        // Set Time.timeScale to 0 to pause gameplay
        Time.timeScale = 0;
        // Make PauseMenu panel visible (activate its gameObject)
        pauseMenu.SetActive(true);

    }

    public void ResumeGame()
    {
        // Set Time.timeScale back to 1 to resume gameplay
        Time.timeScale = 1;
        isPaused = false;
        // Hide PauseMenu panel (deactivate its gameObject)
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void GoToMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(main_menu_name);
        Cursor.lockState = CursorLockMode.None;

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