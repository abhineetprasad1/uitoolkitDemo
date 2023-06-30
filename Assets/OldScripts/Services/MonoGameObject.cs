using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoGameObject : MonoBehaviour
{
   
    private readonly List<Action> updatelisteners = new List<Action>();



    public void AddUpdateListener(Action updateAction)
    {
        List<Action> uList = updatelisteners;
        if (!uList.Contains(updateAction))
        {
            uList.Add(updateAction);
        }
    }

    public void RemoveUpdateListener(Action updateAction)
    {
        List<Action> uList = updatelisteners;
        if (uList.Contains(updateAction))
        {
            uList.Remove(updateAction);
        }
    }

    public Coroutine AttachCoroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }

    public void KillCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }

    private void Update()
    {
        if (updatelisteners.Count <= 0)
        {
            return;
        }
        List<Action> tempList = new List<Action>(updatelisteners);
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i]();
        }
    }

    public IEnumerator DelayedCall(float val, Action action)
    {
        yield return new WaitForSeconds(val);
        action();
    }
    
    public IEnumerator DelayedCallByFrame(float val, Action action)
    {
        for (var i = 0; i < val; i++) 
        {
            yield return new WaitForEndOfFrame(); 
        } 
        action();
    }
}
