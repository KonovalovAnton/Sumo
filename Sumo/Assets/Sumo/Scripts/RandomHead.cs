using UnityEngine;
using System.Collections;

public class RandomHead : MonoBehaviour
{
    
	int type;
	public Sprite s0, s1, s2, s3, s4, s5;

	// Use this for initialization
	void Awake ()
	{
		Sprite s = s0;

		type = Random.Range (0, 6);
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
		case 4:
			s = s4;
			break;
		case 5:
			s = s5;
			break;
		}
		GetComponent<SpriteRenderer> ().sprite = s;
		GetComponentInParent<Walk> ().head = s;
	}

}
