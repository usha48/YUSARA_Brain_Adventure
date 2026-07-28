using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    [SerializeField] private TextMeshProUGUI timerText;

    private float timeRemaining = 60f;
    private bool isRunning = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
        }

        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        isRunning = true;
    }
}