using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;

    [Header("Size Settings")]
    public float startSize = 180f;
    public float shrinkAmount = 25f;
    public float minSize = 70f;

    [Header("Colors")]
    public Color[] colors;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Called from ScoreManager when level changes
    public void UpdateSizeForLevel(int level)
    {
        float size = startSize - (level - 1) * shrinkAmount;
        size = Mathf.Max(size, minSize);

        rectTransform.sizeDelta = new Vector2(size, size);

        SetRandomColor();
    }

    // Called when target is clicked
    public void OnTargetClicked()
    {
        if (ScoreManager.Instance == null) return;
        if (ScoreManager.Instance.gameOver) return; // ⛔ BLOCK clicks

        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayPop();

        ScoreManager.Instance.AddScore(1);

        MoveTarget();
    }


    public void MoveTarget()
    {
        RectTransform canvasRect =
            rectTransform.parent.GetComponent<RectTransform>();

        float padding = rectTransform.sizeDelta.x / 2f;

        float x = Random.Range(
            -canvasRect.rect.width / 2 + padding,
             canvasRect.rect.width / 2 - padding
        );

        float y = Random.Range(
            -canvasRect.rect.height / 2 + padding,
             canvasRect.rect.height / 2 - padding
        );

        rectTransform.anchoredPosition = new Vector2(x, y);

        if (colors != null && colors.Length > 0)
            image.color = colors[Random.Range(0, colors.Length)];
    }

    private void SetRandomColor()
    {
        if (colors != null && colors.Length > 0)
            image.color = colors[Random.Range(0, colors.Length)];
    }
}