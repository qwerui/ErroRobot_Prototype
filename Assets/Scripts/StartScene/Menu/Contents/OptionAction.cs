using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StartMenu
{
    public class OptionAction : StartMenuListOption
    {
        Animator anim;
        OptionMenuManager optionManager;

        const string open = "OpenOption";
        const string canvasTag = "UICanvas";

        private void Start() 
        {
            var canvas = GameObject.FindGameObjectWithTag(canvasTag);
            canvas?.TryGetComponent<Animator>(out anim);
            optionManager = GameObject.FindObjectOfType<OptionMenuManager>();
        }

        public override void Execute()
        {
            anim.SetTrigger(open);
            optionManager.OpenOption();
        }
    }

}
