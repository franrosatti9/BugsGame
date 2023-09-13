using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float spawnRate;
    [SerializeField] private float minSpawnRate;
    [SerializeField] private int spawnsUntilSpecialEnemy;
    [SerializeField] private float spawnRateIncrease;
    
    [FormerlySerializedAs("enemies")]
    [Header("References")]
    [SerializeField] private SpecialEnemy[] specialEnemies;
    [SerializeField] private Enemy normalEnemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    
    private List<GameObject> _availableEnemies = new List<GameObject>();
    private int _spawnedEnemiesCounter;
    private float _cooldown;

    // Eventos para manejar notificaciones de efectos activos
    public event Action<Bug> OnEnemyDied;
    public event Action<Bug> OnEnemySpawned;

    public static EnemySpawner instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    void Start()
    {
        _cooldown = 0;
        
        for (int i = 0; i < specialEnemies.Length; i++)
        {
            SpecialEnemy specialEnemy = Instantiate(specialEnemies[i]);
            _availableEnemies.Add(specialEnemy.gameObject);
        }
    }

    void Update()
    {
        if(_cooldown < spawnRate)
        {
            _cooldown += Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            _cooldown = 0;
        }
    }

    public void SpawnEnemy()
    {
        int rdm = UnityEngine.Random.Range(0, spawnPoints.Length);

        if(spawnRate > minSpawnRate) spawnRate -= spawnRateIncrease;
        
        // Buscamos un spawn point al azar y vemos si toca spawnear un enemigo especial

        if (_spawnedEnemiesCounter == spawnsUntilSpecialEnemy)
        {
            var newEnemy = GetSpecialEnemy();

            //Debug.Log(newEnemy);

            // Si queda algun enemigo especial disponible para spawnear, lo activamos y hacemos que ya no este disponible
            if (newEnemy != null)
            {
                SpecialEnemy enemyComponent = newEnemy.GetComponent<SpecialEnemy>();
                enemyComponent.Respawn(spawnPoints[rdm].position);
                _availableEnemies.Remove(newEnemy);
                OnEnemySpawned?.Invoke(enemyComponent.Bug);

                _spawnedEnemiesCounter = 0;
                return;
            }
        }
        
        // Si no hay especiales disponibles o todavia no toca, spawneamos un enemigo normal
        SpawnNormalEnemy(spawnPoints[rdm].position);
        
    }

    GameObject GetSpecialEnemy()
    {
        if (_availableEnemies.Count > 0)
        {
            return _availableEnemies[UnityEngine.Random.Range(0, _availableEnemies.Count)];
        }
        else
        {
            Debug.Log("All enemies alive");
            return null;
        }
    }

    public void SpawnNormalEnemy(Vector3 pos)
    {
        Instantiate(normalEnemyPrefab, pos , Quaternion.identity);

        _spawnedEnemiesCounter++;
    }
    public void EnemyDied(Bug bug)
    {
        // Al morir el enemigo, si es especial, lo volvemos a añadir a la lista para que vuelva a estar disponible
        // y notificamos a la UI
        if(bug) _availableEnemies.Add(bug.gameObject);
        OnEnemyDied?.Invoke(bug);
    }
}
