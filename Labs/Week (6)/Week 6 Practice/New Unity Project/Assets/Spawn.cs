using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{


    // Update is called once per frame
    public GameObject[] enemies;

    public int amount;

    private Vector3 SpawnPoint;

    void Update ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        amount = enemies.Length;
        if(amount!=20)
        {
            InvokeRepeating("spawnEnemy", 1f, 1f);

        }
	}

    void spawnEnemy()
    {
        SpawnPoint.x = Random.Range(-20, 20);
        SpawnPoint.y = 0.5f;
        SpawnPoint.z = Random.Range(-20, 20);
        Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length - 1)], SpawnPoint, Quaternion.identity);
        CancelInvoke();
    }
}
