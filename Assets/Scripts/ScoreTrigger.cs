using System;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public event Action WavePassed;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
        {
            WavePassed?.Invoke();
            Destroy(gameObject);
        }
    }
}