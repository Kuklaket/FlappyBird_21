using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerShooter _shooter;

    public event UnityAction GameOver;

    private void OnEnable()
    {
        _inputReader.JumpPressed += _mover.Jump;
        _inputReader.AttackPressed += _shooter.StartShooting;
    }

    private void OnDisable()
    {
        _inputReader.JumpPressed -= _mover.Jump;
        _inputReader.AttackPressed -= _shooter.StartShooting;
    }

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Witch _) || collision.TryGetComponent(out DeadZone _))
        {
            Die();
        }
    }

    public void NotifyHit(GameObject bulletOwner)
    {
        if (bulletOwner.TryGetComponent<Witch>(out _))
        {
            Die();
        }
    }

    public void ResetPlayer()
    {
        _mover.Reset();
    }

    public void Die()
    {
        RenderColor();
        GameOver?.Invoke();
    }

    private void RenderColor()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }    
}
