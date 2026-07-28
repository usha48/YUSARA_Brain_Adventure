using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        Debug.Log("Play button clicked");

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
