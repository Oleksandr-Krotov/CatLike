﻿using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
	public enum DisplayMode { FPS, MS }
	
	[SerializeField]
	DisplayMode displayMode = DisplayMode.FPS;
	
    [SerializeField]
    private TextMeshProUGUI display = default;
    
    [SerializeField, Range(0.1f, 2f)]
    private float sampleDuration = 1f;
    
    private int frames;
    float duration, bestDuration = float.MaxValue, worstDuration;

    void Update()
    {
	    float frameDuration = Time.unscaledDeltaTime;
	    frames += 1;
	    duration += frameDuration;
	    
	    TryUpdateBestDuration(frameDuration);
	    TryUpdateWorstDuration(frameDuration);
	    
	    if (duration >= sampleDuration)
	    {
		    UpdateUI();
		    ResetValues();
	    }
    }

    private void TryUpdateWorstDuration(float frameDuration)
    {
	    if (frameDuration > worstDuration)
	    {
		    worstDuration = frameDuration;
	    }
    }

    private void TryUpdateBestDuration(float frameDuration)
    {
	    if (frameDuration < bestDuration)
	    {
		    bestDuration = frameDuration;
	    }
    }
    
    private void ResetValues()
    {
	    frames = 0;
	    duration = 0f;
	    bestDuration = float.MaxValue;
	    worstDuration = 0f;
    }

    private void UpdateUI()
    {
	    if (displayMode == DisplayMode.FPS) {
		    display.SetText(
			    "FPS\n{0:0}\n{1:0}\n{2:0}",
			    1f / bestDuration,
			    frames / duration,
			    1f / worstDuration
		    );
	    }
	    else {
		    display.SetText(
			    "MS\n{0:1}\n{1:1}\n{2:1}",
			    1000f * bestDuration,
			    1000f * duration / frames,
			    1000f * worstDuration
		    );
	    }
    }
}
