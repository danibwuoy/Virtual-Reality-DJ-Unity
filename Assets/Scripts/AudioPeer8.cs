using UnityEngine;
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AudioPeer8 : MonoBehaviour {
	public interface SpikedAudioListener {
		void onAudioSpike(float[] freqBands);
	}
	public static int _numBands = 24;

	public static float[] _samples = new float[2048];
	public static float[] _freqBand = new float[_numBands];
	private List<SpikedAudioListener> callbacks = new List<SpikedAudioListener>();
	private float[] window;
	private int windowFill = 0;

	private int windowSize = 20;

	// Speed at which the bars lerp to the next sampled value
	// const float SPEED = 20.0f;

	// Use this for initialization
	void Start () {
		window = new float[windowSize];

	}

	// Update is called once per frame
	void Update () {
		AudioListener.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
		MakeFrequencyBands();
	}

	public void addCallback(SpikedAudioListener l) {
		callbacks.Add (l);
	}

	void registerCallback(float[] freqBands) {
		for (int i=0; i<callbacks.Count; ++i) {
			callbacks[i].onAudioSpike(freqBands);
		}
	}

	void MakeFrequencyBands() {
		/* 8 band visualizer
		 * 22050 / 512 = 43 hz per sample
		 * 20 - 60 hz
		 * 60 - 250 hz
		 * 250 - 500 hz
		 * 500 - 2000 hz
		 * 2000 - 4000 hz
		 * 4000 - 6000 hz
		 * 6000 - 20000 hz
		 * 
		 * 0: 2 = 86 hz
		 * 1: 4 = 172 hz: 87-258hz
		 * 2: 8 = 344 hz: 259-602hz
		 * 3: 16 = 688 hz: 603-1290
		 * 4: 32 = 1376 hz: 1291-2666
		 * 5: 64 = 2752 hz: 2667-5418
		 * 6: 128 = 5504 hz: 5419-10922
		 * 7: 256 = 11008 hz: 10923-21930
		 * 510, so add 2 samples to last one
		 */ 
		int count = 0;
		for (int i = 0; i < _numBands; i++) {
			float average = 0;
			int sampleCount = (int) Mathf.Pow(2, i - 14) + 1;

			//if (i == _numBands - 1) {
			//	sampleCount += 2;
			//}
			for (int j = 0; j < sampleCount; j++) {
				average += _samples[count] * (count + 1);
				count++;
			}

			average /= sampleCount;

			_freqBand[i] = average;
		}
		float sum = 0;
		Array.ForEach(_freqBand, delegate(float i) {sum+=i;});
		float avgFreq =  sum/_freqBand.Length;
		if (windowFill+1 >= windowSize) {
			//Array.ForEach(window, delegate(float t) {Debug.Log("W:" + t);});
			// test if spiked
			sum = 0;
			Array.ForEach(window, delegate(float i) {sum+=i;});
			float windowAvg = sum / window.Length;

			if (avgFreq > windowAvg) {
				//Debug.Log("AF:" + avgFreq);
				//Debug.Log("WA:" + windowAvg);
				registerCallback(_freqBand);
			}
			// update window
			window[(windowFill++) % windowSize] = avgFreq;
		} else {
			window[windowFill] = avgFreq;
			windowFill++;
		}
	}
}
