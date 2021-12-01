using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public string buttonText;
    public string type;

    public List<string> vars;
    public List<int> varMods;

    //public Interaction(string bT, string t, string v1, int vM1, string v2 = "", int vM2 = 0)
    public Interaction(string bT, string t)
    {

        //Text on button for interaction (specifies subtype), type of interaction
        buttonText = bT;
        type = t;

        //Legacy \/
        /*
        //Variable to be modified, if the modifier is negative or positive, allows for two modifications
        vars = new List<string>();
        varMods = new List<int>(); 

        vars.Add(v1);
        varMods.Add(vM1);

        if (v2 != "")
        {

            vars.Add(v2);
            varMods.Add(vM2);

        }

        vars.Add("default");
        */

    }

}
