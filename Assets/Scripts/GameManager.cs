using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Missions missions;
    private Missions.MissionStruct[] MissionsArray;
    private List<GameObject> missionsCreated = new List<GameObject>();

    [Header("Scroll View")]
    [SerializeField] private GameObject layoutPrefab;
    [SerializeField] private Transform content;

    [Header("Animations")]
    [SerializeField] private Animator popUpAnimator;
    [SerializeField] private Animator goldAnimator;
    [SerializeField] private TextMeshProUGUI goldCount;
    [SerializeField] private int animationSpeed = 1;
    [SerializeField] private AudioSource coinSound;
    private int goldAmount = 0, scoreToReach = 0;

    private void Start() 
    {   
        missions = Missions.Instance;
        GetMissionInfo(missions.missionsSaved); 
        SetMissionsOnContent();

        goldCount.text = "0";
    }

    #region Missions

    private void GetMissionInfo(Missions.MissionStruct[] args)
    {
        MissionsArray = args;
    }

    private void SetMissionsOnContent()
    {
        for(int i=0; i<MissionsArray.Length; i++)
        {
            var newMissions = Instantiate(layoutPrefab, content);
            missionsCreated.Add(newMissions);

            var textToChange = newMissions.GetComponentsInChildren<TextMeshProUGUI>();

            textToChange[0].text = MissionsArray[i].explanation;
            textToChange[1].text = "[" + MissionsArray[i].completed + "/" + MissionsArray[i].required + "]";
        }
    }

    public void MissionCompleted(int key)
    {
        MissionsArray[key].completed++;

        var textToChange = missionsCreated[key].GetComponentsInChildren<TextMeshProUGUI>();
        textToChange[1].text = "[" + MissionsArray[key].completed + "/" + MissionsArray[key].required + "]";

        if(!(MissionsArray[key].completed == MissionsArray[key].required))
            return;

        //Already finished the mission!!

        //Delete the prefab from the content
        Destroy(missionsCreated[key]);

        //Make popup animation
        popUpAnimator.SetTrigger("Trigger");

        //After popup animation is finished, increase gold value with an animation
        scoreToReach += MissionsArray[key].gold;
        goldAnimator.SetTrigger("Trigger");
        coinSound.Play();
    }

    #endregion

    #region GoldCount

    private void Update()
    {
        if(goldAmount == scoreToReach) return;

        else if(goldAmount>scoreToReach) 
        {
            goldAmount = scoreToReach;
            goldCount.text = goldAmount.ToString();
            return;
        }

        goldAmount += (int)(animationSpeed * Time.deltaTime);
        goldCount.text = goldAmount.ToString();
    }

    #endregion

}