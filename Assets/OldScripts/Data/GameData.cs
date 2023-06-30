using System.Collections.Generic;
using UnityEngine;

public class GameData
{

    public List<SlideModel> loadedSlides = new List<SlideModel>();
    public Dictionary<SlideModel, DanceVideoViewController> slideDanceObjectMap = new Dictionary<SlideModel, DanceVideoViewController>();

    
    public bool fetcMoreData = false;
    public int activeItemIndex = 0;


    //config parametere
    public int fetchDataCount = 3;
    public float slideAnimationDuration = 0.33f;
    public float touchSensitivity = 2.0f;
    public int viewThresholdBeforeFetch = 2;//trigger to fetch more videos when x amount of videos left
}
