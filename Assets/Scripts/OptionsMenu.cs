using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullScreenToggle, vsyncToggle;

    public List<ResolutionItem> resolutions = new List<ResolutionItem>();
    private int currentResolution;

    public TMP_Text resolutionLabel;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
        {
            vsyncToggle.isOn = true;
        }

        bool foundResolution = false;

        for(int i = 0; i < resolutions.Count; i++)
        {
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolution = i;
                foundResolution = true;
                UpdateResolutionLabel();
            }
        }

        if (!foundResolution)
        {
            ResolutionItem newResolution = new ResolutionItem();
            newResolution.width = Screen.width;
            newResolution.height = Screen.height;

            resolutions.Add(newResolution);
            currentResolution = resolutions.Count - 1;

            UpdateResolutionLabel();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResolutionLeft()
    {
      currentResolution--;

      if (currentResolution < 0)
      {
        currentResolution = 0;
      }

      UpdateResolutionLabel();
    }

    public void ResolutionRight()
    {
      currentResolution++;

      if (currentResolution > resolutions.Count - 1)
      {
        currentResolution = resolutions.Count - 1;
      }

      UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel()
    {
      resolutionLabel.text = resolutions[currentResolution].width.ToString() + " x " + resolutions[currentResolution].height.ToString();
    }

    public void applyGraphics()
    {
        // Screen.fullScreen = fullScreenToggle.isOn;

        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(
          resolutions[currentResolution].width,
          resolutions[currentResolution].height,
          fullScreenToggle.isOn
          );
    }
}

[System.Serializable]
public class ResolutionItem
{
    public int width, height;
}
