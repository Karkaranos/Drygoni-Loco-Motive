using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DIalogueMessage
{
    public string Text;
    public Constants.Names Names;
    public List<Choice> Branch;
}
