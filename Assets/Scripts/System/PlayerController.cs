using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance = null;
    public static PlayerController instance
    {
        get
        {
            if(_instance==null)
            {
                return null;
            }
            return _instance;
        }
    }

    List<IControllerPlatform> controllerPlatforms = new List<IControllerPlatform>();
    Stack<IControllerBase> controllerStack = new Stack<IControllerBase>();

    public static void Init()
    {
        if(_instance==null)
        {
            var controllerPrefab = Resources.Load<PlayerController>("System/PlayerController");
            _instance = Instantiate<PlayerController>(controllerPrefab);
        }
    }

    private void Awake() 
    {
        if(_instance == null)
        {
            _instance = this;
            controllerPlatforms.Add(new KeyboardController());
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(controllerStack.Count > 0)
        {
            foreach(var controllerPlatform in controllerPlatforms)
            {
                controllerPlatform.Execute(controllerStack.Peek());
            }
        }
    }

    public void AddController(IControllerBase controller)
    {
        controllerStack.Push(controller);
    }

    public void DeleteController(IControllerBase controller)
    {
        if(controllerStack.Count > 0)
        {
            if(controllerStack.Peek() == controller)
            {
                controllerStack.Pop();
            }
        }
    }

    public void ClearController()
    {
        controllerStack.Clear();
    }

    public void ResetPlatform()
    {
        foreach(var controllerPlatform in controllerPlatforms)
        {
            controllerPlatform.Reset();
        }
    }
}
