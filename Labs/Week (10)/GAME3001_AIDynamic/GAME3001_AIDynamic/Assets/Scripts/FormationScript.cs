using UnityEngine;
using System.Collections;

public class FormationScript : MonoBehaviour {
	public GameObject[] magicSlots;
	public GameObject[] meleeSlots;
	public GameObject[] missileSlots;
	
	public GameObject[] archers;
	public GameObject[] elves;
	public GameObject[] mages;
	public GameObject[] fighters;
	
	private 
	// Use this for initialization
	void Start () {
		magicSlots = GameObject.FindGameObjectsWithTag("Magic");
		meleeSlots = GameObject.FindGameObjectsWithTag("Melee");
		missileSlots = GameObject.FindGameObjectsWithTag("Missile");
		
		archers = GameObject.FindGameObjectsWithTag("Archer");
		elves = GameObject.FindGameObjectsWithTag("Elf");
		mages = GameObject.FindGameObjectsWithTag("Mage");
		fighters = GameObject.FindGameObjectsWithTag("Fighter");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
