using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;

    [Header("Sounds")]
    public AudioClip popSound;
    public AudioClip buttonClickSound;
    public AudioClip startSound;

    [Header("Level Up Sounds (Index = Level - 1)")]
    public AudioClip[] levelUpSounds;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayPop()
    {
        if (popSound != null)
            audioSource.PlayOneShot(popSound);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayButtonClick();
        }
    }


    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
    }


    public void PlayStart()
    {
        if (startSound != null)
            audioSource.PlayOneShot(startSound);

    }

    public void PlayLevelUp(int level)
    {
        int index = level - 1;

        if (levelUpSounds != null &&
            index >= 0 &&
            index < levelUpSounds.Length &&
            levelUpSounds[index] != null)
        {
            audioSource.PlayOneShot(levelUpSounds[index]);
        }
    }
}