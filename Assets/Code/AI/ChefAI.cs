
public class ChefAI : BaseAI<ChefState>
{
    private void Start()
    {
        _state = ChefState.Idle;
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
            case ChefState.Idle:
                break;
            case ChefState.ReceivesOrder:
                break;
            case ChefState.PreparesOrder:
                break;
        }
    }
}

[System.Serializable]
public enum ChefState
{
    Idle,
    ReceivesOrder,
    PreparesOrder,
}