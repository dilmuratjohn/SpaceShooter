using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed, tileSizeZ;
    private Vector3 startPosition;

    private void Start()
    {
        this.startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = this.startPosition + new Vector3(0, 0, Mathf.Repeat(Time.time * this.scrollSpeed, this.tileSizeZ));
    }
}
