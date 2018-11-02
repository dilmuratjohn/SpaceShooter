using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed, tilt, fireRate;
    public GameObject shot;
    public Transform shotSpawn;
    public Boundary boundary;

    private float nextFire;
    private new AudioSource audio;
    private new Rigidbody rigidbody;

    private void Start()
    {
        this.audio = GetComponent<AudioSource>();
        this.rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            this.audio.Play();
        }
    }
    
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        this.rigidbody.velocity = movement * speed;
        this.rigidbody.position = new Vector3(
            Mathf.Clamp(this.rigidbody.position.x, boundary.xMin, boundary.xMax),
            0,
            Mathf.Clamp(this.rigidbody.position.z, boundary.zMin, boundary.zMax)
        );
        this.rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, this.rigidbody.velocity.x * -tilt);
    }
}
