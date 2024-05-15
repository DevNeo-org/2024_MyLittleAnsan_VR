using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //ID
    public int id;
    //대사 내용
    public string[] contexts;

}
[System.Serializable]
public class DialogueEvent
{
    //이벤트 이름
    public string name;

    //public Vector2 line;
    public Dialogue[] dialogues;

}