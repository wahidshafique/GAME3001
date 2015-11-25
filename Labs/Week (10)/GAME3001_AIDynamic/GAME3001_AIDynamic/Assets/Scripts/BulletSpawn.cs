using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour {
	public float speed;

	void Start () {
		Destroy(gameObject, 3.0F);
		GetComponent<Rigidbody>().AddForce(transform.forward * speed);
	}
	
	void OnCollisionEnter(Collision c) {
		Destroy(gameObject);
	}
}
