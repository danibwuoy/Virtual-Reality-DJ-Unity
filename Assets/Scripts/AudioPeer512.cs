using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer512 : MonoBehaviour {
	AudioSource _audioSource;
	public static float[] _samples = new float[512];

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		getSpectrumAudioSource ();
	}

	// get all the sample data into our array of floats
	void getSpectrumAudioSource() {
		_audioSource.GetSpectrumData (_samples, 0, FFTWindow.BlackmanHarris);
	}
}
