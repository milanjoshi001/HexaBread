
public class GameCurrency
{
    /// <summary>
    /// Diamonds are special currency that can only be used in purchasing items in levels
    /// </summary>
    public int TotalCoins { get; private set; }
    
    /// <summary>
    /// Diamonds are special currency that can only be used in purchasing items in levels
    /// </summary>
    public int TotalDiamonds { get; private set; }

    public void AddCoins(int amount) => TotalCoins += amount;
    public void RemoveCoins(int amount) => TotalCoins -= amount;
    
    public void AddDiamonds(int amount) => TotalDiamonds += amount;
    public void RemoveDiamonds(int amount) => TotalDiamonds -= amount;
}
