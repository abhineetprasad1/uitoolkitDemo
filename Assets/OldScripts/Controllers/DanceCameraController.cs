using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceCameraController : IController
{
    private GameData gameData;

    Camera[] danceCameras;
    RenderTexture[] renderTextures;

    int[] cameraTargetSlideIndices;

    int currentActiveIndex = 0;

    int[] cameraLayers;
    int invisibleLayer;

    public DanceCameraController(GameData gameData) {
        this.gameData = gameData;
        danceCameras = Main.Instance.danceCameras;
        renderTextures = new RenderTexture[danceCameras.Length];
        cameraTargetSlideIndices = new int[danceCameras.Length];
        cameraLayers = new int[danceCameras.Length];

        for (int i=0; i < danceCameras.Length; i++)
        {
            renderTextures[i] = new RenderTexture(Screen.width, Screen.height, 32);
            danceCameras[i].targetTexture = renderTextures[i];
            cameraTargetSlideIndices[i] = i - 1;
            cameraLayers[i] = LayerMask.NameToLayer("SlideCamera" + (i + 1));

            if (cameraTargetSlideIndices[i] < 0)
                continue;

            var targetSlide = gameData.loadedSlides[cameraTargetSlideIndices[i]];
            if (gameData.slideDanceObjectMap.TryGetValue(targetSlide, out DanceVideoViewController dvc))
            {
                DGUtils.SetLayerRecursively(dvc.gameObject, cameraLayers[i]);
            }

        }
        invisibleLayer = LayerMask.NameToLayer("InvisibleLayer");


        
    }

    
    public void Execute()
    {
      
        if(gameData.activeItemIndex != currentActiveIndex)
        {
            for(int i =0; i< gameData.loadedSlides.Count; i++)
            {
                var targetSlide = gameData.loadedSlides[i];
                if (gameData.slideDanceObjectMap.TryGetValue(targetSlide, out DanceVideoViewController dvc))
                {
                    DGUtils.SetLayerRecursively(dvc.gameObject, invisibleLayer);
                }
            }

            //update cameraSlideIndices
            if(gameData.activeItemIndex > currentActiveIndex)
            {
                var lowestIndexValue = int.MaxValue;
                var lowestIndex = 0;
                //increment lowest index by 3
                for(int i =0; i < cameraTargetSlideIndices.Length; i++)
                {
                    if (cameraTargetSlideIndices[i] < lowestIndexValue)
                    {
                        lowestIndexValue = cameraTargetSlideIndices[i];
                        lowestIndex = i;
                    }
                }
                cameraTargetSlideIndices[lowestIndex] += 3;
            }
            else
            {
                //decrement highest index by 3
                var highestValue = -1;
                var highestIndex = 0;
                for (int i = 0; i < cameraTargetSlideIndices.Length; i++)
                {
                    if (cameraTargetSlideIndices[i] > highestValue)
                    {
                        highestValue = cameraTargetSlideIndices[i];
                        highestIndex = i;
                    }
                }
                cameraTargetSlideIndices[highestIndex] -= 3;
            }

            

            currentActiveIndex = gameData.activeItemIndex;
        }


        for (int i = 0; i < danceCameras.Length; i++)
        {
            var targetSlideIndex = cameraTargetSlideIndices[i];

            if (targetSlideIndex < 0 || targetSlideIndex >= gameData.loadedSlides.Count)
                continue;

            var targetSlide = gameData.loadedSlides[targetSlideIndex];
            if (gameData.slideDanceObjectMap.TryGetValue(targetSlide, out DanceVideoViewController dvc))
            {
                DGUtils.SetLayerRecursively(dvc.gameObject, cameraLayers[i]);
            }
            targetSlide.DanceTexture = renderTextures[i].toTexture2D();

        }
    }

    public void Destroy()
    {

    }
}
