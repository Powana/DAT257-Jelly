using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeleteTimerScript : MonoBehaviour
{
    public float timeToLive = 5.0f;
    private Image r;
    private TextMeshProUGUI tm;

    private void OnEnable()
    {
        r = GetComponent<Image>();
        tm = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            StartCoroutine(fadeOut());
        }
    }

    // "Borrowed" from https://stackoverflow.com/a/44935943
    IEnumerator fadeOut()
    {
        float duration = 1f;
        float counter = 0;
        //Get current color
        Color spriteColor = r.color;
        Color textColor = tm.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            //Change alpha only
            r.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            tm.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        MessageManager.counter--;
        Destroy(this.gameObject);

    }
}
