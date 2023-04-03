
using System.Collections.Generic;
using UnityEngine;

public class ColorToValueConverter <Value>
{
    private Binds<Value> _binds;
    private Dictionary<Color32, Value> _itemsIdDictionary = new Dictionary<Color32, Value>();    

    public ColorToValueConverter(Binds<Value> binds)
    {
        _binds = binds;
        ConstructDictionary();
    }    

    public Value[,] Convert (Color32[,] colors)
    {
        Value[,] itemsIdMap = new Value[colors.GetLength(0), colors.GetLength(1)];

        for (int x = 0; x < itemsIdMap.GetLength(0); x++)
        {
            for (int y = 0; y < itemsIdMap.GetLength(1); y++)
            {
                bool isContainsId = _itemsIdDictionary.TryGetValue(colors[x, y], out Value itemId);
                itemsIdMap[x, y] = isContainsId ? itemId: _binds.Default;                
            }
        }

        return itemsIdMap;
    }   

    private void ConstructDictionary()
    {
        foreach (var bind in _binds.Value)        
            _itemsIdDictionary.Add(bind.Color, bind.Type);        
    }
}
