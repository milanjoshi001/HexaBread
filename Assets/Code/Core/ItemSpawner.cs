using System.Collections.Generic;
using Code.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : Singleton<ItemSpawner>
{
    [Header("Elements")] 
    [SerializeField] private Transform _itemPosParent;
    [SerializeField] private List<AllFoodIdentities> _allFoodIdentitiesList;
    
    private int itemCounter;

    protected override void Awake()
    {
        ItemController.OnStackPlaced += StackPlacedCallback;

        PowerUpUI.OnStackRegenerate += RegenerateStack;
        MergeManager.OnLastStackPlaced += RegenerateStack;
    }
    
    private void Start()
    {
        PowerUpUI.OnStackCollapsed += DisableStackParent;
    }

    private void OnDestroy()
    {
        ItemController.OnStackPlaced -= StackPlacedCallback;
        
        PowerUpUI.OnStackRegenerate -= RegenerateStack;
        MergeManager.OnLastStackPlaced -= RegenerateStack;
        PowerUpUI.OnStackCollapsed -= DisableStackParent;
    }

    private void DisableStackParent() => _itemPosParent.gameObject.SetActive(false);
    public void EnableStackParent() => _itemPosParent.gameObject.SetActive(true);

    private void StackPlacedCallback(GridCell gridCell)
    {
        itemCounter++;

        if (itemCounter >= 3)
        {
            itemCounter = 0;
            GenerateStacks();
        }
    }

    public void ResetStacks()
    {
        for (int i = 0; i < _itemPosParent.childCount; i++)
        {
            _itemPosParent.GetChild(i).Clear();
        }

        itemCounter = 0;
    }

    private void RegenerateStack()
    {
        ResetStacks();
        GenerateStacks();
    }

    public void GenerateStacks()
    {
        for (int i = 0; i < _itemPosParent.childCount; i++)
        {
            GenerateStack(_itemPosParent.GetChild(i));
        }
    }

    private void GenerateStack(Transform parent)
    {
        int randomIdentity =  Random.Range(0, _allFoodIdentitiesList.Count);
        
        FoodItem foodItemInstance = Instantiate(_allFoodIdentitiesList[randomIdentity].FoodItemStack, parent);
        
    }

    public void Activate(bool value) => _itemPosParent.gameObject.SetActive(value);
    
    [System.Serializable]
    public struct AllFoodIdentities
    {
        public FoodItem.FoodIdentity FoodIdentity;
        public FoodItem FoodItemStack;
    }
}
