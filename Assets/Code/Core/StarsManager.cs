using System;
using Code.Utils;

public class StarsManager : Singleton<StarsManager>
{
    public Stars Stars => _stars;

    private Stars _stars;

    protected override void Awake()
    {
        base.Awake();
        _stars = new Stars();
    }
}