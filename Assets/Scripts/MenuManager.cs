using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset slideTemplate;

    UIDocument menuUIDocument;


    [SerializeField] Camera slideCamera1;
    [SerializeField] Camera slideCamera2;

    RenderTexture slideCamera1RenderTexture;
    RenderTexture slideCamera2RenderTexture;

    SlideListController slideListController;

    private void OnEnable()
    {
        menuUIDocument = GetComponent<UIDocument>();
        var rootVisualElement = menuUIDocument.rootVisualElement;
        slideListController = new SlideListController(new GameData());
        slideListController.InitializeSlideList(rootVisualElement);

        slideCamera1RenderTexture = new RenderTexture(Screen.width, Screen.height, 32);
        slideCamera2RenderTexture = new RenderTexture(Screen.width, Screen.height, 32);
        slideCamera1.targetTexture = slideCamera1RenderTexture;
        slideCamera2.targetTexture = slideCamera2RenderTexture;
    }

    private void Update()
    {
        
        //menuUIDocument.rootVisualElement.style.backgroundImage = slideCamera1.targetTexture.toTexture2D();// toTexture2D(slideCamera1.targetTexture);    
        slideListController.SetTexture(0, slideCamera1.targetTexture.toTexture2D());
        slideListController.SetTexture(1, slideCamera2.targetTexture.toTexture2D());
    }

   
}
