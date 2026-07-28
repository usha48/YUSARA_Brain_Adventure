using UnityEngine;

public class LevelMessagePop : MonoBehaviour
{
    private Animator animator;
    private bool hasPlayed = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayPop()
    {
        Debug.Log("PlayPop CALLED");

        if (hasPlayed)
        {
            Debug.Log("PlayPop BLOCKED");
            return;
        }

        hasPlayed = true;
        animator.Play("PopAnim", 0, 0f);
    }

    public void ResetPop()
    {
        hasPlayed = false;
    }
}