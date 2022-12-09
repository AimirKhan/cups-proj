using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupsElements : MonoBehaviour
{
    public CupsApplication app { get { return GameObject.FindObjectOfType<CupsApplication>(); } }
}

public class CupsApplication : MonoBehaviour
{
    public CupsModel model;
    public CupsView view;
    public CupsController controller;


    // Init here
    void Start()
    {
        
    }
}
