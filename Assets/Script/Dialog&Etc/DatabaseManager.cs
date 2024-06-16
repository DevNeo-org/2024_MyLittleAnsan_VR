using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    void Awake()
    {
        //대화 내용 딕셔너리에 저장
        if (instance == null)
        {
            instance = this;
            DialogParser theParser = GetComponent<DialogParser>();
            Dialogue[] dialogues = theParser.Parse("MyLittleAnsan_Dialog");
            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
            isFinish = true;
        }
    }
    public Dialogue[] GetDialogue(int id)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 1; i < dialogueDic.Count; i++)
        {
            if (dialogueDic[i].id == id)
                dialogueList.Add(dialogueDic[i]);
        }
        return dialogueList.ToArray();
    }
}