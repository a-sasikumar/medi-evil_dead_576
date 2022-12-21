using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Doublsb.Dialog;

public class Level2Question : MonoBehaviour
{

    public DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        var dialogTexts = new List<DialogData>();
        var Text1 = new DialogData("What melts to become lava?", "Li");
        Text1.SelectList.Add("Correct", "Rocks");
        Text1.SelectList.Add("Wrong", "Ice");
        Text1.SelectList.Add("What", "Fire");
        Text1.Callback = () => check_correct1();
        dialogTexts.Add(Text1);

        dialogManager.Show(dialogTexts);

    }

    private void check_correct1() {
        if (dialogManager.Result =="Correct") {
           var dialogTexts = new List<DialogData>();
           var q2 = new DialogData ("Congratulations. Now, how hot are volcanoes to be able to make lava?");
            q2.SelectList.Add("Too Hot", "1200000C");
            q2.SelectList.Add("Correct", "1200C");
            q2.SelectList.Add("Cold", "120C");
            q2.Callback = () => check_correct2();
           dialogTexts.Add(q2);
           dialogManager.Show(dialogTexts);
        } else {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add (new DialogData ("Unfortunately you need to try again."));
           dialogManager.Show(dialogTexts);
        }
    }

    private void check_correct2() {
        if (dialogManager.Result =="Correct") {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add(new DialogData ("Well done, you have succeeded in answering my questions."));
           dialogManager.Show(dialogTexts);
        } else {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add (new DialogData ("Unfortunately you need to try again."));
           dialogManager.Show(dialogTexts);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
