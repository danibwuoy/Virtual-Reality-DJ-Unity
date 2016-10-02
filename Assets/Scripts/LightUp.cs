using UnityEngine;
using System.Collections;

public class LightUp : MonoBehaviour,  IGvrGazeResponder {

	public Material def;
	public Material hover;
	public Material select;
	private bool isSelected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see GvrGaze).
	void IGvrGazeResponder.OnGazeEnter() {
		GetComponent<MeshRenderer> ().material = hover;
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	void IGvrGazeResponder.OnGazeExit() {
		if (!isSelected) {
			GetComponent<MeshRenderer> ().material = def;
		} else {
			GetComponent<MeshRenderer> ().material = select;
		}
	}

	/// Called when the trigger is used, between OnGazeEnter and OnGazeExit.
	void IGvrGazeResponder.OnGazeTrigger() {
		if (isSelected) {
			GetComponent<MeshRenderer> ().material = def;
			isSelected = false;
		} else {
			GetComponent<MeshRenderer> ().material = select;
			isSelected = true;
		}
	}
}
