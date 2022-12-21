using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doublsb.Dialog;

public class Level1Question : MonoBehaviour
{

    public DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        var dialogTexts = new List<DialogData>();
        var Start = new DialogData("Answer my questions 2. If you answer wrong, you need to try again.");
        dialogTexts.Add(Start);
        var Text1 = new DialogData("Which is the biggest rainforest in the world?", "Li");
        Text1.SelectList.Add("Correct", "Amazon Rainforest");
        Text1.SelectList.Add("Wrong", "Congo Rainforest");
        Text1.SelectList.Add("What", "Daintree Rainforest");
        Text1.Callback = () => check_correct();
        dialogTexts.Add(Text1);

       var q2 = new DialogData ("Now, how old do you think the oldest tree on the planet is?");
        q2.SelectList.Add("Correct", "4853 years");
        q2.SelectList.Add("Too old!", "11786 years");
        q2.SelectList.Add("Nope", "135 years");
        q2.Callback = () => check_correct2();
        dialogTexts.Add(q2);
        dialogManager.Show(dialogTexts);

        
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
           SceneManager.LoadScene("level1");
        }
    }

    private void check_correct2() {
        if (dialogManager.Result =="Correct") {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add(new DialogData ("Congratulations."));
           dialogManager.Show(dialogTexts);
           SceneManager.LoadScene("Level2Inst");
        } else {
           var dialogTexts = new List<DialogData>();
           dialogTexts.Add (new DialogData ("Unfortunately you need to try again."));
           dialogManager.Show(dialogTexts);
           SceneManager.LoadScene("level1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
