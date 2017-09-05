using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public GameObject play;
    public GameObject howto;
    public GameObject settings;
    public GameObject exit;
    public GameObject sinlge;
    public GameObject two;
    public GameObject load;
    public GameObject start;
    public GameObject join;
    public GameObject create;
    public GameObject back;
    public Transform ht, s, lg, cg, jg;

    public void Start()
    {
        ht.GetComponent<Button>().interactable = false;
        s.GetComponent<Button>().interactable = false;
        lg.GetComponent<Button>().interactable = false;
        cg.GetComponent<Button>().interactable = false;
        jg.GetComponent<Button>().interactable = false;
    }

    public void Playbtn()
    {
         play.SetActive(false);
         howto.SetActive(false);
         settings.SetActive(false);
         exit.SetActive(false);
         sinlge.SetActive(true);
         two.SetActive(true);
         back.SetActive(true);
    }
    //Not for prototype
    public void HowTobtn()
    {

    }
    //Not for prototype
    public void Settingsbtn()
    {

    }

    public void Exitbtn()
    {
        Application.Quit();
    }

    public void SinglePlayerbtn ()
    {
        sinlge.SetActive(false);
        two.SetActive(false);
        load.SetActive(true);
        start.SetActive(true);
    }

    public void Newbtn()
    {
        SceneManager.LoadScene("Prototype");
    }
    //Not for prototype
    public void Loadbtn()
    {

    }
    //Not for prototype
    public void TwoPlayerbtn()
    {
        sinlge.SetActive(false);
        two.SetActive(false);
        join.SetActive(true);
        create.SetActive(true);
    }
    //Not for prototype
    public void Createbtn()
    {

    }
    //Not for prototype
    public void Joinbtn()
    {

    }

    public void Backbtn()
    {
        if(sinlge.activeSelf)
        {
            play.SetActive(true);
            howto.SetActive(true);
            settings.SetActive(true);
            exit.SetActive(true);
            sinlge.SetActive(false);
            two.SetActive(false);
            back.SetActive(false);
        }
        if(join.activeSelf || start.activeSelf)
        {
            sinlge.SetActive(true);
            two.SetActive(true);
            join.SetActive(false);
            create.SetActive(false);
            load.SetActive(false);
            start.SetActive(false);
        }
    }

}
