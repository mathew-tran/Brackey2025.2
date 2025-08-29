using System.Collections;
using UnityEngine;

public class PianoSpawner : MonoBehaviour
{
    public GameObject mPianoToSpawn;

    public int mAmount = 2;

    public void StartSpawningPianos()
    {
        StartCoroutine(SpawnPianos());
    }
    public IEnumerator SpawnPianos()
    {
        for (int i = 0; i < mAmount; ++i)
        {
            yield return new WaitForSeconds(2.5f);
            CreatePiano();
        }
       
        
    }

    private void CreatePiano()
    {
        Vector3 spawnPosition = GameObject.Find("Player").transform.position;
        spawnPosition.y = transform.position.y;
        Instantiate(mPianoToSpawn, spawnPosition, Quaternion.identity);
    }
}
