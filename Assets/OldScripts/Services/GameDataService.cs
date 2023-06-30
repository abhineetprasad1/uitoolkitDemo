using System;
using UnityEngine;

public class GameDataService : IService
{
	
	
	public void OnInit()
	{
		Debug.Log("Initialization of Game Data Service");
		
	}

	public void OnDestroy()
	{
		
	}
	
	
	
	public GameData LoadGame()
	{
		var gameData = new GameData();
		
		return gameData;
	}
	

}
