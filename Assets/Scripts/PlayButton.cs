using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayButton : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite pressedSprite; 

    private bool isTouchingButton = false;

    private int currentTouchId = -1;

    public UnityEvent buttonTouch;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        buttonTouch.AddListener(GameObject.FindFirstObjectByType<GameController>().IncrementScore);
        buttonTouch.AddListener(GameObject.FindFirstObjectByType<UIController>().UpdateScore);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                if (GetComponent<Collider2D>().OverlapPoint(touchPosition))
                {
                    if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary) && !isTouchingButton )
                    {

                        spriteRenderer.sprite = pressedSprite;
                        isTouchingButton = true;
                        currentTouchId = touch.fingerId;
                        buttonTouch.Invoke();
                    }

                    if (touch.fingerId == currentTouchId && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
                    {
                        spriteRenderer.sprite = defaultSprite;
                        isTouchingButton = false;
                        currentTouchId = -1;
                    }
                }
            }
        }
    }
}
