using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour { //controls the projectile ejected from Turret Emitter
    public GameObject explosionPrefab;//object to be instantiated when the projectile is destroyed
    private float speed = 10f;//this is the constant speed in which the projectile will travel at


    void Update() {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        tag = coll.gameObject.tag;
        if (tag == "PlayerPlat" || tag == "Limit") {
            Destroy(this.gameObject);//destroy projectile
        }

    }
}