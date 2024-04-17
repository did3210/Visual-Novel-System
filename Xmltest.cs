using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Xmltest : MonoBehaviour
{
    void Start() 
    { 
        
    }

    public void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)       
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        // 루트 노드 생성       
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);


        // 자식 노드에 들어갈 속성 생성
        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = "wergia";
        child.AppendChild(name);
        XmlElement lv = xmlDoc.CreateElement("Level");
        lv.InnerText = "1";
        child.AppendChild(lv);
        XmlElement exp = xmlDoc.CreateElement("Experience");
        exp.InnerText = "45";
        child.AppendChild(exp);
        xmlDoc.Save("./Assets/Resources/Character.xml");
    }

    public void LoadXml() 
    { 
        TextAsset textAsset = (TextAsset)Resources.Load("Character");
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("CharacterInfo/Character");

        foreach (XmlNode node in nodes) 
        {
            print(node);
            //Debug.Log("Name :: " + node.SelectSingleNode("Name").InnerText);
            //Debug.Log("Level :: " + node.SelectSingleNode("Level").InnerText);
            //Debug.Log("Exp :: " + node.SelectSingleNode("Experience").InnerText);
        }
    }
    
}
