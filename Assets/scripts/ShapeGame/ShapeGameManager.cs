using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShapeGameManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip gameOverSound;
    public AudioClip clickSound;

    [Header("References")]
    public List<ShapeButton> allShapes;
    public Transform shapeCluster;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    public GameObject gameOverPanel;

    [Header("Game Settings")]
    public int level = 1;
    public int maxLevel = 4;

    int score = 0;
    int roundsCompleted = 0;
    bool gameEnded = false;

    ShapeButton correctShape;
    List<ShapeButton> activeShapes = new List<ShapeButton>();

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        gameOverPanel.SetActive(false);
        StartLevel();
    }

    void StartLevel()
    {
        roundsCompleted = 0;
        levelText.text = "Level : " + level;
        StartRound();
    }

    void StartRound()
    {
        // Hide all shapes
        foreach (var shape in allShapes)
        {
            shape.gameObject.SetActive(false);
        }

        activeShapes.Clear();

        int shapesToShow = Mathf.Min(level, allShapes.Count);

        // Pick random shapes
        List<ShapeButton> tempList = new List<ShapeButton>(allShapes);
        Shuffle(tempList);

        for (int i = 0; i < shapesToShow; i++)
        {
            activeShapes.Add(tempList[i]);
        }

        // Shuffle visual order
        Shuffle(activeShapes);

        foreach (var shape in activeShapes)
        {
            shape.transform.SetParent(shapeCluster);
            shape.gameObject.SetActive(true);
        }

        ApplyVisualOrder(activeShapes);

        correctShape = activeShapes[Random.Range(0, activeShapes.Count)];
        instructionText.text = "Tap " + correctShape.shapeName;
    }

    public void OnShapeClicked(ShapeButton clickedShape)
    {
        if (gameEnded) return;

        if (clickedShape == correctShape)
        {
            audioSource.PlayOneShot(correctSound);

            score++;
            roundsCompleted++;
            scoreText.text = "Score : " + score;

            if (roundsCompleted >= level)
            {
                if (level >= maxLevel)
                {
                    EndGame();
                    return;
                }

                level++;
                StartLevel();
            }
            else
            {
                StartRound();
            }
        }
        else
        {
            audioSource.PlayOneShot(wrongSound);
            instructionText.text = "Try again!";
        }
    }

    void EndGame()
    {
        gameEnded = true;
        instructionText.text = "GAME OVER";

        audioSource.PlayOneShot(gameOverSound);
        gameOverPanel.SetActive(true);
    }

    void RestartGame()
    {
        audioSource.PlayOneShot(clickSound);

        gameEnded = false;
        level = 1;
        score = 0;

        scoreText.text = "Score : 0";
        gameOverPanel.SetActive(false);

        StartLevel();
    }
    void Shuffle(List<ShapeButton> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            ShapeButton temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void ApplyVisualOrder(List<ShapeButton> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.SetSiblingIndex(i);
        }
    }
}