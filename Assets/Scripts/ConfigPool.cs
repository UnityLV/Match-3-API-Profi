using System.Collections.Generic;
using UnityEngine;

public class ConfigPool<T>
{
    private ImageToColorConverter _imageConverter;
    private ColorToValueConverter<T> _colorToValueConverter;

    private Queue<T>[] _idPool;

    public ConfigPool(Binds<T> binds)
    {
        _colorToValueConverter = new(binds);
        _imageConverter = new();
    }

    public Queue<T>[] Values => _idPool; 

    public void Init(Texture2D mapTexture)
    {
        T[,] idMap = GetMap(mapTexture);

        int rows = idMap.GetLength(0);
        int collumns = idMap.GetLength(1);

        InitIdPool(collumns);
        FillPool(idMap, rows, collumns);
    }

    private void InitIdPool(int collumns)
    {
        _idPool = new Queue<T>[collumns];

        for (int i = 0; i < Values.Length; i++)
            _idPool[i] = new Queue<T>();        
    }   

    private void FillPool(T[,] idMap, int rows, int collumns)
    {
        for (int collumnIndex = 0; collumnIndex < collumns; collumnIndex++)        
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
                _idPool[collumnIndex].Enqueue(idMap[rowIndex, collumnIndex]);            
        
    }    

    private T[,] GetMap(Texture2D mapTexture) => GetPoolMap(mapTexture);

    private T[,] GetPoolMap(Texture2D texture)
    {
        Color32[,] colorMap = _imageConverter.GetColorMap(texture);
        return _colorToValueConverter.Convert(colorMap);
    }
}
