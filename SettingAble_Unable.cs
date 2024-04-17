using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingAble_Unable : MonoBehaviour
{
    [SerializeField]
    private GameObject SetObject;
    
    private bool isAble = false;
    public void Able()
    {
        if (isAble != true)
        {
            SetObject.SetActive(true);
            isAble = true;
        }
        
    }
    public void Unable()
    {
        if (isAble != false)
        {
            SetObject.SetActive(false);
            isAble = false;
        }
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
