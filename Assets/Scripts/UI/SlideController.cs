
using UnityEngine.UIElements;
using System.ComponentModel;

public class SlideController
{

    Label creatorNameLabel;
    Label creatorDanceLabel;
    Button likeButton;
    Button shareButton;
    Button danceItButton;
    VisualElement danceVideoContainer;
    SlideModel slideModel;

    public void SetSlideVisualElement(VisualElement visualElement)
    {
        
        creatorNameLabel = visualElement.Q<Label>(UIConstants.SlideConstants.CREATOR_LABEL_NAME);
        creatorDanceLabel = visualElement.Q<Label>(UIConstants.SlideConstants.CREATOR_DANCE_NAME);
        likeButton = visualElement.Q<Button>(UIConstants.SlideConstants.LIKE_BUTTON_NAME);
        shareButton = visualElement.Q<Button>(UIConstants.SlideConstants.SHARE_BUTTON_NAME);
        danceVideoContainer = visualElement.Q<VisualElement>(UIConstants.SlideConstants.DANCE_CONTAINER_NAME);
    }


    public void PopulateSlideVisualElement(SlideModel slideModel)
    {
        creatorNameLabel.text = slideModel.creatorName;
        creatorDanceLabel.text = slideModel.creatorDanceName;
        this.slideModel = slideModel;
        slideModel.PropertyChanged += SlidePropertyChanged;
    }

    void SlidePropertyChanged(object sender, PropertyChangedEventArgs eventArgs)
    {
       
        if(eventArgs.PropertyName == nameof(slideModel.DanceTexture))
        {
            danceVideoContainer.style.backgroundImage = slideModel.DanceTexture;
        }

       
    }

  

}
