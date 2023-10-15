using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float jumpForce = 1000f;
    private float gravityModifier = 5f;
    private float movementSpeed = 10f;
    private float turnSpeed = 200f;
    private float zRange = 23f;
    private float xRange = 20f;
    public bool gameOver;
    private bool isOnGround = true;
    public GameObject powerupPrefab;
    public GameObject enemyPrefab;
    private GameManager gameManager;
    public SpawnManager spawnManager;
    public bool powerupIndicator;
    public bool bulletIndicator = true;
    public bool polliceIndicator = true;
    private float interval = 4;
    //private Animator playerAnim;
    //public ParticleSystem explosionParticle;
    //public ParticleSystem dirtParticle;
    //public AudioClip jumpSound;
    // public AudioClip crashSound;
    //private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //playerAnim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Allow the player to jump
        // if (!gameOver)
        // {
        inbounds();
        if (!gameOver)
        {
            move();
            jump();
        }



    }

    void inbounds()
    {
        //Keep the player inbounds
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }
    void move()
    {
        //Allow the player to move in all directions
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.back * horizontalInput * movementSpeed * Time.deltaTime);
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.right * verticalInput * movementSpeed * Time.deltaTime);
        //Allow the player to rotate
        transform.Rotate(Vector3.up, verticalInput * turnSpeed * Time.deltaTime);
    }
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            //playerAnim.SetTrigger("Jump_trig");
            //dirtParticle.Stop();
            //playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            bulletIndicator = false;
            Destroy(other.gameObject);
            StartCoroutine(NextLevelCoroutine());
            polliceIndicator = false;

        }
        else if (other.CompareTag("Enemy"))
        {
            gameOver = true;
            gameManager.GameOver();
            //playerAnim.SetBool("Death_b", true);
            //  playerAnim.SetInteger("DeathType_int", 1);
            //explosionParticle.Play();
            //   dirtParticle.Stop();
            //  playerAudio.PlayOneShot(crashSound, 1.0f);
            Destroy(other.gameObject);


        }
        else if (other.CompareTag("Ground"))
        {
            isOnGround = true;
            // dirtParticle.Play();
        }
    }

    IEnumerator NextLevelCoroutine()
    {
        yield return new WaitForSeconds(interval);
        spawnManager.powerupIndicator = true;
        polliceIndicator = true;
    }
}


