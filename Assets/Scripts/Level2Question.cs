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
        Text1.Callback = () => check_correct();
        dialogTexts.Add(Text1);

        dialogManager.Show(dialogTexts);

    }

    private void check_correct() {
        if (dialogManager.Result =="Correct") {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add (new DialogData ("Congratulations, you may proceed."));
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
