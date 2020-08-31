using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipeUp, pipeDown;
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] private float holeSizeMin = 0.85f;
    [SerializeField] private float holeSizeMax = 3f;
    [SerializeField] private float maxMinOffset = 1;

    [Space]

    [SerializeField] private Point point;

    private Coroutine CR_Spawn;

    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        if (CR_Spawn == null)
        {
            CR_Spawn = StartCoroutine(IeSpawn());
        }
    }

    private void StopSpawn()
    {
        if (CR_Spawn != null)
        {
            StopCoroutine(CR_Spawn);
        }
    }

    private void SpawnPipe()
    {
        float y = maxMinOffset * Mathf.Sin(Time.time);

        float holeSize; 
        
        // Random size for the hole
        float randSize = Random.Range(0, 10);   // Smaller sizes are very common

        if (randSize < 7)
            holeSize = Random.Range(holeSizeMin, holeSizeMax / holeSizeMax);
        else
            holeSize = Random.Range(holeSizeMin, holeSizeMax);


        Pipe newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(0,0,180));

        newPipeUp.gameObject.SetActive(true);
        newPipeUp.transform.position += Vector3.up * (holeSize / 2) + Vector3.up * y;

        Pipe newPipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);

        newPipeDown.gameObject.SetActive(true);
        newPipeDown.transform.position += Vector3.down * (holeSize / 2)  + Vector3.up * y;

        Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
        newPoint.gameObject.SetActive(true);
        newPoint.SetSize(holeSize);
        newPoint.transform.position += Vector3.up * y;
    }

    private IEnumerator IeSpawn()
    {
        while (true)
        {
            if (bird.IsDead())
            {
                StopSpawn();
            }

            SpawnPipe();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    
}
