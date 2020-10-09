using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageManager : MonoBehaviour
{
	// Prefab representing a neutral message.
	public static GameObject prefab;

	// List of gameobjects currently on-screen.
	private static List<GameObject> messages = new List<GameObject>();

	// Number of gameobjects currently on-screen.
	private static int counter = 0;

    public void Awake()
    {
        prefab = Resources.Load<GameObject>("Prefabs/GenericMessage");
    }

	public static void Warn(string message)
	{
		GameObject ob = Instantiate<GameObject>(prefab);
        ob.transform.parent = GameObject.FindGameObjectWithTag("CANVAS").transform;
		Render(ob, message);
	}

	public static void Info(string message)
	{
		GameObject ob = Instantiate<GameObject>(prefab);
		ob.GetComponent<Image>().color = Color.white;
		Render(ob, message);
	}

	// Render given gameobject with specified message.
	public static void Render(GameObject ob, string message)
	{
		// Append to list.
		messages.Add(ob);

		// Fetch dimensions of object.
		float height = ob.GetComponent<RectTransform>().rect.height;
		float width  = ob.GetComponent<RectTransform>().rect.width;

		// Position on top-right corner of screen.
		ob.transform.position = new Vector2(Camera.main.pixelWidth - width / 2, Camera.main.pixelHeight - height / 2);

		// Push it under the current bottom-most message.
		ob.transform.position -= Vector3.up * height * counter++;

		// Set message.
		ob.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;

		// When button is clicked, pull it.
		ob.transform.GetComponent<Button>().onClick.AddListener(() => Pull(ob));
	}

	// Destroys specified object and removes it from messages list and close the
	// gap between panels.
	public static void Pull(GameObject ob)
	{
		int index = messages.IndexOf(ob);
		int count = messages.Count();

		// Raise objects under the specified object to close the gap.
		foreach (int i in Enumerable.Range(index, count - index)) {

			// Fetch height of object over ob.
			float height = messages[i > 0 ? i - 1 : 0].GetComponent<RectTransform>().rect.height;

			// Raise object.
			messages[i].transform.position += Vector3.up * height;
		}

		Destroy(ob);
		messages.Remove(ob);
		counter--;
	}
}
