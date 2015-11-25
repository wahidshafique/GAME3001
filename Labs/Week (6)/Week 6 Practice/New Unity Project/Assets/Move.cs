
using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public Transform track;
    private float moveSpeed=10;
	
	// Update is called once per frame
	void Update () {
        float move = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, track.position, move);
	}
}




