using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyContainer;
    [SerializeField] GameObject[] powerUps;
    bool stopSpawning = false;
    public void StartSpawning()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnBoosterRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator spawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            yield return new WaitForSeconds(2f);
           Vector3 position = new Vector3(Random.Range(-8.5f, 8.5f), 8f, 0f);
           GameObject newEnemy= Instantiate(enemy, position, Quaternion.identity);
           newEnemy.transform.parent = enemyContainer.transform;
           yield return new WaitForSeconds(Random.Range(3f,6f));
        }
    }
    IEnumerator spawnBoosterRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (stopSpawning == false) { 
        Vector3 position = new Vector3(Random.Range(-8.25f, 8.25f), 8f, 0f);  
       Instantiate(powerUps[Random.Range(0,3)],position, Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(3,8));
        }

    }
    public void onPlayDeath()
    {
        stopSpawning = true;
        Destroy(enemyContainer);
    }

}
