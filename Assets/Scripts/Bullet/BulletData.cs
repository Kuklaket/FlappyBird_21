using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Game/Bullet Data")]
public class BulletData : ScriptableObject
{
    public Sprite BulletSprite;
    public float Speed = 10f;
    public Vector2 Direction = Vector2.left;
    public int DefaultCapacity = 10;
    public int MaxSize = 20;

    private IObjectPool<GameObject> _pool;
    private GameObject _poolParent;
    private List<GameObject> _activeBullets = new List<GameObject>();

    private void OnEnable()
    {
        CreatePool();
    }

    private void OnDisable()
    {
        _activeBullets.Clear();
        _pool = null;
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: CreateNewBullet,
            actionOnGet: ActivateBullet,
            actionOnRelease: DeactivateBullet,
            actionOnDestroy: DestroyBullet,
            collectionCheck: true,
            defaultCapacity: DefaultCapacity,
            maxSize: MaxSize
        );
    }

    public GameObject GetBullet(Transform shootPoint, GameObject owner)
    {
        if (_pool == null)
        {
            CreatePool();
        }

        GameObject bulletObj = _pool.Get();
        bulletObj.transform.position = shootPoint.position;

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Initialize(this, owner);

        return bulletObj;
    }

    public void ReturnBullet(GameObject bulletObj)
    {
        if (_pool != null)
        {
            _pool.Release(bulletObj);
        }
        else
        {
            Destroy(bulletObj);
        }
    }

    public void ReturnAllActiveBullets()
    {
        List<GameObject> bulletsToReturn = new List<GameObject>(_activeBullets);

        foreach (GameObject bullet in bulletsToReturn)
        {
            if (bullet != null && bullet.activeInHierarchy)
            {
                Bullet bulletComponent = bullet.GetComponent<Bullet>();

                if (bulletComponent != null && !bulletComponent.IsReturnedToPool)
                {
                    ReturnBullet(bullet);
                }
            }
        }

        _activeBullets.Clear();
    }

    private GameObject CreateNewBullet()
    {
        GameObject bulletObj = new GameObject();

        SpriteRenderer renderer = bulletObj.AddComponent<SpriteRenderer>();
        CircleCollider2D collider = bulletObj.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        Rigidbody2D rb = bulletObj.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        Bullet bullet = bulletObj.AddComponent<Bullet>();

        return bulletObj;
    }

    private void ActivateBullet(GameObject bulletObject)
    {
        bulletObject.SetActive(true);
        _activeBullets.Add(bulletObject);

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.ResetBullet();
    }

    private void DeactivateBullet(GameObject bulletObject)
    {
        bulletObject.SetActive(false);
        _activeBullets.Remove(bulletObject);

        if (_poolParent == null)
        {
            _poolParent = new GameObject();
        }
        bulletObject.transform.SetParent(_poolParent.transform);
    }

    private void DestroyBullet(GameObject bulletObject)
    {
        _activeBullets.Remove(bulletObject);
        Destroy(bulletObject);
    }
}