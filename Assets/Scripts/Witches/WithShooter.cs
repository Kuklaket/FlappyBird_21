using System.Collections;
using UnityEngine;

public class WithShooter : MonoBehaviour
{
    [SerializeField] private Shot _shot;
    [SerializeField] private float _delay = 1.5f;

    private Coroutine _shootingCoroutine;
    private bool _isActive;

    public void StartShooting()
    {
        if (_shootingCoroutine != null)
            StopCoroutine(_shootingCoroutine);

        _shootingCoroutine = StartCoroutine(ShootRoutine());
    }

    public void StopShooting()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;
        }
    }

    public void Activate()
    {
        _isActive = true;
    }

    private IEnumerator ShootRoutine()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(_delay);
            if (_isActive)
                _shot.Shoot();
        }
    }
}
