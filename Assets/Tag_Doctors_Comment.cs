using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tag_Doctors_Comment : MonoBehaviour
{
    public Button[] answers;
    public Text Output;

    public void SetText(string NewOutput, bool [] buttonsActivated)
    {
        Output.text = NewOutput;

        if (buttonsActivated.Length != answers.Length)
            return;
        for (int i=0; i<buttonsActivated.Length;i++)
            answers[i].gameObject.SetActive(buttonsActivated[i]);
    }
}
