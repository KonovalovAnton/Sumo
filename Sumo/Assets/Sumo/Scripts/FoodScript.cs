using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour
{
	public int type;
	public Sprite s0, s1, s2, s3;
	public bool eaten = false;
	// Use this for initialization
	void Start ()
	{
		newImage ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player") && !eaten) {
			col.GetComponent<Walk> ().AteFood ();
			eaten = true;
			xor ();
		}
	}

	public void xor ()
	{
		/*GetComponent<Collider2D> ().enabled = !eaten;
		GetComponent<SpriteRenderer> ().enabled = !eaten;*/
		if(eaten) {
			this.transform.position = new Vector3(-100000,0,0);
		}
		newImage ();
	}

	void newImage ()
	{
		Sprite s = s0;

		type = Random.Range (0, 4);
		switch (type) {
		case 0:
			s = s0;
			break;
		case 1:
			s = s1;
			break;
		case 2:
			s = s2;
			break;
		case 3:
			s = s3;
			break;
		}
		GetComponent<SpriteRenderer> ().sprite = s;
	}
}
