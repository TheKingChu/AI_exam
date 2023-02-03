using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingHuman : MonoBehaviour
{
    public Button button;
    public Human0 h0;
    public Human1 h1;
    public Human2 h2;
    public Human3 h3;

    public void Button()
    {
        h0.isH0 = true;
        h1.isH1 = false;
        h2.isH2 = false;
        h3.isH3 = false;
    }

    public void Button1()
    {
        h1.isH1 = true;
        h2.isH2 = false;
        h3.isH3 = false;
        h0.isH0 = false;
    }

    public void Button2()
    {
        h2.isH2= true;
        h3.isH3 = false;
        h0.isH0 = false;
        h1.isH1 = false;
    }

    public void Button3()
    {
        h3.isH3= true;
        h0.isH0 = false;
        h1.isH1 = false;
        h2.isH2 = false;
    }
}
