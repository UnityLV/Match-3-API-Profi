using UnityEngine;

public class ImageToColorConverter
{
    private const int ChanalsInColor = 3;
    private const int Alpha = 255;

    public Color32[,] GetColorMap(Texture2D texture)
    {
        //Color32[] colors = GetColors(texture);
        Color32[] colors = texture.GetPixels32();

        Color32[,] colorsMap = colors.ArrayTo2DArray(texture.height, texture.width);

        return colorsMap;
    }  

    private Color32[] GetColors(Texture2D texture)
    {
        int width = texture.width;
        int height = texture.height;
        byte[] bytes = texture.GetRawTextureData();
        int length = height * width;
        Color32[] colors = new Color32[length];


        for (int i = 0; i < length; i++)
        {
            byte red = bytes[i * ChanalsInColor];
            byte green = bytes[i * ChanalsInColor + 1];
            byte blue = bytes[i * ChanalsInColor + 2];
            colors[i] = new Color32(red, green, blue, Alpha);
        }

        return colors;

    }

}
