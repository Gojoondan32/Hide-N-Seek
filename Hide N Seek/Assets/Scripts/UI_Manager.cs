using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    private int _playerScore;
    private int _playerLives;
    [SerializeField] private List<GameObject> _lives;
    private int _startingLives = 3;
    [SerializeField] private TextMeshProUGUI _scoreText;

    #region References
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _inGame;
    [SerializeField] private GameObject _gameOver;
    #endregion
    private void Awake() {
        Game_State_Manager.Instance.OnGameStateChange += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState) {
        switch (gameState) {
            case GameState.MainMenu:
                _playerScore = 0;
                _playerLives = _startingLives;
                ResetLivesUI();
                HideAllScreens();
                _mainMenu.SetActive(true);
                break;
            case GameState.Paused:
                // Show pause UI
                break;
            case GameState.GameOver:
                HideAllScreens();
                _gameOver.SetActive(true);
                break;
            default:
                HideAllScreens();
                _inGame.SetActive(true);
                break;
        }
    }

    private void HideAllScreens() {
        _mainMenu.SetActive(false);
        _inGame.SetActive(false);
        _gameOver.SetActive(false);
    }

    public void StartGame() {
        Game_State_Manager.Instance.SetGameState(GameState.RunnerTurn);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void AddToPlayerScore(int amount) {
        _playerScore += amount;
        _scoreText.text = _playerScore.ToString();
    }

    public void RemoveFromPlayerLives(int amount) {
        _playerLives -= amount;
        _lives[_playerLives].SetActive(false);
        if (_playerLives <= 0) {
            Game_State_Manager.Instance.SetGameState(GameState.GameOver);
        }
    }

    private void ResetLivesUI() {
        foreach (GameObject life in _lives) {
            life.SetActive(true);
        }
    }
}
