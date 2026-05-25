using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private ScoreTrigger _zonePrefab;

    private List<ScoreTrigger> _allZones = new List<ScoreTrigger>();

    public ScoreTrigger SpawnZone(Vector3 position)
    {
        ScoreTrigger zone = Instantiate(_zonePrefab, position, Quaternion.identity);
        _allZones.Add(zone);
        return zone;
    }

    public void ClearAllZones()
    {
        foreach (ScoreTrigger zone in _allZones)
        {
            if (zone != null)
                Destroy(zone.gameObject);
        }

        _allZones.Clear();
    }
}