using UnityEngine;

public abstract class TextureMapFactory<P, I> : ConfigFactory<P, I>
{
    public abstract void SetMap(Texture2D texture);
}
