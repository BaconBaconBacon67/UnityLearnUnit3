using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 5;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    public ParticleSystem explosionPartical;
    public ParticleSystem dirtParticle;
    private Animator playerAnim;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    { if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
       
        }
}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticle.Stop();
            explosionPartical.Play();
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
             playerAnim.SetInteger("DeathType_int", 1);
        }
    }
}
