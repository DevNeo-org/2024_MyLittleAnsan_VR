using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    public Dialogue[] Parse(string fileName)
    {
        //��� ����Ʈ ����
        List<Dialogue> dialogueList = new List<Dialogue>();
        //�ؽ�Ʈ ���� �ҷ�����
        TextAsset textData = Resources.Load<TextAsset>(fileName);

        //  data = �� �پ� split
        string[] data = textData.text.Split(new char[] { '\n' });

        for (int i = 0; i < data.Length;)
        {
            //row[0] : ID
            //row[1] : ���
            //'@'�� ID�� ��縦 ����
            string[] row = data[i].Split(new char[] { '@' });

            Dialogue dialogue = new Dialogue();
            //ID �Ҵ�
            int strToInt = int.Parse(row[0]);
            dialogue.id = strToInt;
            //��� �Ҵ�
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
