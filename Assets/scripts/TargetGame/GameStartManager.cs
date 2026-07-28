using UnityEngine;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    [Header("References")]
    public SoundManager soundManager;
    public GameObject target;
    public Button startButton;

    private void Start()
    {
        // Hide target at start
        if (target != null)
            target.SetActive(false);
    }

    // Called by Start Button
    public void OnStartGame()
    {
        // Play "Let's Go" sound
        if (soundManager != null)
            soundManager.PlayStart();

        // Show target
        if (target != null)
            target.SetActive(true);

        // Hide start button
        if (startButton != null)
            startButton.gameObject.SetActive(false);
    }
}