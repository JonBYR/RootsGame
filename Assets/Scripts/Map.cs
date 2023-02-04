using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public BiomePreset[] biomes;
    public GameObject tilePrefab;
    [Header("Dimensions")]
    public int width = 50;
    public int height = 50;
    public float scale = 1.0f;
    public Vector2 offset;
    [Header("Height Map")]
    private List<Wave> heightWaves;
    public float[,] heightMap;
    [Header("Moisture Map")]
    private List<Wave> moistureWaves;
    private float[,] moistureMap;
    [Header("Heat Map")]
    private List<Wave> heatWaves;
    public float[,] heatMap;
    private Transform Parent;
    void Start()
    {
        heightWaves = new List<Wave>();
        moistureWaves = new List<Wave>();
        heatWaves = new List<Wave>();
        Wave height1 = new Wave(Random.Range(0f, 100f), 0.05f, 1);
        Wave height2 = new Wave(Random.Range(0f, 100f), 0.1f, 0.5f);
        heightWaves.Add(height1);
        heightWaves.Add(height2);
        Wave moist = new Wave(Random.Range(0f, 100f), 0.03f, 1);
        moistureWaves.Add(moist);
        Wave heat1 = new Wave(Random.Range(0f, 100f), 0.04f, 1);
        Wave heat2 = new Wave(Random.Range(0f, 100f), 0.02f, 0.5f);
        heatWaves.Add(heat1);
        heatWaves.Add(heat2);
        Parent = GameObject.Find("Tiles").transform;
        GenerateMap();
    }
    void GenerateMap()
    {
        heightMap = NoiseGeneration.generate(width, height, scale, heightWaves, offset);
        moistureMap = NoiseGeneration.generate(width, height, scale, moistureWaves, offset);
        heatMap = NoiseGeneration.generate(width, height, scale, heatWaves, offset);
        for(int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                GameObject tile = GetBiome(heightMap[x, y], moistureMap[x, y], heatMap[x, y]).GetTileSprite();
                var inst = Instantiate(tile, new Vector3(x, 0, y), Quaternion.identity);
                inst.transform.parent = Parent;
            }
        }
    }
    BiomePreset GetBiome(float height, float moisture, float heat)
    {
        BiomePreset biomeToReturn = null;
        List<BiomeTempData> biomeTemp = new List<BiomeTempData>();
        foreach(BiomePreset biome in biomes)
        {
            if (biome.MatchCondition(height, moisture, heat)) biomeTemp.Add(new BiomeTempData(biome));
        }
        float curVal = 0.0f;
        foreach(BiomeTempData biome in biomeTemp)
        {
            if (biomeToReturn == null)
            {
                biomeToReturn = biome.biome;
                curVal = biome.GetDiffValue(height, moisture, heat);
            }
            else
            {
                if(biome.GetDiffValue(height, moisture, heat) < curVal)
                {
                    biomeToReturn = biome.biome;
                    curVal = biome.GetDiffValue(height, moisture, heat);
                }
            }
        }
        if (biomeToReturn == null) biomeToReturn = biomes[0];
        return biomeToReturn;
    }
}
public class BiomeTempData
{
    public BiomePreset biome;
    public BiomeTempData(BiomePreset preset)
    {
        biome = preset;
    }
    public float GetDiffValue(float height, float moisture, float heat)
    {
        return (height - biome.minHeight) + (moisture - biome.minMoisture) + (heat - biome.minHeat);
    }
}
