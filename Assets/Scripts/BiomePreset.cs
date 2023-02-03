using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Biome Preset")]
public class BiomePreset : ScriptableObject
{
    public Sprite[] tiles;
    public float minHeight;
    public float minMoisture;
    public float minHeat;
    public void tileAdd()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>("SpriteSheet Test");
        for (int i = 0; i < allSprites.Length; i++)
        {
            if ((i == 1) || (i == 7) || (i == 19) || (i == 22)) tiles[i] = allSprites[i];
        }
    }
    public Sprite GetTileSprite()
    {
        return tiles[Random.Range(0, tiles.Length)];
    }
    public bool MatchCondition(float height, float moisture, float heat)
    {
        return height >= minHeight && moisture >= minMoisture && heat >= minHeat;
    }
}
