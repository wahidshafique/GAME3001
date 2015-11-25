using UnityEngine;
using System.Collections;

public class ArriveClickScript : MonoBehaviour {
	public Camera sourceCam;
	public GameObject targetObj;
		
	public float moveSpeed;
	public float rotSpeed;
	public float slowDist;
	public float stopDist;
	
	private bool hasTarget = false;
	
	private Vector3 moveTarget;
	private Vector3 destVect;
	private Quaternion destRot;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0) ) { // If left mouse button is clicked.
			Ray ray = sourceCam.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if( Physics.Raycast( ray, out hit ) ) {
				moveTarget = hit.point;
				targetObj.transform.position = moveTarget;
				hasTarget = true;
			}
		}
		
		if ( hasTarget ) {
			// Set the destination vector and rotation.
			destVect = moveTarget - transform.position;
			destRot = Quaternion.LookRotation( destVect );
			
			float distTo = Vector3.Distance(transform.position,moveTarget);
			
			transform.rotation = Quaternion.RotateTowards( transform.rotation, destRot, rotSpeed * Time.deltaTime );
			transform.eulerAngles = new Vector3( 0.0F, transform.eulerAngles.y, 0.0F ); // Ensure no rotation on X-Z axes.
			
			transform.Translate( Vector3.forward * GetMoveSpeed(distTo) * Time.deltaTime );
			if (distTo < stopDist) {
				hasTarget = false;
			}	
		}
	}
	
	float GetMoveSpeed(float dist)
	{
		return (dist >= slowDist?moveSpeed:Mathf.Lerp(0.0F, moveSpeed, dist/slowDist));
	}
}
