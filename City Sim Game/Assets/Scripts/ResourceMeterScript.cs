using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceMeterScript : MonoBehaviour
{
    public string resourceString;

    private TextMeshProUGUI valueTM;
    private TextMeshProUGUI deltaTM;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI[] tms = GetComponentsInChildren<TextMeshProUGUI>();
        valueTM = tms[0];
        if (tms.Length == 2) { 
            deltaTM = tms[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        valueTM.text = Map.resourceManager.resources[resourceString].value.ToString();
        if (deltaTM) deltaTM.text = Map.resourceManager.resources[resourceString].delta.ToString();
    }
}
