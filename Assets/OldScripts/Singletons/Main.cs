using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{

    private static Main instance;
    public static Main Instance {
        get { return instance; }
    }

    public UIDocument mainUIDocument;

    private GameData gameData;
    private SessionController sessionController;

    public Camera[] danceCameras;

    private void Awake()
    {
        if(Main.Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameServices.Initialize();
            gameData = ServiceLocator.GetService<GameDataService>().LoadGame();
        }
    }


    public GameData GetGameData()
    {
        return gameData;
    }


    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    void Play()
    {
        sessionController = new SessionController(gameData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
