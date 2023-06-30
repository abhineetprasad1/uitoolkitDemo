using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPool:IService
{
	[SerializeField]
	private Dictionary<string, PooledObject> objectMap;

	[SerializeField]
	private static ObjectPool instance;

	[SerializeField]
	private Transform parent;

	int i = 0;


	private GameObject objectPool;

	public void OnInit()
	{
		objectPool = new GameObject("ObjectPool");
		Object.DontDestroyOnLoad(objectPool);
		objectMap = new Dictionary<string, PooledObject>();
		parent = objectPool.transform;
	}

	public void OnDestroy()
	{
		objectMap.Clear();
		Object.Destroy(objectPool);
	}

	public void RegisterObject(string prefabPath, string key, int numberOfInstances = 0)
	{
		if(!objectMap.ContainsKey(key))
		{
			if(numberOfInstances == 0)
			{
				objectMap.Add(key, new PooledObject(key, prefabPath, true, parent));
			}
			else
			{
				objectMap.Add(key, new PooledObject(key, prefabPath, true, numberOfInstances, parent));
			}
		}
	}

	public void UnregisterObject(string key)
	{
		if(!objectMap.ContainsKey(key))
		{
			objectMap.Remove(key);
		}
	}

	public void ReturnObject(GameObject go)
	{
		if(go != null && go.GetComponent<PooledObjectMonoExtension>() != null)
		{
			ReturnObject(go, go.GetComponent<PooledObjectMonoExtension>().GetKey());
		}
	}

	public GameObject GetObject(string key)
	{
		if (!objectMap.ContainsKey(key))
			RegisterObject(key, key, 1);

		PooledObject p = objectMap[key];
		return p.GetObjectInstance();
	}

	public void ReturnObject(GameObject go, string key)
	{
		go.transform.SetParent(parent);
		go.SetActive(false);
		objectMap[key].ReturnObjectInstance(go);
	}
}

public class GenericSerializable : MonoBehaviour
{
	public System.Object o;
}


