using Code.Utils;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public GameCurrency GameCurrency {get ; private set;}
    
    protected override void Awake()
    {
        base.Awake();
        GameCurrency = new GameCurrency();
    }
}