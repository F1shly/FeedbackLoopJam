using UnityEngine;
using System.Collections;

public class SpawnBox : MonoBehaviour
{
    public GameObject box;
    public bool OneTimeSpawner;
    public GameObject spawnedBox;
    private void Awake()
    {
        if(!OneTimeSpawner)
        {
            StartCoroutine(SpawnDelay());
        }
        else
        {
            OneSpawn();
        }
    }
    public void OneSpawn()
    {
        if(spawnedBox == null)
        {
            spawnedBox = Instantiate(box, transform.position, transform.rotation);
        }
    }
    IEnumerator SpawnDelay()
    {
        Instantiate(box, transform.position,transform.rotation);
        yield return new WaitForSeconds(4);
        StartCoroutine(SpawnDelay());
    }
}
