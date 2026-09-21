using System;
using Code.Utils;

public class GameState : Singleton<GameState>
{
    public GameStateType CurrentGameState { get; private set; }

    private void Start() => SetState(GameStateType.Cafe);

    public void SetState(GameStateType gameState)
    {
        CurrentGameState = gameState;
    }
}

[System.Serializable]
public enum GameStateType
{
    Gameplay,
    Cafe
}