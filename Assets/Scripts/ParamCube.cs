using UnityEngine;
using System.Collections;

public class ParamCube : MonoBehaviour {
	public int _band;
	public float _startScale, _scaleMultiplier;
	public float _speed;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		Vector3 newScale = transform.localScale;
		float newY = (AudioPeer8._freqBand[_band] * _scaleMultiplier) + _startScale;
		newScale.y = Mathf.Lerp(newScale.y, newY, _speed * Time.deltaTime);
		transform.localScale = newScale;
	}
}
