using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GroundSpawner : MonoBehaviour
{
    [SerializeField] 
    private Ground groundRef;

    private Ground prevGround;

    private void SpawnGround()
    {
        if (prevGround != null)
        {
            Ground newGround = Instantiate(groundRef);

            newGround.gameObject.SetActive(true);

            prevGround.SetNextGround(newGround.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Ground ground = other.GetComponent<Ground>();

        if (ground)
        {
            prevGround = ground;

            SpawnGround();
        }
    }
}
