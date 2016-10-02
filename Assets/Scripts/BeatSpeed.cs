/*
 * Copyright (c) 2015 Allan Pichardo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System;

/*
 * Make your class implement the interface AudioProcessor.AudioCallbaks
 */
public class BeatSpeed : MonoBehaviour, AudioPeer8.SpikedAudioListener
{
	public GameObject thisObject;
    private ParticleSystem ps; 
	private float playbackSpeed = 1;

    void Start()
    {
        //Select the instance of AudioProcessor and pass a reference
        //to this object
        AudioPeer8 processor = FindObjectOfType<AudioPeer8>();
        processor.addCallback(this);
		ps = thisObject.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
    }

    
    void Update()
    {
        if(playbackSpeed > 1) {
			playbackSpeed -= (playbackSpeed - (float)Math.Sqrt((double)playbackSpeed)) / 10;
		}
		ps.playbackSpeed = playbackSpeed;
    }

    //this event will be called every time a beat is detected.
    //Change the threshold parameter in the inspector
    //to adjust the sensitivity
    public void onAudioSpike(float[] freqBands)
    {
        //Debug.Log("Beat!!!");
		playbackSpeed += 10;
		//Debug.Log(playbackSpeed);
    }

    //This event will be called every frame while music is playing
    public void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i], 0);
            //Debug.DrawLine(start, end);
        }
    }
}
