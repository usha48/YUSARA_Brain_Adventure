using UnityEngine;
using UnityEngine.UI;

public class ShapeButton : MonoBehaviour
{
    public string shapeName;

    private ShapeGameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<ShapeGameManager>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        gameManager.OnShapeClicked(this);
    }
}