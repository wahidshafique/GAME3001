using UnityEngine;
using System.Collections;

public class ClickScript : MonoBehaviour {

	void OnMouseDown ()
	{
		print("Clicked at: "+Input.mousePosition);
	}
}
