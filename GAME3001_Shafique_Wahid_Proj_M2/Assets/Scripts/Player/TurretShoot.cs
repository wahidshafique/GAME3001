using UnityEngine;
using System.Collections;

public class TurretShoot : MonoBehaviour {
    //your primary projectile emitter
    public GameObject projectile;
    private float nextFire = 0f;//controls first instance of instantiated object
    public float fireRate = 0.5f; //intervals for projectile to emit

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel("MainMenu");
        if (Time.time > nextFire && Input.GetMouseButton(0)) {
            nextFire = Time.time + fireRate;
            Vector3 position = transform.position;
            Instantiate(projectile, position, transform.rotation);
        }
    }
}