using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class EqPanels : MonoBehaviour {

	public Material noGlowMaterial;
	public Material glowMaterial;
	public Material boostMaterial;
	public Material panelMaterial;
	public GameObject viewer;
	public AudioMixer mixer;

	static float radius = 9.0f;
	static float height = 30.0f;

	const int NUM_PANELS = 8;
	const int BARS_PER_PANEL = 3;

	GameObject[] panels;
	float[] gains;

	// Create panels
	void Start () {
		panels = new GameObject[NUM_PANELS];
		gains = new float[NUM_PANELS];
		for (int i = 0; i < gains.Length; ++i) {
			gains[i] = 1.0f;
		}

		// Trigger when magnet is pulled
		MagnetSensor.OnCardboardTrigger += Trigger;

		float panelWidth = 2 * radius * Mathf.Tan(Mathf.PI / (2 * NUM_PANELS));
		const float offsetAng = -Mathf.PI / NUM_PANELS;
		const float startAng = Mathf.PI + offsetAng / 2;

		GameObject panel;
		Vector3 rot;
		float thisAng;
		for (int i = 0; i < NUM_PANELS; i++) {
			panel = GameObject.CreatePrimitive(PrimitiveType.Cube);
			panel.name = "Panel" + i;
			panel.GetComponent<MeshRenderer>().material = panelMaterial;

			// set position
			thisAng = startAng + offsetAng * i;
			panel.transform.position = new Vector3(
				radius * Mathf.Cos(thisAng),
				height * 0.5f,
				radius * Mathf.Sin(thisAng)
			);

			// face towards player
			rot = panel.transform.position;
			rot.y = 0;
			panel.transform.rotation = Quaternion.FromToRotation(Vector3.forward, rot);

			panel.transform.localScale = new Vector3(panelWidth, height * 2, 0.1f);

			panels[i] = panel;
		}
	}

	void Update () {
		Vector3 viewPos = viewer.transform.position;
		Vector3 viewRot = viewer.transform.rotation * Vector3.forward;

		RaycastHit hit;
		if (Physics.Raycast(viewPos, viewRot, out hit)) {
			// Ray hit a panel
			GameObject hitPanel = hit.collider.transform.gameObject;

			// Color appropriate bars
			for (int i = 0; i < panels.Length; ++i) {
				if (panels[i] == hitPanel) {
					SetBarsMaterial(i, glowMaterial);
				} else {
					if (gains [i] > 1.5) {
						SetBarsMaterial (i, boostMaterial);
					} else {
						SetBarsMaterial (i, noGlowMaterial);
					}
				}
			}
		} else {
			for (int i = 0; i < panels.Length; ++i) {
				if (gains [i] > 1.5) {
					SetBarsMaterial (i, boostMaterial);
				} else {
					SetBarsMaterial (i, noGlowMaterial);
				}
			}
		}

		// Also trigger when mouse is pressed
		if (Input.GetMouseButtonDown(0)) {
			Trigger();
		}

		for (int band = 0; band < gains.Length; band++) {
			mixer.SetFloat("gain" + band, gains[band]);
		}
	}

	void Trigger() {
		Debug.Log("T R I G G E R E D");

		Vector3 viewPos = viewer.transform.position;
		Vector3 viewRot = viewer.transform.rotation * Vector3.forward;

		RaycastHit hit;
		if (Physics.Raycast(viewPos, viewRot, out hit)) {
			// Ray hit a panel
			GameObject hitPanel = hit.collider.transform.gameObject;

			// Color appropriate bars
			for (int i = 0; i < panels.Length; ++i) {
				if (panels[i] == hitPanel) {
					gains[i] = -(gains[i] - 3.0f); // swap between 1.0 and 2.0
				}
			}
		}
	}

	// Center freqs:
	//		30.0f,
	//		90.0f,
	//		250.0f,
	//		600.0f,
	//		1400.0f,
	//		4500.0f,
	//		12000.0f,
	//		20000.0f

	void SetBarsMaterial(int sector, Material mat) {
		GameObject[] bars = GetComponent<BarController>().bars;
		GameObject bar;
		for (int i = 0; i < BARS_PER_PANEL; ++i) {
			bar = bars[sector * BARS_PER_PANEL + i];
			bar.GetComponent<MeshRenderer>().material = mat;
		}
	}
}
