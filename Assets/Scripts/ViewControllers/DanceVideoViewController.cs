using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceVideoViewController : ViewController
{
    [SerializeField] Animator animator;
    public override void Link()
    {
        base.Link();
        animator.enabled = false;
    }

    public void Animate(bool shouldAnimate)
    {
        //enable animation here
        animator.enabled = shouldAnimate;
        
    }
}
