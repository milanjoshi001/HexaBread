
public class CashierAI : BaseAI<CashierState>
{
    private void Start()
    {
        _state = CashierState.Idle;
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
            case CashierState.Idle:
                break;
            case CashierState.ReceivingMoney:
                break;
        }
    }
}

[System.Serializable]
public enum CashierState
{
    Idle,
    ReceivingMoney,
}