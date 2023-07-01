using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;
using System.Linq;

public class SlideListController:IController
{

    GameData gameData;

    VisualTreeAsset slideTemplate;

    VisualElement slideListContainer;
    ListView slideList;
    ScrollView scrollView;

    List<SlideModel> slides;

    private int currentIndex = 0;
    private int itemsViewSinceLastFetch = 0;
    public SlideListController(GameData gameData)
    {
        this.gameData = gameData;
        InitializeSlideList(Main.Instance.mainUIDocument.rootVisualElement);
    }

    int lastDataCount = 0;
  
    public void Execute()
    {
        if(slides.Count > lastDataCount)
        {
            lastDataCount = slides.Count;
            slideList.itemsSource = slides;
            slideList.Rebuild();
        }
    }

    public void Destroy()
    {

    }

    public void InitializeSlideList(VisualElement root)
    {

        
        slides = gameData.loadedSlides;
        //this.slideTemplate = slideTemplate;
        this.slideTemplate = Resources.Load<VisualTreeAsset>("SlideViewElement");

        slideListContainer = root.Q<VisualElement>(UIConstants.SlideListConstants.SLIDE_LIST_CONTAINER_NAME);
        slideList = root.Q<ListView>(UIConstants.SlideListConstants.SLIDE_LISTVIEW_NAME);

        scrollView = slideList.Q<ScrollView>();
        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden; //TODO: do this in USS
      

        PopulateSlideList();

        var swipeManipulator = new SwipeManipulator(slideList);
        swipeManipulator.OnTouchMove += HandleTouchMove;
        swipeManipulator.OnSwipe += HandleSwipe;
        swipeManipulator.OnSwipeCancel += HandleSwipeCancel;

    }

    void FetchSlideData()
    {
        gameData.fetcMoreData = true;
        /*
        var model1 = new SlideModel(); model1.creatorName = "Luffy"; model1.creatorDanceName = "Yohohoho";
        var model2 = new SlideModel(); model2.creatorName = "Nami"; model2.creatorDanceName = "Samba";
        var model3 = new SlideModel(); model3.creatorName = "Sanji"; model3.creatorDanceName = "Swing";
        var model4 = new SlideModel(); model4.creatorName = "Roronoa Zoro"; model4.creatorDanceName = "Tango";
        var model5 = new SlideModel(); model5.creatorName = "Usup"; model5.creatorDanceName = "Bhangra";

        slides = new List<SlideModel>() { model1, model2, model3, model4, model5 };
        */
    }

    void PopulateSlideList()
    {
        slideList.makeItem = () =>
        {
            var slideEntry =  slideTemplate.CloneTree();// slideTemplate.Instantiate();
          
            var slideController = new SlideController();
            slideEntry.userData = slideController;
            
            slideController.SetSlideVisualElement(slideEntry);

            return slideEntry;
        };

        slideList.bindItem = (item, index) =>
        {
            (item.userData as SlideController).PopulateSlideVisualElement(slides[index]);
        };

        slideList.fixedItemHeight =   0.9f * Screen.height;//scrollView.contentViewport.resolvedStyle.height; TODO: gets resolved after a few frames, fetch and set accrodingly , check out GeometryChangeEvent
        slideList.itemsSource = slides;
        
    }

   
    //TOUCH LISTENERS
    void HandleTouchMove(Vector3 moveDelta)
    {
        Debug.Log("finger moved by " + moveDelta);
        
      
        var slideDefaultPosY = GetTargetScrollPositionY(this.currentIndex);
        
        var moveK = gameData.touchSensitivity * (Mathf.Abs(moveDelta.y) / slideList.fixedItemHeight);
        var targetPosY = Mathf.Lerp(slideDefaultPosY, slideDefaultPosY - Mathf.Sign(moveDelta.y) * slideList.fixedItemHeight, moveK);
        var targetPos = new Vector2(scrollView.scrollOffset.x, targetPosY);
        scrollView.scrollOffset = targetPos;// Vector3.Lerp(scrollView.scrollOffset, targetPos, 6 * Time.deltaTime);
    }

    void HandleSwipe(SwipeDirection swipeDirection)
    {
        
        int nextIndex = swipeDirection == SwipeDirection.Up ? currentIndex + 1 : Mathf.Max(0, currentIndex - 1);
        SnapToSlideAtIndex(nextIndex);
        
    }

    void HandleSwipeCancel()
    {
        SnapToSlideAtIndex(currentIndex);
    }


    //SLIDE SCROLL ANIMATIONS


    void SnapToSlideAtIndex(int newSlideIndex)
    {
        this.currentIndex = newSlideIndex;
        

        Debug.Log("Current Index move to " + currentIndex);

        var targetPos = new Vector2(scrollView.scrollOffset.x, GetTargetScrollPositionY(currentIndex));
        var currentPos = scrollView.scrollOffset;
        DOTween.Clear();
        DOTween.To(() => currentPos, x => currentPos = x, targetPos, gameData.slideAnimationDuration).SetEase(Ease.OutCubic).OnUpdate(
            () => {
                scrollView.scrollOffset = currentPos;
            }
            );

        //TODO: check and update if list actually moved
        gameData.activeItemIndex = this.currentIndex;

        int videosRemaining = (slides.Count - 1) - currentIndex;
        if(videosRemaining < gameData.viewThresholdBeforeFetch)
        {
            FetchSlideData();
        }

    }

    float GetTargetScrollPositionY(int index)
    {
        float itemHeight =  slideList.fixedItemHeight;// scrollView.contentContainer.layout.height / scrollView.contentContainer.childCount;
        float viewHeight = scrollView.layout.height;
        float minScrollPos = 0f;
        float maxScrollPos = scrollView.contentContainer.layout.height - viewHeight;
        float targetScrollPositionY = Mathf.Clamp(index * itemHeight - (viewHeight - itemHeight) / 2f, minScrollPos, maxScrollPos);

        return targetScrollPositionY;
    }


   //Set Slide Video
    public void SetTexture(int index, Texture2D tex)
    {
        slides[index].DanceTexture = tex;
    }

}
