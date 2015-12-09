using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public bool manualLoad = false;
	
	void Update () {
		if (manualLoad)
			Application.LoadLevel(0);
	}

	public void StartGame () {
		Application.LoadLevel("Level");
	}
	
	public void MainMenu () {
		Application.LoadLevel("MainMenu");
	}
	
	public void Credits () {
		Application.LoadLevel("Credits");
	}
	
	public void HowTo () {
		Application.LoadLevel("HowTo");
	}
	
	public void Quit () {
		Application.Quit();
	}
}
