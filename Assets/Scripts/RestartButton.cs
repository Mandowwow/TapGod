using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite pressedSprite;

    public UnityEvent restartTouch;

    public AudioClip soundClip;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        restartTouch.AddListener(ReloadCurrentScene);
    }

    private void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (GetComponent<Collider2D>().OverlapPoint(touchPosition))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    spriteRenderer.sprite = pressedSprite;
                }

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    PlaySound(soundClip);
                    spriteRenderer.sprite = defaultSprite;
                    restartTouch.Invoke();

                }
            }

            
        }
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

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
