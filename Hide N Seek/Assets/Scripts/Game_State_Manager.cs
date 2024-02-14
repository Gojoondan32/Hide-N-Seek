public enum GameState
{
    Default,
    MainMenu,
    RunnerTurn,
    PlayerTurn,
    RevealDoors,
    Paused,
    GameOver
}

public class Game_State_Manager
{
    private static Game_State_Manager _instance;
    public static Game_State_Manager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Game_State_Manager();

            return _instance;
        }
    }

    public GameState CurrentGameState { get; private set; }
    public delegate void GameStateChangeHandler(GameState gameState);
    public event GameStateChangeHandler OnGameStateChange;

    private Game_State_Manager()
    {

    }

    public void SetGameState(GameState newGameState)
    {
        if (newGameState == CurrentGameState) return;

        CurrentGameState = newGameState;
        //UnityEngine.Debug.Log($"New game state: {CurrentGameState}");
        OnGameStateChange?.Invoke(CurrentGameState);
    }
}