using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Message : MonoBehaviour
{

	public GameObject warning;
	public GameObject message;

	private int counter = 0;

	// Start is called before the first frame update
	void Start()
	{
		//
	}

	public void Warn(string message)
	{
		GameObject ob = Instantiate<GameObject>(warning, transform);
		ob.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
		float height = ob.GetComponent<RectTransform>().rect.height;
		ob.transform.position += Vector3.up * height * counter++;
		// textMeshReference.text = "HEJ!";
		ob.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => Test(ob));
	}

	public void Test(GameObject ob)
	{
		Destroy(ob);
		counter--;
	}
}
