using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    public Dialogue[] Parse(string fileName)
    {
        //대사 리스트 생성
        List<Dialogue> dialogueList = new List<Dialogue>();
        //텍스트 파일 불러오기
        TextAsset textData = Resources.Load<TextAsset>(fileName);

        //  data = 한 줄씩 split
        string[] data = textData.text.Split(new char[] { '\n' });

        for (int i = 0; i < data.Length;)
        {
            //row[0] : ID
            //row[1] : 대사
            //'@'로 ID와 대사를 구분
            string[] row = data[i].Split(new char[] { '@' });

            Dialogue dialogue = new Dialogue();
            //ID 할당
            int strToInt = int.Parse(row[0]);
            dialogue.id = strToInt;
            //대사 할당
            List<string> contextList = new List<string>();
            do
            {
                contextList.Add(row[1]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { '@' });
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();
    }

    void Start()
    {
        Parse("MyLittleAnsan_Dialog");
    }


}
