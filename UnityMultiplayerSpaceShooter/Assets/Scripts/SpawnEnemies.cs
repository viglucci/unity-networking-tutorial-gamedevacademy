using Mirror;
using UnityEngine;

public class SpawnEnemies : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnInterval = 1.0f;

    [SerializeField] private float enemySpeed = 1.0f;

    public override void OnStartServer()
    {
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-4.0f, 4.0f), transform.position.y);
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity) as GameObject;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -enemySpeed);
        NetworkServer.Spawn(enemy);
        Destroy(enemy, 10);
    }
}