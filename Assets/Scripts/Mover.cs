using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector2 _direction = Vector2.left;

    private bool _isMoving = true;

    private void OnEnable()
    {
        _isMoving = true;
    }

    private void OnDisable()
    {
        _isMoving = false;
    }

    private void Update()
    {
        if (!_isMoving) return;

        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}