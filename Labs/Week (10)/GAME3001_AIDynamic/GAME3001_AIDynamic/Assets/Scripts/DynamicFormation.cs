using UnityEngine;
using System.Collections;

public class DynamicFormation : MonoBehaviour {		
	public float slowDist;
	public float stopDist;
	public float slowRot;
	private float rotLeft; // Rotation remaining to destination rotation.
	
	private float velocity = 0.0F; // Linear velocity.
	private float rotation = 0.0F; // Angular velocity.
	public float velocityMax;
	public float rotationMax;
	private float accelLinear = 0.0F;
	private float accelAngular = 0.0F;
	public float accelLinearInc;
	public float accelAngularInc;
	public float accelLinearMax;
	public float accelAngularMax;
		
	private bool hasTarget = false;
	
	public GameObject moveTarget; // New public target for actor!
	private Vector3 destVect;
	private Quaternion destRot;
	private float distTo;
	
	// Use this for initialization
	void Start () {
		hasTarget = true;
	}
	
	// Update is called once per frame
	void Update () {
		if ( hasTarget ) {
			// Set the destination vector and rotation.
			destVect = moveTarget.transform.position - transform.position;
			distTo = destVect.magnitude; // Vector3.Distance(transform.position, moveTarget) does the same thing.

			destRot = Quaternion.LookRotation( destVect );
			rotLeft = Quaternion.Angle(transform.rotation, destRot);
			
			transform.Translate( Vector3.forward * GetMoveSpeed() * Time.deltaTime );
			velocity = Mathf.Clamp(velocity + accelLinear, 0.0F, velocityMax);
			accelLinear = Mathf.Clamp(accelLinear + accelLinearInc, 0.0F, accelLinearMax);
					
			transform.rotation = Quaternion.RotateTowards( transform.rotation, destRot, GetRotSpeed() * Time.deltaTime );
			rotation = Mathf.Clamp((rotation + accelAngular), 0.0F, rotationMax);
			accelAngular = Mathf.Clamp((accelAngular + accelAngularInc), 0.0F, accelAngularMax);
									
			transform.eulerAngles = new Vector3( 0.0F, transform.eulerAngles.y, 0.0F ); // Ensure no rotation on X-Z axes.
			
			if (distTo < stopDist) {
				//hasTarget = false;
				velocity = 0.0F;
				accelLinear = 0.0F;
				accelAngular = 0.0F;
			}
		}
	}
	
	float GetMoveSpeed()
	{
		return (distTo >= slowDist?velocity:Mathf.Lerp(0.0F, velocity, distTo/slowDist));
	}
	
	float GetRotSpeed()
	{
		return (rotLeft >= slowRot?rotation:Mathf.Lerp(0.0F, rotation, rotLeft/slowRot));
	}
}
