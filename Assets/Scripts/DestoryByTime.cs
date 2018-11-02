using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByTime : MonoBehaviour
{
    public float liftTime;
    private void Start()
    {
        Destroy(gameObject, liftTime);
    }
}
