using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceVideoController : IController
{

    private GameData gameData;
    int invisibleLayer;
    public DanceVideoController(GameData gameData)
    {
        this.gameData = gameData;
        invisibleLayer = LayerMask.NameToLayer("InvisibleLayer");
    }

    public void Execute()
    {
        //populate dancevideos
        for(int i =0;i < gameData.loadedSlides.Count; i++)
        {
            var slide = gameData.loadedSlides[i];
            if (!gameData.slideDanceObjectMap.ContainsKey(slide))
            {
                //instantiate dance video , set it to invisibleLayer
                var key = slide.dancePrefabName;
                var danceGo = ServiceLocator.GetService<ObjectPool>().GetObject("Prefab/" + key);
                var dvc = danceGo.GetComponent<DanceVideoViewController>();
                gameData.slideDanceObjectMap[slide] = dvc;
                dvc.Link();
                dvc.Show();
                DGUtils.SetLayerRecursively(danceGo, invisibleLayer);
            }
            if(gameData.slideDanceObjectMap.TryGetValue(slide, out DanceVideoViewController cdvc))
            {
                if( i == gameData.activeItemIndex)
                {
                    cdvc.Animate(true);
                }
                else
                {
                    cdvc.Animate(false);
                }
            }
        }

    }

    public void Destroy()
    {

    }

    //TODO: move it to UTILS
   

}
