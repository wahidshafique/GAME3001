using UnityEngine;
using System.Collections;

public class TurretShoot : MonoBehaviour {
    //your primary projectile emitter
    public GameObject projectile;
    private float nextFire = 0f;//controls first instance of instantiated object
    private float fireRate = 0.5f; //intervals for projectile to emit

    void Update() {
        if (Time.time > nextFire && Input.GetMouseButtonDown(0)) {
            nextFire = Time.time + fireRate;
            Vector3 position = transform.position;
            Instantiate(projectile, position, transform.rotation);
        }
    }
}