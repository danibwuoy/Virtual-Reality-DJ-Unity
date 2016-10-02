using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class BarController : MonoBehaviour {

	// Speed at which the bars lerp to the next sampled value
	const float SPEED = 8.0f;
	// Lower bounds for y scale
	const float SCALE_MIN = 0.0f;

	public Columns columns;
	// Audio level sens; higher sens makes the visualized bars scale to greater values.
	public float sensitivity;

	// Bar objects
	public GameObject[] bars;

	void Start () {
		bars = columns.createColumns(10.0f, 1.2f, "outer");
	}

	void Update () {
		GameObject bar;
		Vector3 newScale;
		Vector3 newPos;
		float target_scaleY;

		for (int i = 0; i < bars.Length; ++i) {
			bar = bars[i];
			newScale = bar.transform.localScale;
			newPos = bar.transform.position;

			target_scaleY = (AudioPeer8._freqBand[i] * sensitivity) + SCALE_MIN;
			newScale.y = Mathf.Lerp(newScale.y, target_scaleY, SPEED * Time.deltaTime);
			bar.transform.localScale = newScale;

			newPos.y = newScale.y / 2;
			bar.transform.position = newPos;
		}
	}

}
