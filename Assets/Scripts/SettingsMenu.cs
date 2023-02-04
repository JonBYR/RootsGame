using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resDrop;
    void Start()
    {
        resolutions = Screen.resolutions;
        //resDrop = GameObject.Find("Resolution").GetComponent<TMPro.TMP_Dropdown>();
        resDrop.ClearOptions();
        List<string> resLists = new List<string>();
        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string options = resolutions[i].width + " x " + resolutions[i].height;
            resLists.Add(options);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        resDrop.AddOptions(resLists);
        resDrop.value = currentRes;
        resDrop.RefreshShownValue();

    }
    public void SetVolume(float vol)
    {
        Debug.Log(vol);
        audioMixer.SetFloat("Volume", vol);
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void SetFull(bool isFull)
    {
        Screen.fullScreen = isFull;
    }
    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
