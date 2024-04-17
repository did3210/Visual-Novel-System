using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.UIElements;
using System.Linq;




public class xmlTest1 : MonoBehaviour
{
    SelectVar SelectVar = new SelectVar();
    //Command variable
    private GameObject backgroud1;
    private GameObject backgroud2;
    //bool isChangeBg2 = false;
    
    //Xml 로드 관련 변수
    List<List<string>> list1 = new List<List<string>>();
    public Dictionary<string, List<List<string>>> dic1 = new Dictionary<string, List<List<string>>>();
    public Dictionary<string,int> ButtonClick_Counter = new Dictionary<string,int>();
    public CodeLagauge CodeLagauge;
    public int buttonclick = 0;
    
    public string nextID;
    public string currentID;

    //텍스트 출력및 타이핑 관련 변수들
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI nameText;
    public bool isTyping = false;

    [SerializeField]
    private GameObject MainContextBox;

    public ContextChoose3 contextchoose3 = new ContextChoose3();
    public ContextChoose2 Contextchoose2 = new ContextChoose2();
    public ContextChoose1 Contextchoose1 = new ContextChoose1();
    public GameObject Select3UI;
    public GameObject Select2UI;
    public GameObject Select1UI;

    public float StartWaitSeconds = 0.2f;
    public float TypeingWaitSeconds = 0.047f;

    //장면, 오디오 관련 변수들
    public GameObject EffectObject;
    public GameObject BGMObject;
    public GameObject SBGObject;

    public string previousBg; public string previousBGM; public string previousSFX;

    public List<string> CID_list = new List<string>();

    private void Start()
    {
        LoadXmlDialogues();
        FindCommander(ButtonClick_Counter[currentID]);
    }


    public void LoadXmlDialogues()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("dialogue/XmlLoad/dialoguesLoad");
        //Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList nodes = xmlDoc.SelectNodes("dialogueLoadInfo/dialogues");
        XmlNodeList CID = xmlDoc.SelectNodes("dialogueLoadInfo/startID");
        currentID = CID[0].InnerText;
        string dialogue;
        dialogue = nodes[0].InnerText;

