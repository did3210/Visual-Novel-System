using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodeLagauge : MonoBehaviour
{
    public Dictionary<string, int> VAR = new Dictionary<string, int>();

    private void Awake()
    {
        VAR.Add("ver", 10);
    }


    public void CL_COMMAND(string command)
    {
        CL_VAR_SET(command);
        CL_IF(command);
    }


    public void CL_IF(string S1command_temp)
    {
        

        if (S1command_temp.Contains("if"))
        {
            string remove_commandword;
            string[] temp = null;
            string[] temp2 = null;
            

            remove_commandword = S1command_temp.Replace("if", "").Trim();
            temp = remove_commandword.Split(":");
            //print(temp[0]);
            if (temp[0].Contains("bigger"))
            {
                print(100);
                temp2 = temp[0].Split("bigger");
                temp2[0] = temp2[0].Trim();
                temp2[1] = temp2[1].Trim();
                if (VAR[temp2[0]] < Int32.Parse(temp2[1]))
                {
                    CL_VAR_SET(temp[1]);
                    print("bigger");
                }
            }
            else if (temp[0].Contains("lower"))
            {
                print(101);
                temp2 = temp[0].Split("lower");
                temp2[0] = temp2[0].Trim();
                temp2[1] = temp2[1].Trim();
                if (VAR[temp2[0]] > Int32.Parse(temp2[1]))
                {
                    CL_VAR_SET(temp[1]);
                    print("lower");
                }
            }
            else if (temp[0].Contains("equal"))
            {
                //print(102);
                temp2 = temp[0].Split("equal");
                temp2[0] = temp2[0].Trim();
                temp2[1] = temp2[1].Trim();
                
                if (VAR[temp2[0]] == Int32.Parse(temp2[1]))
                {
                    CL_VAR_SET(temp[1]);
                    //print("equal");
                }
            }
            
        }

    }

    public void CL_VAR_SET(string COMMAND)
    {
        
        string[] temp;
        try
        {
            foreach (var name in VAR)
            {
                string key = name.Key;
                //print(key);
                if (VAR.ContainsKey(key) && !COMMAND.Contains("if"))
                {
                    temp = COMMAND.Split("=");
                    temp[0] = temp[0].Trim(); temp[1] = temp[1].Trim();
                    //print(temp[0]);
                    VAR.Remove(temp[0]);
                    VAR.Add(temp[0], Int32.Parse(temp[1]));
                    //VAR[temp[0]] = Int32.Parse(temp[1]);
                    //print(VAR[temp[0]]);
                }
                else if (!VAR.ContainsKey(key) && !COMMAND.Contains("if"))
                {
                    temp = COMMAND.Split("=");
                    temp[0] = temp[0].Trim(); temp[1] = temp[1].Trim();
                    //print(temp[0]);
                    VAR.Add(temp[0], Int32.Parse(temp[1]));
                    //print(VAR[temp[0]]);
                }
            }
        }
        catch(System.InvalidOperationException) { } 
        
        
    }
}
