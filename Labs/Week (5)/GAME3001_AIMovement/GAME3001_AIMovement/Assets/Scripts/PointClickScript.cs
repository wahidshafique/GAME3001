using UnityEngine;
using System.Collections;

public class PointClickScript : MonoBehaviour {
	public bool followWaypoints = false;
	public bool isSeek = true;
	public bool kinematicArrive = false;
	public Camera sourceCam;
	public GameObject targetObj;
	public GameObject[] waypointArray;
	public float moveSpeed;
	public float rotSpeed;
	public float slowDist;
	public float stopDist;

	private bool hasTarget = false;
	private Color arriveColor = new Color( 1.0F, 0.0F, 0.0F, 1.0F);
	private Color seekColor = new Color( 1.0F, 0.5F, 0.0F, 1.0F);
	private int wpIndex = 0;
	private Vector3 moveTarget;
	private Vector3 destVect;
	private Quaternion destRot;
	
	void Start () {
		waypointArray = GameObject.FindGameObjectsWithTag("Waypoint");
		System.Array.Reverse(waypointArray);
		if ( followWaypoints ) {
			moveTarget = waypointArray[ wpIndex ].transform.position;
			targetObj.transform.position = moveTarget;
			hasTarget = true;
		}
	}
	
	void Update () {
		if ( !followWaypoints && Input.GetMouseButtonDown(0) ) { // If left mouse button is clicked.
			Ray ray = sourceCam.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if( Physics.Raycast( ray, out hit ) ) {
				Debug.DrawLine ( sourceCam.transform.position, hit.point);
				moveTarget = hit.point;
				targetObj.transform.position = moveTarget;
				hasTarget = true;
			}
		}
		if ( hasTarget ) {
			destVect = moveTarget - transform.position;
			destRot = Quaternion.LookRotation( destVect );
			float distTo = Vector3.Distance(transform.position,moveTarget);
			transform.rotation = Quaternion.RotateTowards( transform.rotation, destRot, rotSpeed * Time.deltaTime );
			transform.eulerAngles = new Vector3( 0.0F, transform.eulerAngles.y, 0.0F );
			transform.Translate( Vector3.forward * ( moveSpeed * ( isSeek ? 1.0F : Mathf.Clamp((distTo/slowDist),0.01F,1.0F) ) ) * Time.deltaTime );
			if ( distTo <= stopDist && !isSeek )
				hasTarget = false;
			else if ( distTo <= (slowDist/3) && followWaypoints ) {
				wpIndex++;
				if ( wpIndex == waypointArray.Length )
					wpIndex = 0;
				moveTarget = waypointArray[ wpIndex ].transform.position;
				targetObj.transform.position = moveTarget;
			}
		}
		if ( Input.GetKeyDown("space") ) {
			isSeek = !isSeek;
			if ( isSeek ) ChangeColor( seekColor );
			else ChangeColor( arriveColor );
		}
	}
	
	private void ChangeColor( Color colorIn ) {
		GetComponent<Renderer>().material.SetColor("_Color", colorIn );	
		foreach ( Transform tr in transform )
			tr.GetComponent<Renderer>().material.SetColor("_Color", colorIn );
	}
}
