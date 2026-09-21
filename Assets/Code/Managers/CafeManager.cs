using Code.Utils;
using UnityEngine;

public class CafeManager : Singleton<CafeManager>
{
    [SerializeField] private Seat[] _seats;
    
    public Seat GetEmptySeat()
    {
        foreach (Seat seat in _seats)
        {
            if (seat.TryReserve())
                return seat;
        }

        return null;
    }
}