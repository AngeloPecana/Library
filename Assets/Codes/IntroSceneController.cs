using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour
{
    private bool canProceed = false;

    void Start()
    {
        Invoke("EnableInput", 1f); 
    }

    void EnableInput()
    {
        canProceed = true;
    }

    void Update()
    {
        if (canProceed && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
        {
            LoadMainMenu();
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
