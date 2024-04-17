using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SettingApply : MonoBehaviour
{

    public void Awake()
    {
        
        LoadSettingValue();
        
    }

    public void SettingAplyButton()
    {
        CreateSettingValue();
        LoadSettingValue();
    }

    public void CreateSettingValue()
    {
        XmlDocument xmlDoc = new XmlDocument();
        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        // 루트 노드 생성       
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "SettingInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Setting", string.Empty);
        root.AppendChild(child);

        XmlElement SoundVolume = xmlDoc.CreateElement("SoundVolume");
        SoundVolume.InnerText = SoundManager.BGMvolume.ToString();
        child.AppendChild(SoundVolume);

        xmlDoc.Save(Application.persistentDataPath + "/Setting.xml");
    }

    public void LoadSettingValue()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.persistentDataPath + "/Setting.xml");
        XmlNode Load_Setting = xmlDoc.SelectSingleNode("SettingInfo/Setting/SoundVolume");

        SoundManager.BGMvolume = float.Parse(Load_Setting.InnerText);
    }
}
