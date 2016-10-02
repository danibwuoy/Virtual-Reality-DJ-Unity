using UnityEngine;
using System.Collections;
using System;

public class ParticleSeaSimple : MonoBehaviour, AudioPeer8.SpikedAudioListener {

	public ParticleSystem particleSystem;
	private ParticleSystem.Particle[] particlesArray;

	public Gradient colorGradient;

	public int seaResolution = 100;
	public float spacing = 1.0f;
	public float noiseScale = 0.05f;
	public float heightScale = 4f;
	public float speedX = 0.0f;
	public float speedY = 0.0f;

	private float playbackSpeed = 1;
	private float perlinNoiseAnimX = 0.01f;
	private float perlinNoiseAnimY = 0.01f;

	void Start() {
        AudioPeer8 processor = FindObjectOfType<AudioPeer8>();
        processor.addCallback(this);
		particlesArray = new ParticleSystem.Particle[seaResolution * seaResolution];
		particleSystem.maxParticles = seaResolution * seaResolution;
		particleSystem.Emit(seaResolution * seaResolution);
		particleSystem.GetParticles(particlesArray);
	}

	void Update() {
        if(playbackSpeed > 1) {
			//Debug.Log((playbackSpeed - (float)Math.Sqrt((double)playbackSpeed)) / 10);
			playbackSpeed -= (playbackSpeed - (float)Math.Sqrt((double)playbackSpeed)) / 10;
		}
		//Debug.Log(playbackSpeed);
		perlinNoiseAnimX += speedX * playbackSpeed;
		perlinNoiseAnimY += speedY * playbackSpeed;

		for(int i = 0; i < seaResolution; i++) {
			for(int j = 0; j < seaResolution; j++) {
				float zPos = Mathf.PerlinNoise(i * noiseScale + perlinNoiseAnimX, j * noiseScale + perlinNoiseAnimY);
				particlesArray[i * seaResolution + j].color = colorGradient.Evaluate(zPos);
				particlesArray[i * seaResolution + j].position = new Vector3(i * spacing, zPos  * heightScale, j * spacing);
			}
		}

		particleSystem.SetParticles(particlesArray, particlesArray.Length);
	}
	
    public void onAudioSpike(float[] freqBands) {
		playbackSpeed += 10;
	}

}
