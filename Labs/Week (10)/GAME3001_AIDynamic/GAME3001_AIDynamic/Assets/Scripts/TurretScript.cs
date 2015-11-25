using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {
	public float rangeToPlayer;
	public Rigidbody bullet;
	private GameObject player;
	private bool firing = false;
	private float fireTime;
	private float coolDown = 0.15F;
	
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	void Update () {
		if ( PlayerInRange() ) {
			transform.LookAt(player.transform.position);
			RaycastHit hit;
			if( Physics.SphereCast(transform.position, 0.5F, transform.TransformDirection(Vector3.forward), out hit )) {
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20.0F, Color.red);
				if (hit.transform.gameObject.tag == "Player") {
					if ( firing == false )
					{
						firing = true;
						fireTime = Time.time;
						Rigidbody bul;
						Quaternion leadRot = Quaternion.LookRotation(player.transform.position + transform.TransformDirection(Vector3.right) * 2.0F);
						bul = Instantiate(bullet, transform.position, leadRot) as Rigidbody;
					}
				}
			}
		}
		if ( firing && fireTime + coolDown <= Time.time )
			firing = false;
	}
	
	bool PlayerInRange() {
		return ( Vector3.Distance(player.transform.position, transform.position) <= rangeToPlayer );
	}
}
