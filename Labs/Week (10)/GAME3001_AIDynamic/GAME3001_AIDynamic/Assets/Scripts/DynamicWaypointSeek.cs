using UnityEngine;
using System.Collections;

public class DynamicWaypointSeek : MonoBehaviour {
	public Camera sourceCam;
	public GameObject targetObj;
		
	public float slowDistPerc; 
	private float slowDist;
	public float stopDist;
	public float slowRotPerc; 
	private float slowRot;
	private float rotLeft; 
	
	private float velocity = 0.0F; 
	private float rotation = 0.0F; 
	public float velocityMax;
	public float rotationMax;
	private float accelLinear;
	private float accelAngular;
	public float accelLinearInc;
	public float accelAngularInc;
	public float accelLinearMax;
	public float accelAngularMax;
		
	private bool hasTarget = false;
	
	private Vector3 moveTarget;
	private Vector3 destVect;
	private Quaternion destRot;
	private float distTo;
	
	private int wpIndex = 0;
	public GameObject[] waypoints;
	public bool stopAtLastWP;
	
	void Start () {
		moveTarget = waypoints[ wpIndex ].transform.position;
		targetObj.transform.position = moveTarget;
		hasTarget = true;
		StartMoving();
	}
	
	void Update () {		
		if ( hasTarget ) {
			// Set the destination vector and rotation.
			destVect = moveTarget - transform.position;
			distTo = destVect.magnitude; 

			destRot = Quaternion.LookRotation( destVect );
			rotLeft = Quaternion.Angle(transform.rotation, destRot);
			
			transform.Translate( Vector3.forward * GetMoveSpeed() * Time.deltaTime );
			velocity = Mathf.Clamp(velocity + accelLinear, 0.0F, velocityMax);
			accelLinear = Mathf.Clamp(accelLinear + accelLinearInc, 0.0F, accelLinearMax);
					
			transform.rotation = Quaternion.RotateTowards( transform.rotation, destRot, GetRotSpeed() * Time.deltaTime );
			rotation = Mathf.Clamp((rotation + accelAngular), 0.0F, rotationMax);
			accelAngular = Mathf.Clamp((accelAngular + accelAngularInc), 0.0F, accelAngularMax);
									
			transform.eulerAngles = new Vector3( 0.0F, transform.eulerAngles.y, 0.0F ); 
			
			if (distTo < stopDist && stopAtLastWP && wpIndex == waypoints.Length-1) {
				hasTarget = false;
				velocity = 0.0F;
			}
			else if (distTo < stopDist) {
				wpIndex++;
				if ( wpIndex == waypoints.Length )
					wpIndex = 0;
				moveTarget = waypoints[ wpIndex ].transform.position;
				targetObj.transform.position = moveTarget;
				StartMoving();
			}	
		}
	}
	
	void StartMoving()
	{
		accelLinear = 0.0F;
		accelAngular = 0.0F;
		rotation = 0.0F;
		
		destVect = moveTarget - transform.position;
		distTo = destVect.magnitude; 
		slowDist = distTo * slowDistPerc;
		
		destRot = Quaternion.LookRotation( destVect );
		rotLeft = Quaternion.Angle(transform.rotation, destRot);
		slowRot = rotLeft * slowRotPerc;
	}
	
	float GetMoveSpeed()
	{
		return ((stopAtLastWP && distTo >= slowDist)? velocity:Mathf.Lerp(0.0F, velocity, distTo/slowDist));
	}
	
	float GetRotSpeed()
	{
		return (rotLeft >= slowRot?rotation:Mathf.Lerp(0.0F, rotation, rotLeft/slowRot));
	}
}
