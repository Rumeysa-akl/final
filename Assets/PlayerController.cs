using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Action OnPlayerUpdate;

    [SerializeField] private float Speed = 0.5f;
    [SerializeField] private int Score = 0;
    [SerializeField] private int WinScore = 50;
    [SerializeField] private Text winText;
    [SerializeField] private Text timerText;
    [SerializeField] private float gameTime = 60f;
    [SerializeField] private float bonusTime = 15f; // Bonus zaman miktarı

    private SpriteRenderer SpriteRenderer;
    private bool Grounded;
    private bool Jumping;
    private float timer;

    private float HorizontalAxis;
    private float VerticalAxis;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        winText.gameObject.SetActive(false);
        timer = gameTime;
    }

    private void Update()
    {
        var NewValue = Input.GetAxis("Horizontal");

        if (NewValue != 0f)
        {
            HorizontalAxis = NewValue;
            SpriteRenderer.flipX = HorizontalAxis < 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Grounded)
            {
                VerticalAxis = 1f;
                Jumping = true;
                Grounded = false;
            }
        }

        if (Jumping)
        {
            if (Grounded)
            {
                VerticalAxis = 0f;
                Jumping = false;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position =
            new Vector3(
                transform.position.x + HorizontalAxis * Speed,
                transform.position.y + VerticalAxis * Speed
            );

        OnPlayerUpdate?.Invoke();

        if (Score >= WinScore)
        {
            winText.gameObject.SetActive(true);
        }

        // Coin alındığında süreyi arttır
        if (timer > 0f)
        {
            timer -= Time.fixedDeltaTime;
        }

        timerText.text = "Time: " + Mathf.Round(timer);

        // Zaman kontrolü ve bonus zaman eklenmesi
        if (timer <= 0f)
        {
            Debug.Log("Game Over - Time's up!");
            // İsterseniz burada başka bir işlem yapabilirsiniz, örneğin oyunu yeniden başlatma
        }
    }

    private void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
        else if (Collision.gameObject.CompareTag("Circle"))
        {
            Score += 10;
            Debug.Log("Score: " + Score);
            Destroy(Collision.gameObject);

            // Coin alındığında süreyi arttır
            timer += bonusTime;
        }
    }

    private void OnCollisionExit2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Ground"))
        {
            Grounded = false;
        }
    }
}
