using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] AnimationClip animationClip;

    void Start()
    {
        Destroy(gameObject, animationClip.length);
    }

}
