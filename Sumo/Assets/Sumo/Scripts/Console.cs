using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class Console : MonoBehaviour
{

	SoundManager manager;
	public bool ready;
	ArrayList readyList = new ArrayList ();
	ArrayList iconColors = new ArrayList ();
    ArrayList playerHeads = new ArrayList();
	public GameObject UI;
	public GameObject[] ticks;
	public Walk sumoRef;
	public GameObject fader;
	ArrayList sumo = new ArrayList ();
	ArrayList cd = new ArrayList ();
	ArrayList cd1 = new ArrayList ();
	Image playerIcon;
	SpriteRenderer faderRenderer;
	CircleCollider2D gg;


	void Start ()
	{
        AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onDisconnect += OnDisconnect;
		gg = GetComponent<CircleCollider2D> ();
		//StartCoroutine (WaitForPlayers ());

		manager = GameObject.Find ("_SoundManager").GetComponent<SoundManager> ();
		faderRenderer = fader.GetComponent<SpriteRenderer> ();
	}


    ArrayList converter = new ArrayList();


    void OnConnect(int from)
    {
        if(sumo.Count < 8)
        {
            //AirConsole.instance.SetActivePlayers(8);
            converter.Add(from);
            Object t = Instantiate(sumoRef, GameObject.Find("Spawn" + count).transform.position, Quaternion.identity);
            t.name = "sumo" + from;
            sumo.Add(t);
            Walk w = ((Walk)sumo[count-1]);
            if (!ready)
            {
                GameObject.Find("P" + count).GetComponentInChildren<CanvasRenderer>().SetColor(new Color(w.r, w.g, w.b));
                GameObject.Find("P" + count).GetComponentInChildren<SpriteRenderer>().sprite = w.head;
            }
            cd.Add(0f);
            cd1.Add(0f);
            count++;
            readyList.Add(false);
            iconColors.Add(new Color(w.r, w.g, w.b));
            playerHeads.Add(w.head);
        }
    }

	int count = 1;

	/*IEnumerator WaitForPlayers ()
	{
        
		while (AirConsole.instance.devices.Count < 10) {
			if (count < AirConsole.instance.devices.Count) {
                GameObject instantiated = (GameObject)Instantiate(sumoRef, GameObject.Find("Spawn" + count).transform.position, Quaternion.identity);
				sumo.Add (instantiated);
                Walk w = ((Walk)sumo[count - 1]);
                if (!ready)
                {
                    GameObject.Find("P" + count).GetComponentInChildren<CanvasRenderer>().SetColor(new Color(w.r, w.g, w.b));
                    GameObject.Find("P" + count).GetComponentInChildren<SpriteRenderer>().sprite = w.head;
                }
				cd.Add (0f);
				cd1.Add (0f);
				count++;
				readyList.Add (false);
				iconColors.Add (new Color (w.r, w.g, w.b));
                playerHeads.Add(w.head);
			}
			
			yield return null;
		}

		Reset ();
	}*/

	float time = 0;
	bool reset = false;

	void Reset ()
	{
		reset = true;
		time = 0;

		ready = false;
		for (int i = 0; i < sumo.Count; i++) {
            ((Walk)sumo[i]).input = new Vector2(0, 0);
		}
		SetReadyFalse ();
		StartCoroutine (FadeOut (1.0f, 0.5f));
	}
	
	void OnMessage (int from, JToken data)
	{
        int id = converter.IndexOf(from);//AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);
		if (id < 8 && id >= 0 && !ready) {
            AirConsole.instance.SetActivePlayers(8);
            print("Player Number: " + AirConsole.instance.ConvertDeviceIdToPlayerNumber(from));
			if (data ["A"] != null && (bool)data ["A"] ["pressed"] == true) {
				ticks [id].SetActive (true);
				readyList [id] = true;

				if (CheckReady ()) {
					ready = true;
					UI.SetActive (false);
					StartCoroutine (FadeIn (0.0f, 1.0f));
					manager.PlayGong ();
				}
			}
		}
        if (id < 8 && id >= 0 && ready)
        {
            
			if (data ["joystick-left"] != null && (bool)data ["joystick-left"] ["pressed"] == true) {
                ((Walk)sumo[id]).input = new Vector3((float)data["joystick-left"]["message"]["x"], -(float)data["joystick-left"]["message"]["y"], 0);
			} else {
                ((Walk)sumo[id]).input = new Vector3(0, 0, 0);
			}

			if (data ["A"] != null && (bool)data ["A"] ["pressed"] == true && ((float)cd [id]) > 1) {
                if (((Walk)sumo[id]).isAlive) ((Walk)sumo[id]).GetComponentInChildren<Leg>().Hit();
				cd [id] = 0f;
			} else if (data ["B"] != null && (bool)data ["B"] ["pressed"] == true && ((float)cd1 [id]) > 2) {
                if (((Walk)sumo[id]).isAlive) ((Walk)sumo[id]).Jump(id);
				cd1 [id] = 0f;
			}
		}
	}

	IEnumerator FadeIn (float aValue, float aTime)
	{
		float alpha = faderRenderer.material.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
			Color newColor = new Color (1, 1, 1, Mathf.Lerp (alpha, aValue, t));
			faderRenderer.material.color = newColor;
			yield return null;
		}
	}

	IEnumerator FadeOut (float aValue, float aTime)
	{
        yield return new WaitForSeconds(1f);

		float alpha = faderRenderer.material.color.a;
		
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
			Color newColor = new Color (1, 1, 1, Mathf.Lerp (alpha, aValue, t));
			faderRenderer.material.color = newColor;
			yield return null;
		}

        UI.SetActive(true);
        for (int i = 0; i < sumo.Count; i++)
        {
            GameObject.Find("P" + (i + 1)).GetComponentInChildren<CanvasRenderer>().SetColor((Color)iconColors[i]);
            GameObject.Find("P" + (i + 1)).GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)playerHeads[i];
            ((Walk)sumo[i]).SetSpawnPosition(GameObject.Find("Spawn" + (i + 1)).transform.position);
        }
        for (int i = sumo.Count; i < 8; i++)
        {
            GameObject.Find("P" + (i + 1)).GetComponentInChildren<CanvasRenderer>().SetColor(Color.white);
            GameObject.Find("P" + (i + 1)).GetComponentInChildren<SpriteRenderer>().sprite = null;
        }
		
	}
	
	bool CheckReady ()
	{
        for (int i = 0; i < readyList.Count; i++)
        {
            if ((bool)readyList[i] == false)
            {
                return false;
            }
        }

        return true;
	}

	void SetReadyFalse ()
	{
		for (int i = 0; i < 8; i++) {
            if (i < readyList.Count)
            {
                readyList[i] = false;
            }
			ticks [i].SetActive (false);
		}

	}

	int resetCount;

	void Update ()
	{
		if (ready) {

			resetCount = 0;
			for (int i = 0; i < sumo.Count; i++) {
                if (((Walk)sumo[i]).getRange(gg.bounds.center.x, gg.bounds.center.y) > gg.radius)
                {
                    ((Walk)sumo[i]).isAlive = false;
					resetCount++;
				}
			}
			if (resetCount == sumo.Count - 1) {
				Reset ();
			}
		}
	}
	
	void FixedUpdate ()
	{
		if (!reset) {
			for (int i = 0; i < cd.Count; i++) {
				cd [i] = (float)cd [i] + Time.fixedDeltaTime;
				cd1 [i] = (float)cd1 [i] + Time.fixedDeltaTime;
			}
		} else {
			time += Time.fixedDeltaTime;
			if (time > 1.3f) {
				for (int i = 0; i < sumo.Count; i++) {
                    ((Walk)sumo[i]).reset();
				}
				reset = false;
			}
		}
	}

	void OnDestroy ()
	{

		// unregister airconsole events on scene change
		if (AirConsole.instance != null) {
			AirConsole.instance.onMessage -= OnMessage;
            AirConsole.instance.onDisconnect -= OnDisconnect;
		}
	}

    void OnDisconnect(int from)
    {
        if(!converter.Contains(from))
        {
            return;
        }

        int id = converter.IndexOf(from);//AirConsole.instance.ConvertDeviceIdToPlayerNumber(from); // CONVERT DEVICE ID TO PLAYER NUMBER
        Destroy(GameObject.Find("sumo"+from));
        sumo.RemoveAt(id);
        cd.RemoveAt(id);
        cd1.RemoveAt(id);
        readyList.RemoveAt(id);
        iconColors.RemoveAt(id);
        playerHeads.RemoveAt(id);
        count--;
        //AirConsole.instance.SetActivePlayers(8);
        converter.Remove(from);
        if (!ready)
        {
            SetReadyFalse();
            for (int i = 0; i < sumo.Count; i++)
            {
                GameObject.Find("P" + (i + 1)).GetComponentInChildren<CanvasRenderer>().SetColor((Color)iconColors[i]);
                GameObject.Find("P" + count).GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)playerHeads[i];
                ((Walk)sumo[i]).SetSpawnPosition(GameObject.Find("Spawn" + (i+1)).transform.position);
            }
            for (int i = sumo.Count; i < 8; i++)
            {
                GameObject.Find("P" + (i + 1)).GetComponentInChildren<CanvasRenderer>().SetColor(Color.white);
                GameObject.Find("P" + count).GetComponentInChildren<SpriteRenderer>().sprite = null;
            }
        }
    } 
}
