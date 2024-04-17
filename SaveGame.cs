using JetBrains.Annotations;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public xmlTest1 game;

    public static bool mainmenu_load = false;
    public string pp;
    public void CreateSaveXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        // 루트 노드 생성       
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "SaveInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Save", string.Empty);
        root.AppendChild(child);


        // 자식 노드에 들어갈 속성 생성
        XmlElement savegameName = xmlDoc.CreateElement("savegameName");
        savegameName.InnerText = "save1";
        child.AppendChild(savegameName);

        XmlElement CID = xmlDoc.CreateElement("CID");
        CID.InnerText = game.currentID;
        child.AppendChild(CID);

        XmlElement previousBGM = xmlDoc.CreateElement("previousBGM");
        previousBGM.InnerText = game.previousBGM;
        child.AppendChild(previousBGM);

        XmlElement previousSFX = xmlDoc.CreateElement("previousSFX");
        previousBGM.InnerText = game.previousSFX;
        child.AppendChild(previousSFX);

        XmlElement previousBg = xmlDoc.CreateElement("previousBg");
        previousBg.InnerText = game.previousBg;
        child.AppendChild(previousBg);



        for (int i = 0; i < game.CID_list.Count; i++)
        {
            XmlElement btnClick = xmlDoc.CreateElement("CIDcount" + game.CID_list[i]);
            btnClick.InnerText = Convert.ToString(game.ButtonClick_Counter[game.CID_list[i]]);
            child.AppendChild(btnClick);
        }
        
        xmlDoc.Save(Application.persistentDataPath +"/Save.xml");
    }

    public void Update()
    {
        if(mainmenu_load == true)
        {
            LoadSaveFile();
            mainmenu_load = false;
        }
    }
    public void LoadSaveFile()
    {
        //TextAsset textAsset = (TextAsset)Resources.Load(Application.persistentDataPath + "/Save.xml");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.persistentDataPath + "/Save.xml");
        XmlNode load_CID = xmlDoc.SelectSingleNode("SaveInfo/Save/CID");
        XmlNode load_BGM = xmlDoc.SelectSingleNode("SaveInfo/Save/previousBGM");
        XmlNode load_SFX = xmlDoc.SelectSingleNode("SaveInfo/Save/previousSFX");
        XmlNode load_BG = xmlDoc.SelectSingleNode("SaveInfo/Save/previousBg");
        
        pp = load_CID.InnerText;
        game.currentID = pp;
        game.BGMObject.GetComponent<SoundManager>().BgSoundPlay(Resources.Load("Audio/Music/" + load_BGM.InnerText) as AudioClip);
        game.BGMObject.GetComponent<SoundManager>().SFX(Resources.Load("Audio/SoundEffect/" + load_SFX.InnerText) as AudioClip);
        game.SBGObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/" + load_BG.InnerText);
        for (int i = 0; i < game.CID_list.Count; i++)
        {
            XmlNode save_btnclick_node = xmlDoc.SelectSingleNode("SaveInfo/Save/CIDcount" + game.CID_list[i]);
            print(save_btnclick_node.InnerText);
            game.ButtonClick_Counter[game.CID_list[i]] = Convert.ToInt32(save_btnclick_node.InnerText);
        }

        string main = game.currentID;

        try
        {
            if (game.isTyping == false)
            {
                
                    
                        game.FindCommander(game.ButtonClick_Counter[game.currentID]);
                        //game.ButtonClick_Counter[game.currentID] += 1;
                    
                
            }
        }
        catch (System.ArgumentOutOfRangeException)
        { }


        //game.buttonclick = Convert.ToInt32(save_btnclick_node.InnerText);
    }

    
}
