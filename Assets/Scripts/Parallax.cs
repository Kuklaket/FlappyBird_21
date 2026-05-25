using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;

    private RawImage _image;
    private Rect _uvRect;

    private void Start()
    {
        _image = GetComponent<RawImage>();
        _uvRect = _image.uvRect;
    }

    private void Update()
    {
        _uvRect.x += _speed * Time.deltaTime;

        _image.uvRect = _uvRect;
    }
}
