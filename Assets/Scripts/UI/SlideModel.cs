using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.ComponentModel;

[System.Serializable]
public class SlideModel :INotifyPropertyChanged
{
    public string creatorName;
    public string creatorDanceName;
    public string creatorImageURL;
    public string songName;
    public string songImageURL;
    public string dancePrefabName;
    private Texture2D danceTexture;
    public Texture2D DanceTexture
    {
        get { return danceTexture; }
        set
        {
            danceTexture = value;
            OnPropertyChanged(nameof(DanceTexture));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
