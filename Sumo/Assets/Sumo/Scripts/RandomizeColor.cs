using UnityEngine;
using System.Collections;

public class RandomizeColor : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Walk w = GetComponentInParent<Walk>();
		w.r = Random.value;
		w.g = Random.value;
		w.b = Random.value;
        GetComponent<SpriteRenderer>().color = new Color(w.r, w.g, w.b);
	}
}
