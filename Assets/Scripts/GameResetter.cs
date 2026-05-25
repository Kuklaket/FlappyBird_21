using UnityEngine;
using System.Collections.Generic;

public class GameResetter : MonoBehaviour
{
    [SerializeField] private WaveCounter _waveCounter;
    [SerializeField] private ZoneSpawner _zoneSpawner;
    [SerializeField] private List<BulletData> _bulletData;

    public void ResetAll(Player player, WaveSpawner waveSpawner)
    {
        ReturnAllToPools(waveSpawner);
        ResetGameState(player);
    }

    private void ResetGameState(Player player)
    {
        player?.ResetPlayer();
        _waveCounter?.ResetCount();
        _zoneSpawner.ClearAllZones();
    }

    private void ReturnAllToPools(WaveSpawner waveSpawner)
    {
        waveSpawner?.ReturnAllToPool();

        foreach (BulletData bulletData in _bulletData)
        {
            bulletData?.ReturnAllActiveBullets();
        }
    }
}