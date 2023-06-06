using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFactory
{
    public static T CreateController<T>() where T : IControllerBase, new()
    {
        return new T();
    }
}
