using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D playerRigidBody;
    public Animator playerAnimator;
    public BoxCollider2D playerCollider;

    private bool isGrounded = true;

    public int lives = 3;
    public bool isInvincible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            playerAnimator.SetInteger("state", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            if (!isGrounded)
            {
                playerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                Destroy(collision.gameObject);
                Hit();
            }
        }
        else if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.CompareTag("Golden"))
        {
            Destroy(collision.gameObject);
            StartInvincible();
        }
    }

    private void Hit()
    {
        lives -= 1;
        if (lives == 0)
        {
            KillPlayer();
        }
    }

    private void Heal()
    {
        lives = Mathf.Min(3, lives + 1);
    }

    private void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvinvible", 5f);
    }

    private void StopInvinvible()
    {
        isInvincible = false;
    }

    private void KillPlayer()
    {
        playerCollider.enabled = false;
        playerAnimator.enabled = false;
        playerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }
}
