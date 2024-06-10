using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    //rigid body er maddhobe player control korbo
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private BoxCollider2D playerCollider;
    private AudioSource playerAudioSource;
    public TMP_Text pointText;
    public GameObject gameOverPanel;

    public float leftRightSpeed;
    public float jumpSpeed;
    public float bulletSpeed;
    

    float playerOriginalScaleX;
    int playerFacingDirection = 1; // player right side e takay ase
    int point = 0;

    public LayerMask groundLayer;

    public AudioClip gameBackgroundMusic;
    public AudioClip jumpSFX;
    public AudioClip bulletSFX;
    public AudioClip gameOverSFX;
    public AudioClip collectableSFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAudioSource = GetComponent<AudioSource>();
        playerOriginalScaleX = gameObject.transform.localScale.x;
        playerAudioSource.clip = gameBackgroundMusic;
        playerAudioSource.Play();
        
    }
    private void Update()
    {   //right e move
        if (Input.GetKey(KeyCode.D))
        {
            playerFacingDirection = 1; ;
            rb.velocity = new Vector2(leftRightSpeed, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(playerFacingDirection * playerOriginalScaleX, 
                gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            PlayAnimation("PlayerWalking");

        }//left e 
        else if (Input.GetKey(KeyCode.A))
        {
            playerFacingDirection = -1;
            rb.velocity = new Vector2 (-leftRightSpeed, rb.velocity.y);
            gameObject.transform.localScale = new Vector3(playerFacingDirection * playerOriginalScaleX,
                gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            PlayAnimation("PlayerWalking");
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            PlayAnimation("PlayerIdle");
        }
        if(Input.GetKeyDown(KeyCode.Space))// palyer will jump if he is in ground
        {   if (playerCollider.IsTouchingLayers(groundLayer))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                //play sfx
                playerAudioSource.PlayOneShot(jumpSFX, 1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //genrate bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // add velocity to player facing direction
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerFacingDirection * bulletSpeed, 0f);
            //play sfx
            playerAudioSource.PlayOneShot(bulletSFX, 1f);
        }
    }

    void PlayAnimation(string animationName)
    {
        if (playerCollider.IsTouchingLayers(groundLayer))
        {
            playerAnimator.Play(animationName);
        }
        else
        {
            playerAnimator.Play("NoAnimation");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Collectable")
        {
            //incrase point +1
            point = point + 1;

            pointText.text = "Point : " + point;
            Destroy(collision.gameObject);
            //play sfx
            playerAudioSource.PlayOneShot(collectableSFX, 1f);
        }
        if(collision.tag == "Enemy")
        {
            gameOverPanel.SetActive(true);
            //play sfx
            playerAudioSource.PlayOneShot(gameOverSFX, 1f);
        }

        if(collision.tag == "Level1 complete")
        {
            //load level2
            SceneManager.LoadScene(2);
        }
        if (collision.tag == "Level2 complete")
        {
            //load level2
            SceneManager.LoadScene(0);
        }
    }
    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
