using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    AudioClip UIButton;
    private void Start()
    {
        UIButton = Resources.Load<AudioClip>("Sounds/UI-Click");
    }
    public void RestartScene()
    {
        AudioSource.PlayClipAtPoint(UIButton, transform.position);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSceneToMenu()
    {

        AudioSource.PlayClipAtPoint(UIButton, transform.position);
        SceneManager.LoadScene(0);
    }
}
