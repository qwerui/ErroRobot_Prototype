using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueContainer
{

    public string speaker;
    public string text;
    public string condition = null;
    public string wait_until = null;

}
