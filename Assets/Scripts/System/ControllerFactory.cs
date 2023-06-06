using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFactory
{
    ///<summary>
    ///컨트롤러 생성
    ///</summary>
    public static T CreateController<T>() where T : IControllerBase, new()
    {
        return new T();
    }
}
