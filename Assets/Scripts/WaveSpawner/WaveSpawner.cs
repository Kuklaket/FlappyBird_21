using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WitchSpawner _witchSpawner;
    [SerializeField] private ZoneSpawner _zoneSpawner;
    [SerializeField] private int _witchesPerWave = 3;
    [SerializeField] private float _timeBetweenWaves = 2f;
    [SerializeField] private Transform _zoneSpawnPoint;

    public event Action<ScoreTrigger> TriggerSpawned;
    public event Action<int> WaveSpawned;

    private Coroutine _spawnCoroutine;
    private int _currentWaveIndex = 0;

    private void Awake()
    {
        if (_witchSpawner == null)
            _witchSpawner = GetComponent<WitchSpawner>();

        if (_zoneSpawner == null)
            _zoneSpawner = GetComponent<ZoneSpawner>();
    }

    private void OnDestroy()
    {
        ReturnAllToPool();
    }

    public void StartSpawning()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = StartCoroutine(SpawnWaves());
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    public void ReturnAllToPool()
    {
        StopSpawning();
        _witchSpawner.ReturnAllToPool();
        _zoneSpawner.ClearAllZones();
    }

    private IEnumerator SpawnWaves()
    {
        while (enabled)
        {
            _witchSpawner.SpawnWave(_witchesPerWave);

            Vector3 zonePosition = (_zoneSpawnPoint ?? transform).position;

            ScoreTrigger zone = _zoneSpawner.SpawnZone(zonePosition);
            TriggerSpawned?.Invoke(zone);

            WaveSpawned?.Invoke(_currentWaveIndex);
            _currentWaveIndex++;

            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }
}