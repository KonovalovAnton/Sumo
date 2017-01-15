using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour
{

	public bool hit = false;
	Walk owner;
	// Use this for initialization
	void Start ()
	{
		owner = GetComponentInParent<Walk> ();
	}

	public void setHit ()
	{
		hit = true;
		t = 0;
	}

	float t;
	// Update is called once per frame
	void Update ()
	{
		if (hit) {
			t += Time.deltaTime;
			if (t > 2) {
				hit = false;
				owner.Active ();
				t = 0;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			Walk p = col.GetComponent<Walk> ();
			p.StunIt ();
		}
	}
}
