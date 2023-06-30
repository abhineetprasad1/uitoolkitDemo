using System;
using System.Collections;
using UnityEngine;

public class MonoService : IMono
{
    private MonoGameObject monoGO;
    public void OnInit()
    {
        
        var go = new GameObject("MonoService");
        monoGO = go.AddComponent<MonoGameObject>();
        GameObject.DontDestroyOnLoad(go);
    }

    public void AddUpdateListener(Action updateAction)
    {
        monoGO.AddUpdateListener(updateAction);
    }

    public void RemoveUpdateListener(Action updateAction)
    {
        monoGO.RemoveUpdateListener(updateAction);
    }
    
    public Coroutine AttachCoroutine(IEnumerator enumerator)
    {
       return monoGO.AttachCoroutine(enumerator);
    }

    public void KillCoroutine(Coroutine coroutine)
    { 
        monoGO.KillCoroutine(coroutine);
    }
    
    public void DelayedCall(float val, Action action)
    {
        monoGO.AttachCoroutine(monoGO.DelayedCall(val, action));
    }
    
    public void DelayedCallByFrame(float val, Action action)
    {
        monoGO.AttachCoroutine(monoGO.DelayedCallByFrame(val, action));
    }

    public void OnDestroy()
    {
       
    }
}
