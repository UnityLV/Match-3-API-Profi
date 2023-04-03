using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemInCellInjector
{
    private float _fillTime = 0.03f;

    public async UniTask InjectItemInCell( IItem item, ICell cell, params Vector3[] path)
    {
        cell.Item = item;
        item.View.Show();

        bool isNoPath = path.Length == 0;
        if (isNoPath)
        {
            item.View.SetWorldPosition(cell.View.GetWorldPosition());
        }
        else
        {
            await TryMoveOnPatn(path, item);
        }
    }

    private async UniTask TryMoveOnPatn(Vector3[] path, IItem item)
    {
        item.View.SetWorldPosition(path[0]);
        bool isPathExist = path.Length > 1;
        if (isPathExist)
        {
            await item.View.MoveOn(_fillTime, path: path);
        }
    }
}




