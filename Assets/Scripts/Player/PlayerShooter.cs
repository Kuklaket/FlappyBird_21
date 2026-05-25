using System.Collections;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Shot _shot;
    [SerializeField] private float _delay = 1f;

    private bool _canShoot = true;

    public void StartShooting()
    {
        if (_shot != null && _canShoot)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    private IEnumerator ShootWithDelay()
    {
        _canShoot = false;
        _shot.Shoot();
        yield return new WaitForSeconds(_delay);
        _canShoot = true;
    }
}