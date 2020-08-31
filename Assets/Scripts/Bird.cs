using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private int score;

    [Space][Space]
    
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private GameObject pfProjectile;

    [Space][Space]

    // Rotation / facing to the ground variables
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform surfaceTargetRef;
 
    [Space][Space][Space]
    
    [SerializeField] private UnityEvent OnJump, OnDead, OnAddPoint;

    private Rigidbody2D rigidBody2D;
    private Animator animator;
    [HideInInspector]
    public SaveSystem saveSystem;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        saveSystem = GetComponent<SaveSystem>();

        if (scoreText != null)
            scoreText.text = "0";
        highscoreText.text = saveSystem.LoadScore().ToString();
    }

    private void Update()
    {
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            Jump();

            // Shoot spit (projectile)
            if (pfProjectile != null)
                Instantiate(pfProjectile, transform.position, Quaternion.identity);
        }

        if (!IsDead() && surfaceTargetRef != null)
            RotateToGround();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Dead()
    {
        if (!isDead && OnDead != null)
        {
            OnDead.Invoke();

            saveSystem.SaveScore(score);
            highscoreText.text = saveSystem.LoadScore().ToString();
        }

        isDead = true;
    }

    private void Jump()
    {
        if (rigidBody2D)
        {
            rigidBody2D.velocity = Vector2.zero;

            rigidBody2D.AddForce(new Vector2(0, upForce));

            RotateTo(20f);
        }

        if (OnJump != null)
        {
            OnJump.Invoke();
        }
    }

    public void AddScore(int value)
    {
        score += value;

        if (OnAddPoint != null)
        {
            OnAddPoint.Invoke();
        }

        scoreText.text = score.ToString();
        
        //Debug.Log("Score : " + score);
    }

    private void RotateToGround()
    {
        // Rotate towards the ground/surface overtime

        Vector3 distance = surfaceTargetRef.position - transform.position;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotateSpeed);
    }

    public void RotateTo(float rotationValue)
    {
        // Facing to the sky a bit on each jump

        transform.rotation = Quaternion.Euler(0, 0, rotationValue);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        animator.enabled = false;
    }
}
