using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private Transform _shootPoint;

    public void Shoot()
    {
        if (_bulletData == null)
        {
            return;
        }

        if (_shootPoint == null)
        {
            return;
        }

        _bulletData.GetBullet(_shootPoint, gameObject);
    }
}