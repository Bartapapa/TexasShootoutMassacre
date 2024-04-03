using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_RestartOnDeath : MonoBehaviour
{
    public void RestartScene()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}