using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour
{
	SoundManager manager;

	// Use this for initialization
	void Start ()
	{
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
		startPos = transform.position;
		manager = GameObject.Find ("_SoundManager").GetComponent<SoundManager> ();
	}

	public float globalSpeed;
    public float rotSpeed;
	public string Name = "player ";
	public Vector3 input;
	public int Food = 0;
	Rigidbody2D rgb;
	Animator anim;
	Vector3 startPos;
	public bool isAlive = true;
	public float r, g, b;
	public Sprite head;
	private int score;
    float rscale = 0.5f;
    float rspeed = 200f;
    float rrotSpeed = 12f;

    public int ID;

	void Update ()
	{

		if (isAlive) {
			if (jump && !stunned) {
				jumpIterable ();
				rgb.AddForce (transform.up * 100);
			} else if (!stunned) {
				Move ();
				Anim ();
			}
		} else
        {

        }
	}

	private void UpdateMass ()
	{
		GetComponent<Rigidbody2D> ().mass = 2 * Mathf.Pow (1.5f, Food);
	}

	public float getRange (float centreX, float centreY)
	{
		return Mathf.Sqrt ((centreX - transform.position.x) * (centreX - transform.position.x) +
			(centreY - transform.position.y) * (centreY - transform.position.y));
	}

	void Move ()
	{
		float x = input.x;
		float y = input.y;
		float destination = Mathf.Rad2Deg * Mathf.Atan2 (y, x) - 90;

		if (Mathf.Abs (x) > 0 || Mathf.Abs (y) > 0) {
            float angle = Mathf.LerpAngle(transform.localEulerAngles.z, destination, Time.deltaTime * rotSpeed);
			transform.localEulerAngles = new Vector3 (0, 0, angle);
			Vector3 mover = new Vector3 (Mathf.Cos ((destination + 90) * Mathf.Deg2Rad), Mathf.Sin ((destination + 90) * Mathf.Deg2Rad), 0);
			rgb.AddForce (mover * globalSpeed);
		}
	}

	bool stay = false;

	void Anim ()
	{
		if (Mathf.Abs (rgb.velocity.x) < 2 && Mathf.Abs (rgb.velocity.y) < 2 && !push) {
			anim.SetTrigger ("STAY");
			stay = true;
		} else {
			anim.SetTrigger("RUN");
			stay = false;
		}
	}

	bool push = false;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.CompareTag ("Player") && !stay) {
			push = true;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		push = false;
	}

	public void Kicked (Vector3 dir)
	{
		manager.PlayAttacked ();
		rgb.AddForce (dir);
	}

	bool stunned;

	public void StunIt ()
	{
        anim.SetTrigger("STUN");
		manager.PlayStunned ();
		stunned = true;
		GetComponentInChildren<Stun> ().setHit ();
	}

	public void Active ()
	{
		stunned = false;
	}

	float t1;
	bool jump;

	public void Jump (int id)
	{
        ID = id;
		jump = true;
		anim.SetTrigger("JUMP");
		Collider2D[] a = GetComponents<Collider2D> ();
		foreach (Collider2D col in a) {
			col.enabled = false;
		}
		SpriteRenderer[] b = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer p in b) {
			p.sortingOrder += 20*(ID+1);
		}
	}

	void jumpIterable ()
	{
		t1 += Time.deltaTime;
		if (t1 > 1) {
			t1 = 0;			
			Collider2D[] a = GetComponents<Collider2D> ();
			foreach (Collider2D col in a) {
				col.enabled = true;
			}
			SpriteRenderer[] b = GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer p in b) {
                p.sortingOrder -= 20 * (ID + 1);
			}
            jump = false;
		}
	}

	public void AteFood ()
	{
        manager.PlayEat();
        if (Food < 5)
        {
            Food++;
            globalSpeed *= 0.9f;
            rotSpeed *= 0.65f;
            transform.localScale *= 1.1f;
        }
	}

	public void reset ()
	{
		transform.position = startPos;
		Food = 0;
        rotSpeed = rrotSpeed;
        transform.localScale = new Vector3(rscale,rscale,rscale);
        globalSpeed = rspeed;
		isAlive = true;
        if (jump)
        {
            jump = false;
            Collider2D[] a = GetComponents<Collider2D>();
            foreach (Collider2D col in a)
            {
                col.enabled = true;
            }
            SpriteRenderer[] b = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer p in b)
            {
                p.sortingOrder -= 20 * (ID + 1);
            }
        }
	}

    public void SetSpawnPosition(Vector2 pos)
    {
        transform.position = pos;
        startPos = pos;
    }

   
}
