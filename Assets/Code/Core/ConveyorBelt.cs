using System;
using System.Collections.Generic;
using System.Linq;
using Code.Utils;
using UnityEngine;
using UnityEngine.Splines;

public class ConveyorBelt : Singleton<ConveyorBelt>
{
    public List<GridCell> GridCells => _gridCells;
    [SerializeField] private List<GridCell> _gridCells = new List<GridCell>();
    
    private List<SplineAnimate> _splineAnimates = new List<SplineAnimate>();
    private float _previousOffset = 0f;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _gridCells[i].LoadSplineAnimate();
            if (_gridCells[i].TryGetComponent(out SplineAnimate splineAnimate))
                _splineAnimates.Add(splineAnimate);
        }

        for (int i = 0; i < _splineAnimates.Count; i++)
        {
            _previousOffset += LevelManager.Instance.LevelDataLibrary.LevelDataList[LevelManager.Instance.CurrentLevel].HexagonOffset;
            _splineAnimates[i].StartOffset = _previousOffset;
        }
    }

    public void StartConveyorBelt()
    {
        _splineAnimates.ForEach(h => h.Play());
    }
    
    public void StopConveyorBelt()
    {
        _splineAnimates.ForEach(h => h.Pause());
    }

    public int GetUnoccupiedGridCells() => _gridCells.Count(g => !g.IsOccupied);

    public void ResetConveyorBelt()
    {
        foreach (var gridCell in _gridCells)
        {
            var hexagonStack = gridCell.GetComponentInChildren<HexagonStack>();
            
            if (hexagonStack != null) 
                Destroy(hexagonStack.gameObject);
        }
    }
}
