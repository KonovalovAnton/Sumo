using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{

	public FoodScript f;
	CircleCollider2D col;
	bool eaten;
    
	// Use this for initialization
	void Start ()
	{
		col = GetComponent<CircleCollider2D> ();
		float x = Random.Range (-col.radius * 0.4f, col.radius * 0.4f);
		float y = Random.Range (-col.radius * 0.4f, col.radius * 0.4f);
		Vector3 v = new Vector3 (x, y, 0);
        m = max;
	}
	
	// Update is called once per frame
	public float timer = 0;
	public float max;
    private float m;

	void Update ()
	{
		if (f.eaten) {
			timer += Time.deltaTime;
			if (timer > m) {
				spawn ();
				timer = 0;
				m = Random.Range (max, max*2);
			}
		}
	}

	void spawn ()
	{
		float x = Random.Range (-col.radius * 0.4f, col.radius * 0.4f);
		float y = Random.Range (-col.radius * 0.4f, col.radius * 0.4f);
		Vector3 v = new Vector3 (x, y, 0);
		f.transform.position = transform.position + v;
		f.eaten = false;
		f.xor ();
        
	}

    
}
