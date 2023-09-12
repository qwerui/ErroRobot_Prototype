using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class ReturnMainOption : OptionsContent
    {
        Animator anim;
        OptionMenuController optionController;

        const string close = "CloseOption";
        const string canvasTag = "UICanvas";

        void Start()
        {
            var canvas = GameObject.FindGameObjectWithTag(canvasTag);
            canvas.TryGetComponent<Animator>(out anim);
            optionController = GameObject.FindObjectOfType<OptionMenuController>(true);
        }

        public override void Execute()
        {
            anim.SetTrigger(close);
            optionController.gameObject.SetActive(false);
        }
    }

}
