using Code.Utils;

public class CoinsManager : Singleton<CoinsManager>
{
    public Coins Coins {get ; private set;}
    
    protected override void Awake()
    {
        base.Awake();
        Coins = new Coins();
    }
}