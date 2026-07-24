
public class Coins
{
    public int TotalCoins { get; private set; }

    public void AddCoins(int amount) => TotalCoins += amount;
    public void RemoveCoins(int amount) => TotalCoins -= amount;
}
