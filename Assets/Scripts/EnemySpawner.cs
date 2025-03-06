using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]private GameObject[] enemyPrefabs;


    [Header("Attributes")]
    [SerializeField ] private int enemiesCount = 10;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 10f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();


    private int currentWave  = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private float eps; // Enemies per second

    void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    void Start()
    { 
        StartCoroutine(StartWave());
    }
    
    void Update()
    {
        if(!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1 / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        eps = EnemiesPerSecond();
        enemiesLeftToSpawn = EnemiesPerWave();
        isSpawning = true;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[enemyIndex];
        Instantiate(prefabToSpawn, GameManager.main.startPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(enemiesCount * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(Mathf.RoundToInt(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor)), 0 , enemiesPerSecondCap);
    }
}
