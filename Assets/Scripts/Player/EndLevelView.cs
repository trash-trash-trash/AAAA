using TMPro;
using UnityEngine;

public class EndLevelView : MonoBehaviour
{
    public EndLevel endLevel;

    public GameObject canvasObj;

    public TMP_Text newRecordText;
    public TMP_Text timeText;
    public TMP_Text deathText;
    public TMP_Text rankText;

    public GameObject newRecordObj;

    public Color newRecordColorOne;
    public Color newRecordColorTwo;

    private float speed = 5f;
    
    void OnEnable()
    {
        endLevel.AnnounceEndLevelScreen += SetText;
        canvasObj.SetActive(false);
    }

    private void SetText(float arg1, int arg2, Rank arg3, bool newRecord)
    {
        canvasObj.SetActive(true);
        
        newRecordObj.SetActive(newRecord);
        
        int minutes = Mathf.FloorToInt(arg1 / 60f);
        int seconds = Mathf.FloorToInt(arg1 % 60f);
        int milliseconds = Mathf.FloorToInt((arg1 * 1000f) % 1000f);

        timeText.text = $"TIME: {minutes:00}:{seconds:00}<size=50%>.{milliseconds:000}</size>";
        
        deathText.text = "DEATHS: "+arg2.ToString();
        rankText.text = "RANK: "+arg3.ToString();
    }

    void Update()
    {
            // oscillates between 0 and 1
            float t = Mathf.PingPong(Time.time * speed, 1f);

            Color lerped = Color.Lerp(newRecordColorOne, newRecordColorTwo, t);
            newRecordText.color = lerped;
    }

    void OnDisable()
    {
        endLevel.AnnounceEndLevelScreen -= SetText;
    }
}