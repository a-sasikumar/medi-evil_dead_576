using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class Level2Question : MonoBehaviour
{

    public DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        var dialogTexts = new List<DialogData>();
        var Start = new DialogData("Answer my questions 2. If you answer wrong, you need to try again.");
        dialogTexts.Add(Start);
        var Text1 = new DialogData("What melts to become lava?", "Li");
        Text1.SelectList.Add("Correct", "Rocks");
        Text1.SelectList.Add("Wrong", "Ice");
        Text1.SelectList.Add("What", "Fire");
        Text1.Callback = () => check_correct();
        dialogTexts.Add(Text1);

       var q2 = new DialogData ("Now, how hot are volcanoes to be able to make lava?");
        q2.SelectList.Add("Too Hot", "1200000C");
        q2.SelectList.Add("Correct", "1200C");
        q2.SelectList.Add("Cold", "120C");
        q2.Callback = () => check_correct2();
        dialogTexts.Add(q2);
        dialogManager.Show(dialogTexts);

        // TODO load success scene
    }

    private void check_correct() {
        if (dialogManager.Result =="Correct") {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add(new DialogData ("Congratulations."));
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
           dialogTexts.Add(new DialogData ("Congratulations."));
           dialogManager.Show(dialogTexts);
          SceneManager.LoadScene("EndScene");
        } else {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add (new DialogData ("Unfortunately you need to try again."));
           dialogManager.Show(dialogTexts);
           SceneManager.LoadScene("Level2");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
