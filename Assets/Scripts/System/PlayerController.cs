using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //컨트롤러 종류에 따른 스크립트
    IControllerPlatform controllerPlatform;
    //컨트롤러는 스택으로 관리
    //Top이 현재 사용중인 컨트롤러
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //키보드 입력 시 키보드 컨트롤러로 전환
        if(!(controllerPlatform is KeyboardController) && Input.anyKeyDown)
        {
            controllerPlatform = new KeyboardController();
        }
        if(controllerStack.Count > 0)
        {
            controllerPlatform?.Execute(controllerStack.Peek());
        }
    }

    public void AddController(IControllerBase controller)
    {
        controllerStack.Push(controller);
    }

    public void ClearController()
    {
        controllerStack.Clear();
    }
}
