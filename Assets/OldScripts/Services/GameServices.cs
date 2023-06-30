using UnityEngine;

public static class GameServices  
{
	public static void Initialize()
	{	
		
		Debug.Log("Game Initialization Started");
		Debug.Log("App Name: " + Application.productName +  "   Bundle ID: " + Application.identifier + "   App Version: " + Application.version);
		
		Application.targetFrameRate = 60;
		Input.multiTouchEnabled = false;
		
		
		ServiceLocator.RegisterService(new MonoService());
		ServiceLocator.RegisterService(new ObjectPool());
		ServiceLocator.RegisterService(new GameDataService());

        ServiceLocator.Initialize();

    }
}
