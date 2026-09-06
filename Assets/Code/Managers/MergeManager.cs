using System;
using System.Collections;
using System.Collections.Generic;
using Code.Utils;
using UnityEngine;

public class MergeManager : Singleton<MergeManager>
{
    [Header("Level")]
    [SerializeField] private int _maxMoves = 50;


    [Header("Neighbour Detection")]
    [SerializeField] private float _neighborRadius = 1.25f;

    [Header("Collection")]
    [SerializeField] private float _collectionDelay = 0.15f;
    [SerializeField] private float _collectionItemDelay = 0.04f;

    public static Action<int> OnMoveChanged;
    public static Action<FoodItem.FoodIdentity, int, int> OnFoodCollected;
    public static Action OnLevelComplete;
    public static Action OnGameOver;
    public static Action OnLastStackPlaced;
    public int MovesRemaining { get; private set; }
    public bool IsLevelCompleted { get; private set; }
    public bool IsGameOver { get; private set; }

    private List<CollectionObjective> _objectives => LevelManager.Instance.GetLevelData().CollectionObjectives;

    protected override void Awake()
    {
        MovesRemaining = _maxMoves;

        ResetObjectives();

        StackController.OnFoodPlaced += FoodPlaced;
        StackController.OnFoodSwapped += FoodSwapped;
    }

    private void OnDestroy()
    {
        StackController.OnFoodPlaced -= FoodPlaced;
        StackController.OnFoodSwapped -= FoodSwapped;
    }
    
    public void InitializeLevel(int maxMoves)
    {
        _maxMoves = maxMoves;

        MovesRemaining = Mathf.Max(0, maxMoves);
        
        ResetObjectives();

        IsLevelCompleted = false;
        IsGameOver = false;

        OnMoveChanged?.Invoke(MovesRemaining);
    }

    private void ResetObjectives()
    {
        foreach (CollectionObjective objective in _objectives)
        {
            objective.Reset();
        }
    }
    
    private void FoodPlaced(GridCell cell)
    {
        if (IsLevelCompleted || IsGameOver)
            return;

        ConsumeMove();

        StartCollectionCheck(cell);
    }
    
    private void FoodSwapped(GridCell firstCell, GridCell secondCell)
    {
        if (IsLevelCompleted || IsGameOver)
            return;

        ConsumeMove();
        StartCollectionCheck(firstCell);
        StartCollectionCheck(secondCell);
    }
    
    private void StartCollectionCheck(GridCell cell)
    {
        if (cell == null)
        {
            CheckGameState();
            return;
        }

        StartCoroutine(ProcessCollection(cell));
    }

    private IEnumerator ProcessCollection(GridCell startCell)
    {
        yield return null;

        if (IsLevelCompleted || IsGameOver)
            yield break;

        if (startCell == null || !startCell.IsOccupied)
        {
            CheckGameState();
            yield break;
        }

        GridCell matchingCell = FindMatchingNeighbor(startCell);

        if (matchingCell == null)
        {
            CheckGameState();
            yield break;
        }

        FoodItem.FoodIdentity identity = startCell.FoodItem.FoodItemIdentity;

        yield return CollectPair(startCell, matchingCell, identity);

        CheckGameState();
    }

    private GridCell FindMatchingNeighbor(GridCell cell)
    {
        if (cell == null || !cell.IsOccupied || cell.FoodItem == null)
            return null;

        FoodItem.FoodIdentity identity = cell.FoodItem.FoodItemIdentity;

        List<GridCell> neighbors = GetNeighborGridCells(cell);

        foreach (GridCell neighbor in neighbors)
        {
            if (neighbor == null || !neighbor.IsOccupied)
                continue;

            if (neighbor.FoodItem == null)
                continue;

            if (neighbor.FoodItem.FoodItemIdentity != identity)
                continue;

            return neighbor;
        }

        return null;
    }

    private IEnumerator CollectPair(GridCell firstCell, GridCell secondCell, FoodItem.FoodIdentity identity)
    {
        if (firstCell == null || secondCell == null)
            yield break;

        FoodItem firstFood = firstCell.FoodItem;

        FoodItem secondFood = secondCell.FoodItem;

        if (firstFood == null || secondFood == null)
            yield break;

        firstCell.ClearFood();
        secondCell.ClearFood();

        firstFood.ActivateCollider(false);
        secondFood.ActivateCollider(false);

        AddCollectedFood(identity, 2);

        OnFoodCollected?.Invoke(identity, 2, GetCollectedAmount(identity));

        firstFood.Vanish(0f);

        secondFood.Vanish(_collectionItemDelay);

        yield return new WaitForSeconds(_collectionDelay);
    }
    
    private List<GridCell> GetNeighborGridCells(GridCell cell)
    {
        List<GridCell> result = new List<GridCell>();

        if (cell == null)
            return result;

        if (PlayGrid.Instance == null)
            return result;

        Vector3 cellPosition = cell.transform.position;

        foreach (GridCell candidate in PlayGrid.Instance.GridCells)
        {
            if (candidate == null || candidate == cell)
                continue;

            if (!candidate.IsOccupied)
                continue;

            float distance = Vector3.Distance(cellPosition, candidate.transform.position);

            if (distance <= _neighborRadius)
            {
                result.Add(candidate);
            }
        }

        return result;
    }
    
    private void AddCollectedFood(FoodItem.FoodIdentity identity, int amount)
    {
        CollectionObjective objective = GetObjective(identity);

        if (objective == null)
            return;

        objective.CollectedAmount += amount;

        objective.CollectedAmount = Mathf.Min(objective.CollectedAmount, objective.RequiredAmount);
    }

    private CollectionObjective GetObjective(FoodItem.FoodIdentity identity)
    {
        foreach (CollectionObjective objective in _objectives)
        {
            if (objective.FoodIdentity == identity)
                return objective;
        }

        return null;
    }

    public int GetCollectedAmount(FoodItem.FoodIdentity identity)
    {
        CollectionObjective objective = GetObjective(identity);

        return objective == null ? 0 : objective.CollectedAmount;
    }

    public int GetRequiredAmount(FoodItem.FoodIdentity identity)
    {
        CollectionObjective objective = GetObjective(identity);

        return objective == null ? 0 : objective.RequiredAmount;
    }

    public bool IsObjectiveCompleted(FoodItem.FoodIdentity identity)
    {
        CollectionObjective objective = GetObjective(identity);

        return objective != null && objective.IsCompleted;
    }

    private bool AreAllObjectivesCompleted()
    {
        if (_objectives.Count == 0)
            return false;

        foreach (CollectionObjective objective in _objectives)
        {
            if (!objective.IsCompleted)
                return false;
        }

        return true;
    }
    
    private void ConsumeMove()
    {
        if (MovesRemaining <= 0)
            return;

        MovesRemaining--;
        
        OnMoveChanged?.Invoke(MovesRemaining);
    }
    
    private void CheckGameState()
    {
        if (IsLevelCompleted || IsGameOver)
            return;

        if (AreAllObjectivesCompleted())
        {
            CompleteLevel();
            return;
        }

        if (MovesRemaining <= 0)
            GameOver();
    }

    private void CompleteLevel()
    {
        if (IsLevelCompleted || IsGameOver)
            return;

        IsLevelCompleted = true;
        
        OnLevelComplete?.Invoke();
    }

    private void GameOver()
    {
        if (IsGameOver || IsLevelCompleted)
            return;

        IsGameOver = true;
        
        OnLastStackPlaced?.Invoke();
        OnGameOver?.Invoke();
    }
}