using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSceneManager : MonoBehaviour
{
    public float delay = 3f;

    void Start()
    {
        Invoke("LoadMainMenu", delay);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu"); 
    }
}
