using System;
using System.Collections;
using UnityEngine;

public interface IMono : IService
{
   
     void AddUpdateListener(Action updateAction);
     void RemoveUpdateListener(Action updateAction);

     Coroutine AttachCoroutine(IEnumerator enumerator);
     void KillCoroutine(Coroutine coroutine);

}