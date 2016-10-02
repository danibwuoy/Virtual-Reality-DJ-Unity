using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class BarController2 : MonoBehaviour {

	// Number of frequency samples to obtain. Must be a power of 2.
	const int NUM_SAMPLES = 512;
	// Speed at which the bars lerp to the next sampled value
	const float SPEED = 8.0f;
	// Lower bounds for y scale
	const float SCALE_MIN = 0.0f;

	public Columns columns;
	// Audio level sens; higher sens makes the visualized bars scale to greater values.
	public float sensitivity;

	// Bar objects
	GameObject[] bars;
	// Buffers for Update()
	float[] spectrum;

	void Start () {
		spectrum = new float[NUM_SAMPLES];

		bars = columns.createColumns(10.0f, 1.2f, "outer");

		Debug.Assert(bars.Length <= NUM_SAMPLES,
			"Audio sample rate too low to produce data for all bars.");
	}

	void Update () {
		GameObject bar;
		Vector3 newScale;
		float target_scaleY;

		for (int i = 0; i < bars.Length; ++i) {
			bar = bars[i];
			newScale = bar.transform.localScale;
			target_scaleY = (AudioPeer8._freqBand[i] * sensitivity) + SCALE_MIN;
			newScale.y = Mathf.Lerp(newScale.y, target_scaleY, SPEED * Time.deltaTime);
			bar.transform.localScale = newScale;
		}
	}

}
