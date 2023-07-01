using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour, IView 
{
	public virtual void Link()
	{
		gameObject.transform.SetParent(Main.Instance.gameObject.transform);
	}

	public virtual void Show()
	{
		
		gameObject.SetActive(true);
	}

	public virtual void Reset()
	{
		ServiceLocator.GetService<ObjectPool>().ReturnObject(gameObject);
		
	}
}
