using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float travelSpeed = 4.0f;
    [SerializeField] private GameObject fxDestroy;
    [SerializeField] private GameObject audioSource;

    [Space][Space][Space]

    [SerializeField] private UnityEvent OnDestroyed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("DestroyProjectile", 2.0f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.right * travelSpeed * 10.0f * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Instantiate(fxDestroy, transform.position, Quaternion.identity);

        audioSource.transform.parent = null;
        OnDestroyed.Invoke();

        Destroy(audioSource, 1f);
        Destroy(gameObject);
    }
}
