using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerPlatform
{
    void Reset();
    void Execute(IControllerBase controller);
}