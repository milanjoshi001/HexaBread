using System;

public class Dishwasher : BaseAI<DishwasherState>
{
    private void Start()
    {
        _state = DishwasherState.Idle;
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
            case DishwasherState.Idle:
                break;
        }
    }
}

[System.Serializable]
public enum DishwasherState
{
    Idle,
}