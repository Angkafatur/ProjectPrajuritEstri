using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTabs : MonoBehaviour
{
    public GameObject B_Suara;
    public GameObject B_Kontrol;

    public GameObject Tab_Suara;
    public GameObject Tab_Kontrol;

    public void HideALLTabs()
    {
        Tab_Suara.SetActive(false);
        Tab_Kontrol.SetActive(false);
    }

    public void ShowTab1()
    {
        HideALLTabs();
        Tab_Suara.SetActive(true);
    }public void ShowTab2()
    {
        HideALLTabs();
        Tab_Kontrol.SetActive(true);
    }
}