        string[] dialogueTemp = dialogue.Split(',');
        for (int i = 0; i < dialogueTemp.Length; i++)
        {
            LoadXml("dialogue/dialogues/" + dialogueTemp[i]);
            
        }
    }

    public void LoadXml(string path)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(path);
        //Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("ContextInfo/Context");
        
        //XmlNodeList Contexts = xmlDoc.SelectNodes("ContextInfo/Context");
        string name = "";
        string text = "";
        string id = "";
        string Select1, Select2, Select3;
        string BGChanger = "";
        string EffectChanger = "";
        string BGMChanger = "";
        string SFXChanger = "";
        

        foreach (XmlNode node in nodes)
            {

            //node2 = node.ParentNode;
                try
                {
                    id = node.SelectSingleNode("id").InnerText;
                    dic1.Add(id, new List<List<string>>());
                    ButtonClick_Counter.Add(id, 0);
                    CID_list.Add(id);
                }
                catch (System.NullReferenceException)
                { }
                catch (System.ArgumentException)
                {
                    id = node.SelectSingleNode("id").InnerText;
                }

                try
                {
                    //text = node.SelectSingleNode("Text").InnerText;
                    name = node.SelectSingleNode("Text").Attributes["name"].Value;
                }
                catch (System.NullReferenceException)
                { }
                try
                {
                    //text = node.SelectSingleNode("Text").InnerText;
                    text = node.SelectSingleNode("Text").Attributes["text"].Value;
                }
                catch (System.NullReferenceException)
                { }


                try
                {
                    BGChanger = node.SelectSingleNode("Text").Attributes["BGChanger"].Value;
                }
                catch (System.NullReferenceException)
                { BGChanger = null; }

                try
                {
                    EffectChanger = node.SelectSingleNode("Text").Attributes["effect"].Value;
                }
                catch (System.NullReferenceException)
                { EffectChanger = null; }

                try
                {
                    BGMChanger = node.SelectSingleNode("Text").Attributes["BGM"].Value;
                }
                catch (System.NullReferenceException)
                { BGMChanger = null; }

                try
                {
                    SFXChanger = node.SelectSingleNode("Text").Attributes["SFX"].Value;
                }
                catch (System.NullReferenceException)
                { SFXChanger = null; }

                try
                { //문제 발생중 foreach 문에서
                    Select1 = node.SelectSingleNode("Choose/Select1").Attributes["text"].Value;
                    SelectVar.S1_idChange = node.SelectSingleNode("Choose/Select1").Attributes["idchange"].Value;
                    try { SelectVar.S1_command = node.SelectSingleNode("Choose/Select1").Attributes["command"].Value; } catch (System.NullReferenceException) { SelectVar.S1_command = null; }
                }
                catch (System.NullReferenceException)
                {
                    Select1 = "";
                    SelectVar.S1_idChange = null;
                }


                try
                {
                    Select2 = node.SelectSingleNode("Choose/Select2").Attributes["text"].Value;
                    SelectVar.S2_idChange = node.SelectSingleNode("Choose/Select2").Attributes["idchange"].Value;
                    try { SelectVar.S2_command = node.SelectSingleNode("Choose/Select1").Attributes["command"].Value; } catch (System.NullReferenceException) { SelectVar.S2_command = null; }
                }
                catch (System.NullReferenceException)
                {
                    Select2 = "";
                    SelectVar.S2_idChange = null;
                }

                try
                {
                    Select3 = node.SelectSingleNode("Choose/Select3").Attributes["text"].Value;
                    SelectVar.S3_idChange = node.SelectSingleNode("Choose/Select3").Attributes["idchange"].Value;
                    try { SelectVar.S3_command = node.SelectSingleNode("Choose/Select1").Attributes["command"].Value; } catch (System.NullReferenceException) { SelectVar.S3_command = null; }
                }
                catch (System.NullReferenceException)
                {
                    Select3 = "";
                    SelectVar.S3_idChange = null;
                }

                //print(SelectVar.S1_idChange);

                dic1[id].Add(new List<string> {
                name,
                text,
                Select1, Select2, Select3,
                SelectVar.S1_idChange, SelectVar.S2_idChange, SelectVar.S3_idChange,
                SelectVar.S1_command, SelectVar.S2_command, SelectVar.S3_command,
                BGChanger,EffectChanger,BGMChanger,SFXChanger }
                );
                //list1.Add(new List<string> { name, text, Select1, Select2, Select3, BGChanger });
            }
        
        //print(dic1[id][0][1]);
       
    }

    public void FindCommander(int index)
    // txt 커맨드를 정의하고 찾고 명령어를 실행시킵니다.
    {
        Select3UI.SetActive(false);
        string main = currentID; // 시작 id를 100으로 설정 id는 실행시킬 딕셔너리를 선택할 수 있다.
        int name = 0;
        int text = 1;
        int select1 = 2, select2 = 3, select3 = 4;
        int S1_idChanger = 5, S2_idChanger = 6, S3_idChanger = 7;
        int S1_command = 8, S2_command = 9, S3_command = 10;
        int BGChanger = 11, EffectChanger = 12, BGMChanger = 13, SFXChanger = 14;

        if (dic1[main][index][name] != string.Empty) {
            nameText.text = dic1[main][index][name];
        }
        if (dic1[main][index][text] != string.Empty) {
            MainContextBox.SetActive(true);
            dic1[main][index][text] = dic1[main][index][text].Replace("|n", Environment.NewLine);
            dic1[main][index][text] = dic1[main][index][text].Replace("[", "<");
            dic1[main][index][text] = dic1[main][index][text].Replace("]", ">");
            //print(dic1[main][index][text]);
            TypeingWaitSeconds = 0.047f;
            StartCoroutine(_typing(dic1[main][index][text]));
        }
        else { MainContextBox.SetActive(false); }

        //선택지 관련
        print(dic1[main][index][select1]);
        print(dic1[main][index][select2]);
        print(dic1[main][index][select3]);
        if (dic1[main][index][select3] != string.Empty) {
            Select1UI.SetActive(false);
            Select2UI.SetActive(false);

            Select3UI.SetActive(true);
            contextchoose3.CSelect1.text = dic1[main][index][select1]; 
            contextchoose3.CSelect2.text = dic1[main][index][select2];
            contextchoose3.CSelect3.text = dic1[main][index][select3];
        }
        else if (dic1[main][index][select2] != string.Empty) {
            Select1UI.SetActive(false);
            Select3UI.SetActive(false);

            Select2UI.SetActive(true);
            Contextchoose2.CSelect1.text = dic1[main][index][select1];
            Contextchoose2.CSelect2.text = dic1[main][index][select2];
        }
        else if (dic1[main][index][select1] != string.Empty) {
            Select2UI.SetActive(false);
            Select3UI.SetActive(false);

            Select1UI.SetActive(true);
            Contextchoose1.CSelect1.text = dic1[main][index][select1];
        }
        else { 
            Select3UI.SetActive(false);
            Select2UI.SetActive(false);
            Select1UI.SetActive(false);
        }


        if (dic1[main][index][S1_idChanger] != null)
        {
            SelectVar.S1_nextID = dic1[main][index][S1_idChanger];
        }
        if (dic1[main][index][S2_idChanger] != null)
        {
            SelectVar.S2_nextID = dic1[main][index][S2_idChanger];
        }
        if (dic1[main][index][S3_idChanger] != null)
        {
            SelectVar.S3_nextID = dic1[main][index][S3_idChanger];
        }

        //Command 명령어들을 다룹니다.
        if (dic1[main][index][S1_command] != null)
        {
            string[] S1commnad_temp = dic1[main][index][S1_command].Trim().Split("\n");
            for( int i = 0; i < S1commnad_temp.GetLength(0); i++)
            {
                S1commnad_temp[i] = S1commnad_temp[i].Trim();
                //print(S1commnad_temp[i]);
                CodeLagauge.CL_COMMAND(S1commnad_temp[i]);
                //CodeLanguageCommand(S1commnad_temp[i]);
            }
        }

        if (dic1[main][index][S2_command] != null)
        {
            string[] S2commnad_temp = dic1[main][index][S2_command].Trim().Split("\n");
            for (int i = 0; i < S2commnad_temp.GetLength(0); i++)
            {
                S2commnad_temp[i] = S2commnad_temp[i].Trim();
                //print(S2commnad_temp[i]);
                CodeLagauge.CL_COMMAND(S2commnad_temp[i]);
                //CodeLanguageCommand(S1commnad_temp[i]);
            }
        }

        if (dic1[main][index][S3_command] != null)
        {
            string[] S3commnad_temp = dic1[main][index][S3_command].Trim().Split("\n");
            for (int i = 0; i < S3commnad_temp.GetLength(0); i++)
            {
                S3commnad_temp[i] = S3commnad_temp[i].Trim();
                //print(S3commnad_temp[i]);
                CodeLagauge.CL_COMMAND(S3commnad_temp[i]);
                //CodeLanguageCommand(S1commnad_temp[i]);
            }
        }

        //배경 관련
        //print(dic1[main][index][BGChanger]);
        if (dic1[main][index][BGChanger] != null) {

            SBGObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/" + dic1[main][index][BGChanger]);
            previousBg = dic1[main][index][BGChanger];
            //추후 애니메이션 백그라운드 제어를 위해 아래 코드를 남견둔다.
            //string[] BGtemp_split = dic1[main][index][BGChanger].Split(",");
            /*if (BGtemp_split[0] != null || BGtemp_split[1] != null) {
                print(BGtemp_split[0]);
                print(BGtemp_split[1]);
                BackGroundChanger(BGtemp_split[0], BGtemp_split[1]);
            }  */
        }

        //EFFECT 관련
        if (dic1[main][index][EffectChanger] != null)
        {
            string[] temp = dic1[main][index][EffectChanger].Split(",");
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                print(temp[i]);
                if (temp[i].Contains("Fade"))
                { EffectObject.GetComponent<Fade2Effect>().EffectStart(temp[i]); }
                else if (temp[i].Contains("Shake"))
                { EffectObject.GetComponent<CameraShake>().Shake();}
            }
        }

        if (dic1[main][index][BGMChanger] != null && dic1[main][index][BGMChanger] != "stop")
        {
            BGMObject.GetComponent<SoundManager>().BgSoundPlay(Resources.Load("Audio/Music/" + dic1[main][index][BGMChanger]) as AudioClip);
            previousBGM = dic1[main][index][BGMChanger];
        }
        else if (dic1[main][index][BGMChanger] == "stop")
        {
            BGMObject.GetComponent<SoundManager>().BgSoundStop();
            previousBGM = "stop";
        }

        if (dic1[main][index][SFXChanger] != null)
        {
            string[] temp = dic1[main][index][SFXChanger].Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                BGMObject.GetComponent<SoundManager>().SFX(Resources.Load("Audio/SoundEffect/" + temp[i]) as AudioClip);
            }
        }
        




        IEnumerator _typing(string t) {
            yield return new WaitForSeconds(StartWaitSeconds);
            
            for (int i = 0; i <= t.Length; i++) {
                
                talkText.text = t.Substring(0,i);
                isTyping = true;
                yield return new WaitForSeconds(TypeingWaitSeconds);
            }
            isTyping = false;
        }

    }

    

    public void BackGroundChanger(string IMG_FILE1, string IMG_FILE2)
    // 애니메이션을 넣을 수 있는 장면의 배경을 바꿔줍니다.
    {

        backgroud1 = GameObject.Find(IMG_FILE1);
        backgroud2 = GameObject.Find(IMG_FILE2);
        
        backgroud1.SetActive(false);
        backgroud2.SetActive(true);
        //isChangeBg2 = true;
        
        /*else if (isChangeBg2 == true) {
            backgroud1.SetActive(true);
            backgroud2.SetActive(false);
            isChangeBg2 = false;
        }*/
    }

    public void SBGChanger()
    // 배경 이미지를 바꿉니다.
    {
        SBGObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/");
    }

    public void ButtonPressed()
    {
        string main = currentID;
        print(main);
        print(ButtonClick_Counter[currentID]);
        
        // 버튼 클릭시 일어나는 이벤트들을 정의합니다.
        try {

            

            if (isTyping == false) {
                TypeingWaitSeconds = 0.047f;
                if (ButtonClick_Counter[currentID] < dic1[main].Count-1) {
                    ButtonClick_Counter[currentID] = ButtonClick_Counter[currentID]+1;
                    FindCommander(ButtonClick_Counter[currentID]);
                }
                else {
                    ButtonClick_Counter[currentID] = dic1[main].Count-1;
                }
            }
            else if (isTyping == true) {
                TypeingWaitSeconds = 0f;
            }

        }

        catch (System.ArgumentOutOfRangeException)
        { }

    }

    public void Select1_Pressed()
    {
        string main = currentID;
        try {
            if (isTyping == false) {
                
                currentID = SelectVar.S1_nextID;
                if (isTyping == false) {
                    
                    if (ButtonClick_Counter[currentID] < dic1[main].Count) {
                        FindCommander(ButtonClick_Counter[currentID]);
                        //ButtonClick_Counter[currentID] += 1;
                    }
                    else {
                        ButtonClick_Counter[currentID] = dic1[main].Count;
                    }
                }
            }
        }

        catch (System.ArgumentOutOfRangeException)
        { }

    }
    public void Select2_Pressed()
    {
        string main = currentID;
        try {
            if (isTyping == false) {
                
                currentID = SelectVar.S2_nextID;
                if (isTyping == false) {
                    
                    if (ButtonClick_Counter[currentID] < dic1[main].Count) {
                        FindCommander(ButtonClick_Counter[currentID]);
                        //ButtonClick_Counter[currentID] += 1;
                    }
                    else {
                        ButtonClick_Counter[currentID] = dic1[main].Count;
                    }
                }
            }
        }
        catch (System.ArgumentOutOfRangeException)
        { }
    }
    public void Select3_Pressed()
    {
        string main = currentID;
        try {
            if (isTyping == false) {
                currentID = SelectVar.S3_nextID;
                if (isTyping == false) {
                    
                    if (ButtonClick_Counter[currentID] < dic1[main].Count)
                    {
                        FindCommander(ButtonClick_Counter[currentID]);
                        //ButtonClick_Counter[currentID] += 1;
                    }
                    else
                    {
                        ButtonClick_Counter[currentID] = dic1[main].Count;
                    }
                }
            }
        }
        catch (System.ArgumentOutOfRangeException)
        { }
    }
}

[Serializable]
public class ContextChoose3
{
    public TextMeshProUGUI CSelect1;
    public TextMeshProUGUI CSelect2;
    public TextMeshProUGUI CSelect3;
}
[Serializable]
public class ContextChoose2
{
    public TextMeshProUGUI CSelect1;
    public TextMeshProUGUI CSelect2;
}
[Serializable]
public class ContextChoose1
{
    public TextMeshProUGUI CSelect1;
}

public class SelectVar
{
    public string S1_idChange;
    public string S2_idChange;
    public string S3_idChange;

    public string S1_nextID;
    public string S2_nextID;
    public string S3_nextID;

    public string S1_command, S2_command, S3_command;
}




