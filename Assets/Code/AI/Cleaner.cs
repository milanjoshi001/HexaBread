using System;

public class Cleaner : BaseAI<CleanerState>
{
    private void Start()
    {
        _state = CleanerState.Idle;
    }

    private void Update()
    {
        if(GameState.Instance.CurrentGameState != GameStateType.Cafe)
        {
            _agent.isStopped = true;
            return;
        }
        
        if(_agent.isStopped) _agent.isStopped = false;

        switch (_state)
        {
            case CleanerState.Idle:
                break;
        }
    }
}

[System.Serializable]
public enum CleanerState
{
    Idle,
}