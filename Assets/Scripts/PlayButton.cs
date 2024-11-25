using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite pressedSprite; 

    public UnityEvent buttonTouch;

    //Timer vars
    private bool timerActive = false;
    private float timerDuration = 5f;

    //Prefab spawning variables
    public List<GameObject> prefabs;
    public Camera mainCamera;

    //Audio variables
    public AudioClip soundClip;
    private AudioSource audioSource;
    public AudioClip timerEndSoundClip;

    //Other buttons
    public GameObject restartButton;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        buttonTouch.AddListener(GameObject.FindFirstObjectByType<GameController>().IncrementScore);
        buttonTouch.AddListener(GameObject.FindFirstObjectByType<UIController>().UpdateScore);

        buttonTouch.AddListener(SpawnRandomPrefab);

        buttonTouch.AddListener(delegate { PlaySound(soundClip); });
    }

    void Update()
    {
        if (Input.touchCount > 0 && !timerActive)
        {
            Touch[] touches = Input.touches;

            // Process only the first touch in the array
            for (int i = 0; i < touches.Length; i++)
            {
                Touch touch = touches[i];
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (GetComponent<Collider2D>().OverlapPoint(touchPosition))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        spriteRenderer.sprite = pressedSprite;
                        buttonTouch.Invoke();
                        StartCoroutine(StartTimer());
                        break; // Only process the first valid touch in this frame
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        spriteRenderer.sprite = defaultSprite;
                    }
                }
            }
        }
    }

    IEnumerator StartTimer()
    {
        
        yield return new WaitForSeconds(timerDuration); 
        DisableButton(); 
    }

    void DisableButton()
    {
        timerActive = true;
        PlaySound(timerEndSoundClip);
        restartButton.SetActive(true);
    }

    void SpawnRandomPrefab()
    {
        if (prefabs.Count == 0 || mainCamera == null)
        {
            Debug.LogWarning("Prefab list empty");
            return;
        }

        GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];

        Vector2 randomPosition = GetRandomScreenPosition();

        Instantiate(randomPrefab, randomPosition, Quaternion.identity);
    }

    Vector2 GetRandomScreenPosition()
    {
        // Get screen bounds in world space
        float screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize * 2f;

        float x = Random.Range(-screenWidth / 2f, screenWidth / 2f);
        float y = Random.Range(-screenHeight / 2f, screenHeight / 2f);

        return new Vector2(x, y);
    }

    void PlaySound(AudioClip sound)
    {
        if (soundClip == null || audioSource == null)
        {
            Debug.LogWarning("No sound added");
            return;
        }

        audioSource.PlayOneShot(sound);
    }
}
