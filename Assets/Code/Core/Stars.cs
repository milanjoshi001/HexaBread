
public class Stars
{
    public int TotalStars { get; private set; } = 0;
    
    public void AddStars(int stars) => TotalStars += stars;
    public void RemoveStars(int stars) => TotalStars -= stars;
}
