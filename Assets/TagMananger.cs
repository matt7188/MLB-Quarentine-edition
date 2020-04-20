using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagMananger : MonoBehaviour
{
    public Text numberIt;
    public Text Timer;
    public Text StarNumber;
    float Time;
    public Tag_Opponents [] AllOpponents;

    public GameObject movementControls;
    public GameObject DoctorsText;

    public The_Doctor doctor;

    public bool paused;

    Tag_Doctors_Comment TDC;

    bool[] usedButtons;
    bool[] onlyGameStart;

    public int NumberOfStars;

    bool LessPuffs;
    bool MultipleTags;
    int NumberOfTags;
    public bool Color;
    public bool TimedTag;
    bool Isolated;


    public GameObject SickAndSad;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(375, 667, false);
        LessPuffs = false;
         MultipleTags=false;
         Color=false;
         TimedTag = false;

        numberIt.text = "Number Tagged: 1";
        AllOpponents = FindObjectsOfType<Tag_Opponents>();

        TDC = FindObjectOfType<Tag_Doctors_Comment>();


        paused = true;
        DoctorsText.SetActive(true);
        doctor.ChangePose(The_Doctor.Poses.Nutral);
        movementControls.SetActive(false);
        onlyGameStart = new bool[] { false, true, false, false, false, false };
        TDC.SetText("Your goal is to last as long as you can without getting tagged, while collecting stars If you collect all the stars you win!", onlyGameStart);

        usedButtons = new bool[] { true, false, true, false, true, true };

        SickAndSad.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        int x = 0;
        foreach (Tag_Opponents checking in AllOpponents)
        {
            if (checking.It)
                x++;
        }

        if (x==0)
        {
            AllOpponents[Random.Range(0, AllOpponents.Length)].It=true;
        }

        numberIt.text = "Number Tagged: "+ x;
        StarNumber.text = "Stars Found: " + NumberOfStars;

        if(NumberOfStars== 10)
        { EndGame(); }

    }


    public void StartNewGame()
    {
        NumberOfStars = 0;
           Time = 0;
        NumberOfTags = 0;

        int countOut = 0;

        foreach (Tag_Opponents set in AllOpponents)
        {
            
            set.transform.position = set.StartingPoint;
            set.It = false;
                    set.CurrentlyOut = false;

            if (LessPuffs && countOut< (AllOpponents.Length/2))
                if (Random.Range(0, 1) == 0)
                {
                    countOut++;
                    set.CurrentlyOut = true;
                    set.transform.position = new Vector3(0, 100000, 0);
                }


        }
        if (!Isolated)
        {
            int hold = Random.Range(0, AllOpponents.Length);
            while (AllOpponents[hold].CurrentlyOut)
            {
                Debug.Log("hit");
                hold = Random.Range(0, AllOpponents.Length);
            }
            AllOpponents[hold].It = true;
        }

        FindObjectOfType<TagPlayerMovment>().transform.position = new Vector2(0, 5.6f);

        paused = false;
        DoctorsText.SetActive(false);
        movementControls.SetActive(true);
        StartCoroutine(TimerSeconds());

        FindObjectOfType<Star_Manager>().SetStars();

    }

    public bool EndGame()
    {

        


            if (MultipleTags)
        {
            NumberOfTags++;
            if (NumberOfTags<3)
            return false;
        }

        paused = true;
        DoctorsText.SetActive(true);
        movementControls.SetActive(false);
        doctor.ChangePose(The_Doctor.Poses.Sad);

        if (NumberOfStars == 10)
            TDC.SetText("You got all the stars! Good job! would you like to try any of those again?",
            new bool[] { true, true, true, true, true, true });
        else
            TDC.SetText("You stayed untagged for "+Time.ToString()+" Seconds. You did a pretty good job, did you notice how it got harder and harder to stay untagged as more and more people became tagged? What could we do to make it easier for you to not be tagged?",
            usedButtons);

        return true;
    }


    public void UseButton(int whichButton)
    {
        usedButtons[whichButton] = false;



        LessPuffs = false;
        MultipleTags = false;
        Color = false;
        TimedTag = false;

        switch (whichButton)
        {

            case 0:
                LessPuffs = true;
                TDC.SetText("If we remove the number of puffs, there are less to pass around the tag. This is why social distancing, or the ability to keep people seperated, is so important!", onlyGameStart);
                break;
            case 2:
                Color = true;
                TDC.SetText("Your right, if you could easily see who has been tagged, it can help you avoid them!", onlyGameStart);
                break;
            case 3:
                Isolated = true;
                TDC.SetText("Put everyone who is it in their own special place? Well aren’t you clever! Doctors actually do this, they call it Quarantine.", onlyGameStart);
                SickAndSad.SetActive(true);
                break;
            case 4:
                MultipleTags = true;
                TDC.SetText("This is very similar to taking medicines and wearing special clothing that makes it harder for you to catch the virus. Let’s see how that goes!", onlyGameStart);
                break;
            case 5:
                TimedTag = true;
                TDC.SetText("If we give people time to recover from being sick, they can't pass it on! Lets see what happens if they are only tagged for a little bit!", onlyGameStart);
                break;
        }
    
        if (!(usedButtons[0]|| usedButtons[2] || usedButtons[4] || usedButtons[5]))
        {
            usedButtons[3] = true;
        }

    }

    

    IEnumerator TimerSeconds()
    {
        while (!paused) {
            yield return new WaitForSeconds(1);
            //Debug.Log("Click");
            Time++;
            Timer.text = Time.ToString();
        }
    }
}
