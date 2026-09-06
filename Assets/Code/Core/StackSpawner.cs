using System.Collections.Generic;
using System.Linq;
using Code.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StackSpawner : Singleton<StackSpawner>
{
    [Header("Elements")] 
    [SerializeField] private Transform _stackPosParent;
    [SerializeField] private List<AllFoodIdentities> _allFoodIdentitiesList;
    
    private int itemCounter;

    protected override void Awake()
    {
        StackController.OnStackPlaced += StackPlacedCallback;

        PowerUpUI.OnStackRegenerate += RegenerateStack;
        MergeManager.OnLastStackPlaced += RegenerateStack;
    }
    
    private void Start()
    {
        PowerUpUI.OnStackCollapsed += DisableStackParent;
    }

    private void OnDestroy()
    {
        StackController.OnStackPlaced -= StackPlacedCallback;
        
        PowerUpUI.OnStackRegenerate -= RegenerateStack;
        MergeManager.OnLastStackPlaced -= RegenerateStack;
        PowerUpUI.OnStackCollapsed -= DisableStackParent;
    }

    private void DisableStackParent() => _stackPosParent.gameObject.SetActive(false);
    public void EnableStackParent() => _stackPosParent.gameObject.SetActive(true);

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
        for (int i = 0; i < _stackPosParent.childCount; i++)
        {
            _stackPosParent.GetChild(i).Clear();
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
        for (int i = 0; i < _stackPosParent.childCount; i++)
        {
            GenerateStack(_stackPosParent.GetChild(i));
        }
    }

    private void GenerateStack(Transform parent)
    {
        int randomIdentity =  Random.Range(0, _allFoodIdentitiesList.Count);
        
        FoodItem foodItemInstance = Instantiate(_allFoodIdentitiesList[randomIdentity].foodItemStack, parent);
        
    }

    public void Activate(bool value) => _stackPosParent.gameObject.SetActive(value);
    
    [System.Serializable]
    public struct AllFoodIdentities
    {
        public FoodItem.FoodIdentity FoodIdentity;
        [FormerlySerializedAs("HexagonStack")] public FoodItem foodItemStack;
    }
}
