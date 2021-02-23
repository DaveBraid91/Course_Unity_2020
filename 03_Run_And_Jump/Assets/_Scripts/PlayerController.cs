using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private const string SPEED_F = "Speed_f";
    private const string SPEED_F_MULTIPLIER = "Speed_f_Multiplier";
    private const string JUMP_TRIG = "Jump_trig";
    private const string DEATH_B = "Death_b";
    private const string DEATH_TYPE_INT = "DeathType_int";
    private Rigidbody _rb;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField, Range(10, 50)]
    private float jumpForce;
    private float jumpMovement;
    private float speeedMultiplier = 1;
    private bool isGrounded;
    private bool _gameOver;
    public bool GameOver{ get => _gameOver;}

    public float gravityMultiplier;
    [Range(0, 1)]
    public float audioVolume;
    public ParticleSystem explosion;
    public ParticleSystem dirtTrail;
    public AudioClip jumpClip, crashClip;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _animator.SetFloat(SPEED_F, 1);
        Physics.gravity *= gravityMultiplier;
    }

    private void Update()
    {
        speeedMultiplier += Time.deltaTime/10;
        _animator.SetFloat(SPEED_F_MULTIPLIER, 1 + speeedMultiplier/10); 
        _animator.SetFloat(SPEED_F, speeedMultiplier);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        jumpMovement = Input.GetAxis("Jump");
        if(isGrounded && jumpMovement > 0.5f && !_gameOver)
        {
            dirtTrail.Stop();
            _animator.SetTrigger(JUMP_TRIG);
            _rb.AddForce(Vector3.up * jumpForce * jumpMovement, ForceMode.Impulse);
            _audioSource.PlayOneShot(jumpClip, audioVolume);
            isGrounded = false;
        }            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtTrail.Play();
        }
            
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            _gameOver = true;
            dirtTrail.Stop();
            explosion.Play();
            _audioSource.PlayOneShot(crashClip, audioVolume);
            _animator.SetInteger(DEATH_TYPE_INT, Random.Range(1, 3));
            _animator.SetBool(DEATH_B, true);
            Debug.Log("Game Over");
            Invoke("RestartGame", 3);
        }
    }

    void RestartGame()
    {
        speeedMultiplier = 1;
        Physics.gravity /= gravityMultiplier;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
