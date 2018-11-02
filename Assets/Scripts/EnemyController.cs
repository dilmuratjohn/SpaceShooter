using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public float delay, tilt, fireRate, targetManeuver, dodge, smoothing;
    public Vector2 startWait, maneuverTime, maneuverWait;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    private new AudioSource audio;
    private new Rigidbody rigidbody;
    private float currentSpeed;

    private void Start()
    {
        this.audio = GetComponent<AudioSource>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.currentSpeed = this.rigidbody.velocity.z;
        InvokeRepeating("Fire", delay, fireRate);
        StartCoroutine(this.Envade());
    }

    private void FixedUpdate()
    {
        float maneuver = Mathf.MoveTowards(this.rigidbody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        this.rigidbody.velocity = new Vector3(maneuver, 0.0f, currentSpeed);
        this.rigidbody.position = new Vector3
        (
            Mathf.Clamp(this.rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(this.rigidbody.position.z, boundary.zMin, boundary.zMax)
        );
        this.rigidbody.rotation = Quaternion.Euler(0.0f, 180.0f, this.rigidbody.velocity.x * -tilt);
    }

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        this.audio.Play();
    }

    private IEnumerator Envade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }

    }

}
