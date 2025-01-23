using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AnimationUpdate(float speed)
    {
        anim.SetFloat("speed", speed);
    }
}
