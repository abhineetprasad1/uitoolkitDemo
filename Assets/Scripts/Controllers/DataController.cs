using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : IController
{
    private GameData gameData;

    public DataController(GameData gameData)
    {
        this.gameData = gameData;
        PopulateSlideModelData(gameData.fetchDataCount);
    }

    int currentCount = 0;
    List<string> names = new List<string>(){"Luffy","Naruto" ,"Michelle", "Sanji", "Roronoa Zoro", "Robin", "Usoop"};
    List<string> danceNames = new List<string>() { "Chicken Dance", "Swing Dance", "Hip Hop", "Macarena", "Salsa", "Bhangra" };
    List<string> prefabNames = new List<string>() { "Luffy", "Naruto","Michelle", "Capsule", "Cube", "Sphere", "DoubleCube" };

    void PopulateSlideModelData(int count)
    {
        for(int i =0; i < count; i++)
        {
            var slideModel = new SlideModel();
            slideModel.creatorName = names[currentCount%names.Count];
            slideModel.creatorDanceName = danceNames[currentCount % danceNames.Count];
            slideModel.dancePrefabName = prefabNames[currentCount % prefabNames.Count] ;// "Prefab" + currentCount;
            currentCount++;
            gameData.loadedSlides.Add(slideModel);
        }
    }

    public void Execute()
    {
        if (gameData.fetcMoreData)
        {
            PopulateSlideModelData(gameData.fetchDataCount);
            gameData.fetcMoreData = false;
        }

    }

    public void Destroy()
    {

    }
}
