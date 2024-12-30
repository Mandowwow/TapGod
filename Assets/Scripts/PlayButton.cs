using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite pressedSprite; 

    //Timer vars
    private bool timerActive = false;
    private float timerDuration = 5f;

    //Other buttons
    public GameObject restartButton;

    //Event for button press to notify listeners
    public static event System.Action OnButtonPressed;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

                        // notify global event
                        OnButtonPressed?.Invoke();

                        StartCoroutine(StartTimer());

                        // Only process the first valid touch in this frame
                        break; 
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
        FindObjectOfType<AudioController>()?.PlayTimerEndSound();
        restartButton.SetActive(true);
    }

}
