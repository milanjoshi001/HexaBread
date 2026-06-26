using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackSpawner : MonoBehaviour
{
    public static StackSpawner Instance;
    
    [Header("Elements")] 
    [SerializeField] private Transform _stackPosParent;
    [SerializeField] private Hexagon _hexagonPrefab;
    [SerializeField] private HexagonStack _hexagonStackPrefab;

    [Header("Settings")]
    [SerializeField] private Vector2Int _minMaxHexCount;
    [SerializeField] private Color[] _colors;

    public List<HexagonStack> Stacks { get; private set; } = new List<HexagonStack>();

    private int stackCounter;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        StackController.OnStackPlaced += StackPlacedCallback;
        LevelCompleteUI.OnLevelComplete += ResetStacks;
        LevelCompleteUI.OnLevelComplete += GenerateStacks;

        GameplayUI.OnStackRegenerate += RegenerateStack;
        MergeManager.OnLastStackPlaced += RegenerateStack;
    }
    
    private void Start()
    {
        GenerateStacks();
        GameplayUI.OnStackCollapsed += DisableStackParent;
    }

    private void OnDestroy()
    {
        StackController.OnStackPlaced -= StackPlacedCallback;
        LevelCompleteUI.OnLevelComplete -= ResetStacks;
        LevelCompleteUI.OnLevelComplete -= GenerateStacks;
        
        GameplayUI.OnStackRegenerate -= RegenerateStack;
        MergeManager.OnLastStackPlaced -= RegenerateStack;
        GameplayUI.OnStackCollapsed -= DisableStackParent;
    }

    private void DisableStackParent() => _stackPosParent.gameObject.SetActive(false);
    public void EnableStackParent() => _stackPosParent.gameObject.SetActive(true);

    private void StackPlacedCallback(GridCell gridCell)
    {
        stackCounter++;

        if (stackCounter >= 3)
        {
            stackCounter = 0;
            GenerateStacks();
        }
    }

    private void ResetStacks()
    {
        for (int i = 0; i < _stackPosParent.childCount; i++)
        {
            _stackPosParent.GetChild(i).Clear();
        }

        stackCounter = 0;
    }

    private void RegenerateStack()
    {
        ResetStacks();
        GenerateStacks();
    }

    private void GenerateStacks()
    {
        for (int i = 0; i < _stackPosParent.childCount; i++)
        {
            GenerateStack(_stackPosParent.GetChild(i));
        }
    }

    private void GenerateStack(Transform parent)
    {
        HexagonStack hexStack = Instantiate(_hexagonStackPrefab, parent.position, Quaternion.identity, parent);
        hexStack.name = $"Stack {parent.GetSiblingIndex()}";

        int amount = Random.Range(_minMaxHexCount.x, _minMaxHexCount.y);
        int firstColorHexagonCount = Random.Range(0, amount);

        Color[] colorArray = GetRandomColors();

        for (int i = 0; i < amount; i++)
        {
            Vector3 hexagonLocalPos = Vector3.up * i * 0.2f;
            Vector3 spawnPosition = hexStack.transform.TransformPoint(hexagonLocalPos);
            
            Hexagon hexagonInstance = Instantiate(_hexagonPrefab, spawnPosition, Quaternion.identity, hexStack.transform);

            hexagonInstance.Color = i < firstColorHexagonCount ? colorArray[0] : colorArray[1];
            hexagonInstance.Configure(hexStack);
            hexStack.AddHexagon(hexagonInstance);
        }
        
        hexStack.SetTotalSimilarHexagons();
        
        Stacks.Add(hexStack);
    }

    public void Activate(bool value) => _stackPosParent.gameObject.SetActive(value);

    private Color[] GetRandomColors()
    {
        List<Color> colorList = new List<Color>();
        colorList.AddRange(_colors);

        if (colorList.Count <= 0)
        {
            Debug.LogError("No color found!");
            return null;
        }
        
        Color firstColor = colorList.OrderBy(x=>Random.value).First();
        
        colorList.Remove(firstColor);

        if (colorList.Count <= 0)
        {
            Debug.LogError("Only one color was found!");
            return null;
        }
        
        Color secondColor = colorList.OrderBy(x => Random.value).First();

        colorList.Remove(secondColor);
        
        if (colorList.Count <= 0)
        {
            Debug.LogError("No color found!");
            return null;
        }
        
        Color thirdColor = colorList.OrderBy(x=>Random.value).First();
        
        return new Color[] { firstColor, secondColor, thirdColor };
    }
}
