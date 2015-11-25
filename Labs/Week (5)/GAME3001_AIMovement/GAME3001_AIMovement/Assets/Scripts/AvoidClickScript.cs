using UnityEngine;
using System.Collections;

public class AvoidClickScript : MonoBehaviour {
	public Camera sourceCam;
	public GameObject targetObj;
		
	public float moveSpeed;
	public float rotSpeed;
	public float slowDist;
	public float stopDist;
	
	private bool hasTarget = false;
	private bool hasObstacle = false;
	
	private Vector3 moveTarget;
	public Vector3 avoidTarget;
	private Vector3 destVect;
	private Vector3 avoidVect;
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
			if (hasObstacle)
			{
				avoidVect = transform.position - avoidTarget;
				float avoidDist = Vector3.Distance(transform.position, avoidTarget);
				destRot = Quaternion.LookRotation( destVect.normalized + avoidVect.normalized );
			}
			else
			{
				destRot = Quaternion.LookRotation( destVect );
			}
			float distTo = Vector3.Distance(transform.position, moveTarget);
			
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
	
	void OnTriggerEnter(Collider other) {
		print("In1!");
        if (other.gameObject.tag == "Obstacle")
		{
			print("In2!");
			hasObstacle = true;
			avoidTarget = other.transform.position;
		}
    }
	
	void OnTriggerExit(Collider other) {
        print("Out1!");
		if (other.gameObject.tag == "Obstacle")
		{
			print("Out2!");
			hasObstacle = false;
		}
    }
}
