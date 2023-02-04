using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoiseGeneration : MonoBehaviour
{
    public static float[,] generate(int width, int height, float scale, List<Wave> waves, Vector2 offset)
    {
        float[,] noiseMap = new float[width, height];
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                float samplePosX = (float)x * scale + offset.x;
                float samplePosY = (float)y * scale + offset.y;
                float normalize = 0.0f;
                foreach(Wave wave in waves)
                {
                    //wave.setSeed(Random.Range(0f, 500f));
                    noiseMap[x, y] += wave.amplitude * Mathf.PerlinNoise(samplePosX * wave.frequency + wave.seed, samplePosY * wave.frequency + wave.getSeed());
                    normalize += wave.amplitude;
                }
                noiseMap[x, y] /= normalize;
            }
        }
        return noiseMap;
    }
}
[System.Serializable]
public class Wave
{
    
    public float seed;
    public float frequency;
    public float amplitude;
    public Wave(float s, float f, float a)
    {
        seed = s;
        frequency = f;
        amplitude = a;
    }
    public void setSeed(float s)
    {
        seed = s;
    }
    public float getSeed() { return seed; }
}