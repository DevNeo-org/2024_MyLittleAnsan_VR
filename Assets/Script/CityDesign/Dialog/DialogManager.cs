using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogManager : MonoBehaviour
{
    public static DatabaseManager instance;
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;


    [SerializeField] DialogueEvent dialogue;
    //대화창
    [SerializeField] GameObject dialogUI;
    //텍스트
    [SerializeField] TextMeshProUGUI dialogText;
    //텍스트 출력 딜레이
    [SerializeField] float textDelay;

    Dialogue[] dialogues;
    bool isNext = false;
    int lineCount = 0;//대화 카운트
    int contextCount = 0; //대사 카운트
    public GameObject dialog;
    bool isDialogue = false;
    bool isTyping = false;


    void Start()
    {
        //대화 내용 딕셔너리에 저장
        
            
        
    }
    void Awake()
    {
        DialogParser theParser = GetComponent<DialogParser>();
        Dialogue[] dialogues = theParser.Parse("MyLittleAnsan_Dialog");
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }
        isFinish = true;

        if (!System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            int sceneNum = SceneManager.GetActiveScene().buildIndex;
            dialogUI.SetActive(true);
            isDialogue = true;
            ShowDialogue(GetDialogue(sceneNum));
        }

        
    }
    void Update()
    {
        //대화 중인지 확인
        if (isDialogue)
        {
            //다음 대사로 넘어갈 경우
            if (isNext)
            {
                if (!isTyping)
                {
                    isTyping = true;
                    isNext = false;
                    //대화 텍스트 비우기
                    dialogText.text = "";
                    //대사가 더 남아있을 경우
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWriter());//타이핑 효과 실행
                    }
                    else//대사가 끝났을 경우
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)//대화가 남아있을 경우
                        {
                            StartCoroutine(TypeWriter());//다음 대화 출력
                        }
                        else//대화가 모두 끝났을 경우
                        {
                            //dialogText.text = "앞에 있는 버튼을 눌러보세요";
                            EndDialogue();
                        }
                    }
                }
            }
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


    void EndDialogue()
    {
        dialogUI.SetActive(false);
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        dialogText.text = "";

        dialogues = p_dialogues;

        StartCoroutine(TypeWriter());
    }

    IEnumerator TypeWriter()//텍스트를 출력하는 코루틴
    {
        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        //t_ReplaceText = t_ReplaceText.Replace("'", ","); //'을 ,로 치환
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            dialogText.text += t_ReplaceText[i];
            //글자 출력 간 딜레이
            yield return new WaitForSeconds(textDelay);
        }
        //대사가 끝나면 다음 대사로
        isNext = true;
        isTyping = false;

        yield return null;
    }

}
