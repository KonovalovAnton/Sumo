using UnityEngine;
using System.Collections;

public class Leg : MonoBehaviour
{

	bool hit = true;
	Walk owner;

	// Use this for initialization
	void Start ()
	{
		owner = GetComponentInParent<Walk> ();
	}

	public void Hit ()
	{
		hit = true;
		GetComponentInParent<Animator> ().SetTrigger ("KICK");
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (hit && col.CompareTag ("Player")) {
			Walk p = col.GetComponent<Walk> ();
			p.Kicked (transform.up * (3000 * (1 + owner.Food)));
			hit = false; 
		}
	}
}
