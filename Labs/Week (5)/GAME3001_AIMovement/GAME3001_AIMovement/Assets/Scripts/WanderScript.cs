using UnityEngine;
using System.Collections;

public class WanderScript : MonoBehaviour {
	public Camera sourceCam;
	public GameObject targetObj;
		
	public float moveSpeed;
	public float rotSpeed;
	public float slowDist;
	public float stopDist;
	
	private bool hasTarget = true;
	
	public Transform moveTarget;
	public GameObject wanderObj;
	
	private Vector3 destVect;
	private Quaternion destRot;
	
	private int wanderCtr = 0;
	private int ctrMax = 120;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( hasTarget ) {
			if (wanderCtr == ctrMax)
			{
				wanderCtr = 0;
				float newRot = transform.eulerAngles.y + Random.Range(-45.0F, 45.0F);
				wanderObj.transform.eulerAngles = new Vector3( 0.0F, newRot, 0.0F );
			}
			// Set the destination vector and rotation.
			destVect = moveTarget.position - transform.position;
			destRot = Quaternion.LookRotation( destVect );
			
			transform.rotation = Quaternion.RotateTowards( transform.rotation, destRot, rotSpeed * Time.deltaTime );
			transform.eulerAngles = new Vector3( 0.0F, transform.eulerAngles.y, 0.0F ); // Ensure no rotation on X-Z axes.
			
			transform.Translate( Vector3.forward * moveSpeed * Time.deltaTime );
			wrapPosition();
			wanderCtr++;
		}
	}
	
	void wrapPosition()
	{
		if (transform.position.z > 25.0F) {
			transform.position = new Vector3(transform.position.x,transform.position.y,-25.0F);
		}
		else if (transform.position.z < -25.0F) {
			transform.position = new Vector3(transform.position.x,transform.position.y,25.0F);
		}
		if (transform.position.x > 25.0F) {
			transform.position = new Vector3(-25.0F,transform.position.y,transform.position.z);
		}
		else if (transform.position.x < -25.0F) {
			transform.position = new Vector3(25.0F,transform.position.y,transform.position.z);
		}
	}
}
