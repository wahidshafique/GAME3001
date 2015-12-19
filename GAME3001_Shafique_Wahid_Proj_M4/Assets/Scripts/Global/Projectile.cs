using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour { //controls the projectile ejected from Turret Emitter
    public GameObject explosionPrefab;//object to be instantiated when the projectile is destroyed
    private float speed = 10f;//this is the constant speed in which the projectile will travel at
    public int lifetime = 1;
    void Awake() {
        Destroy(this.gameObject, lifetime);
    }
    void Update() {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}