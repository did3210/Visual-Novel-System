using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using TMPro;
using System;

public class FileOpenRead : MonoBehaviour
{
    
    List<string> UNovelCommand_List = new List<string>();

    public int buttonclick = 0;
    bool isChangeBg2 = false;

    private GameObject backgroud1;
    private GameObject backgroud2;

    public TextMeshProUGUI talkText;

    

    public void Start()
    // 최초 시스템 파일을 정의합니다.
    {
        //UNovelCommand_List.Add("안녕 여기는 한국이야");
        //print(UNovelCommand_List[0]);
        ReadTxt("Assets/Resource/text/testfile.txt");
    }
    
    string ReadTxt(string filePath)
    // 외부 txt 파일을 불러옵니다. (외부의 인원이 편집을 편하게 하기 위해 이 시스템을 사용했습니다.)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";
        if (fileInfo.Exists)
        {
            
            StreamReader reader = new StreamReader(filePath, Encoding.UTF8);
            while ( (value = reader.ReadLine()) != null )
            {
                UNovelCommand_List.Add(value);
                
            }
            
            reader.Close();
        }

        else
            value = "no have fileData";

        return value;
    }

    public void ButtonPressed() {
     // 버튼 클릭시 일어나는 이벤트들을 정의합니다.
        try
        {
            if (buttonclick < UNovelCommand_List.Count)
            {
                Debug.Log(FindCommander(UNovelCommand_List[buttonclick]));
                buttonclick++;
            }
            else
            {
                buttonclick = UNovelCommand_List.Count;
            }
            
        }
        
        catch (System.ArgumentOutOfRangeException)
        { 
        }
        
    }

    

    string FindCommander(string text)
    // txt 커맨드를 정의하고 찾고 명령어를 실행시킵니다.
    {
        string[] words = text.Split(':');
        string ascii = asciiencoding();
        if (words[0] == "st")
        { 
            print("log");
            //talkText.text = words[1];
            StartCoroutine(_typing());
            return words[1];
        }
        else if (words[0] == "t")
        { 
            print("text");
            //talkText.text = words[1];
            StartCoroutine(_typing());
            return words[1];
        }
        else if (words[0] == "i")
        { 
            print("int");
            //talkText.text = words[1];
            StartCoroutine(_typing());
            return words[1];
        }
        else if (words[0] == "BGChange")
        {
            words[1] = words[1].Replace(" ", string.Empty);
            string[] backgroundSplit = words[1].Split(",");
            print(backgroundSplit[0]);
            print(backgroundSplit[1]);
            BackGroundChanger(backgroundSplit[0], backgroundSplit[1]);
            return " ";
        }
        else
        {
            return text;
        }

        IEnumerator _typing()
        {
            yield return new WaitForSeconds(2f);
            for (int i = 0; i <= words[1].Length; i++)
            {
                talkText.text = words[1].Substring(0, i);

                yield return new WaitForSeconds(0.15f);
            }
        }

        string asciiencoding()
        {
            string unicodeString = words[1];

            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array.
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            // Display the strings created before and after the conversion.
            //Console.WriteLine("Original string: {0}", unicodeString);
            Console.WriteLine("Ascii converted string: {0}", asciiString);
            return asciiString;
        }
        
    }

    public void BackGroundChanger(string IMG_FILE1, string IMG_FILE2)
     // 장면의 배경을 바꿔줍니다.
    {
        
        backgroud1 = GameObject.Find(IMG_FILE1);
        backgroud2 = GameObject.Find(IMG_FILE2);
        if (isChangeBg2 == false)
        {
            backgroud1.SetActive(false);
            backgroud2.SetActive(true);
            isChangeBg2 = true;
        }
        else if (isChangeBg2 == true)
        {
            backgroud1.SetActive(true);
            backgroud2.SetActive(false);
            isChangeBg2 = false;
        }
        
    }

    
}


