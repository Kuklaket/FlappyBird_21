using System;
using UnityEngine;

public class Witch : MonoBehaviour, IDamageable
{
    [SerializeField] private WithShooter _shooter;

    public event Action<Witch> WitchDisabled;

    private bool _isReturnedToPool = false;
    public bool IsActive { get; private set; } = false;

    private void OnDisable()
    {
        _isReturnedToPool = true;
        IsActive = false;
        _shooter.StopShooting();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isReturnedToPool) return;

        if (collision.TryGetComponent(out DeadZone deadZone))
        {
            ReturnToPool();
        }
    }

    public void Initialize()
    {
        _isReturnedToPool = false;
        IsActive = true;
        _shooter.Activate();
        _shooter.StartShooting();
    }

    public void ReturnToPool()
    {
        if (_isReturnedToPool) return;

        _isReturnedToPool = true;
        IsActive = false;
        _shooter.StopShooting();
        WitchDisabled?.Invoke(this);
    }

    public void NotifyHit(GameObject bulletOwner) 
    {
        if (_isReturnedToPool) return;

        if (bulletOwner.TryGetComponent<Player>(out _))
        {
            ReturnToPool();
        }
    }
}