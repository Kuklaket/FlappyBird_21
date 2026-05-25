using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WitchSpawner : MonoBehaviour
{
    [SerializeField] private Witch _witchPrefab;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private List<Transform> _spawnAnchors = new List<Transform>();
    [SerializeField] private bool _randomizeAnchors = true;

    private IObjectPool<Witch> _witchPool;
    private List<Witch> _activeWitches = new List<Witch>();

    private void Awake()
    {
        InitializePool();
    }

    private void OnDestroy()
    {
        ReturnAllToPool();
    }

    private void InitializePool()
    {
        _witchPool = new ObjectPool<Witch>(
            createFunc: () => Instantiate(_witchPrefab, transform),
            actionOnGet: (witch) =>
            {
                witch.gameObject.SetActive(true);
                witch.WitchDisabled += ReturnWitchToPool;
            },
            actionOnRelease: (witch) =>
            {
                witch.WitchDisabled -= ReturnWitchToPool;
                witch.transform.SetParent(transform);
                witch.transform.localPosition = Vector3.zero;
                witch.gameObject.SetActive(false);
            },
            actionOnDestroy: (witch) => Destroy(witch.gameObject),
            defaultCapacity: _poolSize,
            maxSize: _poolSize
        );
    }

    public void SpawnWave(int waveSize)
    {
        List<Transform> selectedAnchors = GetSpawnAnchors(waveSize);

        foreach (Transform anchor in selectedAnchors)
        {
            Witch witch = _witchPool.Get();
            witch.transform.position = anchor.position;
            witch.transform.SetParent(anchor);
            witch.Initialize();
            _activeWitches.Add(witch);
        }
    }

    public void ReturnAllToPool()
    {
        List<Witch> witchesToReturn = new List<Witch>(_activeWitches);

        foreach (Witch witch in witchesToReturn)
        {
            if (witch != null && witch.gameObject.activeInHierarchy)
            {
                witch.ReturnToPool();
            }
        }

        _activeWitches.Clear();
    }

    public int ActiveWitchesCount => _activeWitches.Count;

    private List<Transform> GetSpawnAnchors(int count)
    {
        if (_spawnAnchors.Count == 0)
            return new List<Transform>();

        List<Transform> result = new List<Transform>(_spawnAnchors);

        if (_randomizeAnchors)
        {
            for (int i = 0; i < result.Count; i++)
            {
                Transform temp = result[i];
                int randomIndex = Random.Range(i, result.Count);
                result[i] = result[randomIndex];
                result[randomIndex] = temp;
            }
        }

        int takeCount = Mathf.Min(count, result.Count);
        return result.GetRange(0, takeCount);
    }

    private void ReturnWitchToPool(Witch witch)
    {
        if (_activeWitches.Remove(witch))
        {
            _witchPool.Release(witch);
        }
    }
}