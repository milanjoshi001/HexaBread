using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackSpawner : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Transform _stackPosParent;
    [SerializeField] private Hexagon _hexagonPrefab;
    [SerializeField] private HexagonStack _hexagonStackPrefab;

    [Header("Settings")]
    [NaughtyAttributes.MinMaxSlider(2,8)]
    [SerializeField] private Vector2Int _minMaxHexCount;
    [SerializeField] private Color[] _colors;

    private int stackCounter;

    private void Awake()
    {
        StackController.OnStackPlaced += StackPlacedCallback;
    }

    private void OnDestroy()
    {
        StackController.OnStackPlaced -= StackPlacedCallback;
    }

    private void StackPlacedCallback(GridCell gridCell)
    {
        stackCounter++;

        if (stackCounter >= 3)
        {
            stackCounter = 0;
            GenerateStacks();
        }
    }

    private void Start()
    {
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
    }

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
        
        return new Color[] { firstColor, secondColor };
    }
}
