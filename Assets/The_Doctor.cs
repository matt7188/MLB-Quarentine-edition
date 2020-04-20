using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class The_Doctor : MonoBehaviour
{

    public enum Poses { Explaining, Nutral,Sad, Excited};

    public Poses CurrentPose;

    public Sprite [] avalible;

    Image MySprite;

    public void ChangePose(Poses newPose)
    {
        Debug.Log(newPose.ToString());

        CurrentPose = newPose;
        switch (newPose)
        {
            case Poses.Explaining:
                MySprite.sprite = avalible[0];
                break;
            case Poses.Nutral:
                MySprite.sprite = avalible[1];
                break;
            case Poses.Sad:
                MySprite.sprite = avalible[2];
                break;
            case Poses.Excited:
                MySprite.sprite = avalible[3];
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MySprite = GetComponent<Image>();
        ChangePose(CurrentPose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
