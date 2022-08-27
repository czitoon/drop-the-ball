using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public float bounceSpeed;
    public AudioSource bounceSound;

    private void OnCollisionEnter(Collision collision) {
//        Vector3 direction = collision.transform.position - transform.position;
//        float impact = Vector3.Project(collision.relativeVelocity, direction).magnitude;
        if (collision.impulse.magnitude >= bounceSpeed) {
            bounceSound.Play();
        }
    }
}
