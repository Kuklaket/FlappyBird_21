using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletData _bulletData;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private GameObject _owner;
    private bool _isPlayerBullet;
    private bool _isReturnedToPool = false;

    public bool IsReturnedToPool => _isReturnedToPool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isReturnedToPool)
            return;

        if (collision.gameObject == _owner)
            return;

        if (collision.TryGetComponent(out DeadZone _))
        {
            ReturnToPool();
            return;
        }

        if (collision.TryGetComponent(out IDamageable damageable) && CanHitTarget(collision))
        {
            damageable.NotifyHit(_owner);
            ReturnToPool();
        }
    }

    public void Initialize(BulletData data, GameObject owner)
    {
        _isReturnedToPool = false;
        _isPlayerBullet = owner.TryGetComponent(out Player player);
        _bulletData = data;
        _owner = owner;

        if (_spriteRenderer != null && _bulletData.BulletSprite != null)
        {
            _spriteRenderer.sprite = _bulletData.BulletSprite;
        }

        Vector2 shootDirection = _bulletData.Direction;
        _rigidbody.linearVelocity = shootDirection * _bulletData.Speed;
    }

    public void ResetBullet()
    {
        _isReturnedToPool = false;

        if (_rigidbody != null)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
        }

        _owner = null;

        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null)
            collider.isTrigger = true;

        if (_spriteRenderer != null)
            _spriteRenderer.sprite = null;
    }

    private bool CanHitTarget(Collider2D collision)
    {
        if (_isPlayerBullet)
            return true;

        return collision.TryGetComponent<Player>(out _);
    }

    private void ReturnToPool()
    {
        if (_isReturnedToPool)
            return;

        _isReturnedToPool = true;

        if (_bulletData != null)
        {
            _bulletData.ReturnBullet(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}