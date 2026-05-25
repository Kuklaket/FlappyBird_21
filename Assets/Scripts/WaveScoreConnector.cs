using UnityEngine;

public class WaveScoreConnector : MonoBehaviour
{
    [SerializeField] private WaveSpawner _spawner;
    [SerializeField] private WaveCounter _waveCounter;

    private void Start()
    {
        if (_spawner != null)
        {
            _spawner.TriggerSpawned += SubscribeTrigger;
        }
    }

    private void OnDestroy()
    {
        if (_spawner != null)
        {
            _spawner.TriggerSpawned -= SubscribeTrigger;
        }
    }

    private void SubscribeTrigger(ScoreTrigger trigger)
    {
        trigger.WavePassed += _waveCounter.AddPoints;
    }

}