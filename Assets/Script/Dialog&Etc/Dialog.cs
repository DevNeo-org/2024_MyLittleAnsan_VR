using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //ID
    public int id;
    //��� ����
    public string[] contexts;

}
[System.Serializable]
public class DialogueEvent
{
    //�̺�Ʈ �̸�
    public string name;

    //public Vector2 line;
    public Dialogue[] dialogues;

}