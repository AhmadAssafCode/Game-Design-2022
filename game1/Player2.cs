using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip fallSound;

    public int score = 0;
    public TextMeshProUGUI ScoreText;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public Animator animator;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private bool grounded = true;
    public float jumpPower = 5000f;
    private Vector3 lastFlag;
    public UImanager ui;
    //public Transform uim;
    // Start is called before the first frame update
    void Start()
    {
        // ui = uim.GetComponent<UImanager>();
        //  ui =GameObject.Find("UI").GetComponent<UImanager>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        grounded = true;
        jumpPower = 5000f;

        audioSource = GetComponent<AudioSource>();
        ScoreText.text = PlayerPrefs.GetInt("coins").ToString();
        score = PlayerPrefs.GetInt("coins");

        // ui=GetComponent<scrip>
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        animator.SetFloat("speed", Mathf.Abs(horizontal));
        if (ui.moveX != 0)
            animator.SetFloat("speed", Mathf.Abs(ui.moveX));
        if ((horizontal > 0 || ui.moveX > 0) && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if ((horizontal < 0 || ui.moveX < 0) && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        //if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) && grounded == true)

        if ((Input.GetKeyDown("space") || ui.moveY > 0) && grounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpPower);
            grounded = false;
            animator.SetBool("isJumping", true);
            audioSource.clip = jumpSound;
            audioSource.Play();

        }
    }




    void FixedUpdate()
    {

        if (ui.moveX != 0)
            horizontal = ui.moveX;

        Vector2 position = rigidbody2d.position;
        position.x = position.x + 5.0f * horizontal * Time.deltaTime;
        //position.y = position.y + 10.0f * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(grounded);
        if (other.gameObject.CompareTag("coin"))
        {
            score++;
            // ScoreText.text = score.ToString();
            Destroy(other.gameObject);
            audioSource.clip = coinSound;
            audioSource.Play();
            PlayerPrefs.SetInt("coins", score);
            PlayerPrefs.Save();

        }

        if (other.gameObject.CompareTag("ground"))
        {
            grounded = true;
            animator.SetBool("isJumping", false);

            Debug.Log("grounded" + grounded);

        }


        if (other.gameObject.CompareTag("fall"))
        {
            // score++;
            // ScoreText.text = "Score: " + score;
            transform.position = lastFlag;
            audioSource.clip = fallSound;
            audioSource.Play();
            //muteAllSounds();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //Application.LoadLevel
        }


    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("hi" + lastFlag.x);

        if (col.transform.tag == "flag")
        {

            lastFlag = col.transform.position;

            Debug.Log("flag x" + lastFlag.x);

        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void muteAllSounds()

    {

        AudioListener.volume = 1 - AudioListener.volume;

    }

}