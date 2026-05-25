using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Window _startScreen;
    [SerializeField] private Window _endScreen;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private GameResetter _gameResetter;

    private void OnEnable()
    {
        _startScreen.ButtonClicked += PlayButtonClick;
        _endScreen.ButtonClicked += RestartButtonClicked;
        _player.GameOver += EndGame;
    }

    private void OnDisable()
    {
        _startScreen.ButtonClicked -= PlayButtonClick;
        _endScreen.ButtonClicked -= RestartButtonClicked;
        _player.GameOver -= EndGame;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void PlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void RestartButtonClicked()
    {
        _endScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        _gameResetter.ResetAll(_player, _waveSpawner);

        if (_waveSpawner != null)
        {
            _waveSpawner.StartSpawning();
        }

        Time.timeScale = 1;
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        _endScreen.Open();
    }
}