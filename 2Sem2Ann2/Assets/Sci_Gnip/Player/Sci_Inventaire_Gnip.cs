using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sci_Inventaire_Gnip : MonoBehaviour
{
    public enum Balise_2
    {
        Vide,
        Regen,
        Atk,
        Bouc
    }
    public Balise_2 Balise_2_State = Balise_2.Vide;
    public enum Balise_3
    {
        Vide,
        Regen,
        Atk,
        Bouc
    }
    public Balise_3 Balise_3_State = Balise_3.Vide;

    public Sprite Balise_Regen;
    public Sprite Balise_Atk;
    public Sprite Balise_Bouc;

    public SpriteRenderer Inv_Balise_2;
    public SpriteRenderer Inv_Balise_3;


    // Update is called once per frame
    void Update()
    {
        switch (Balise_2_State)
        {
            case Balise_2.Vide:
                Inv_Balise_2.sprite = null;
                break;
            case Balise_2.Regen:
                Inv_Balise_2.sprite = Balise_Regen;
                break;
            case Balise_2.Atk:
                Inv_Balise_2.sprite = Balise_Atk;
                break;
            case Balise_2.Bouc:
                Inv_Balise_2.sprite = Balise_Bouc;
                break;
        }
        switch (Balise_3_State)
        {
            case Balise_3.Vide:
                Inv_Balise_3.sprite = null;
                break;
            case Balise_3.Regen:
                Inv_Balise_3.sprite = Balise_Regen;
                break;
            case Balise_3.Atk:
                Inv_Balise_3.sprite = Balise_Atk;
                break;
            case Balise_3.Bouc:
                Inv_Balise_3.sprite = Balise_Bouc;
                break;
        }
    }
}
