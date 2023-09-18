using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StartMenu
{
    public class LoadGameAction : StartMenuListOption
    {
        protected override void Awake() 
        {
            base.Awake();
            
            //불러오기 활성화
            var saveData = JSONParser.ReadJSON<SaveData>($"{Application.persistentDataPath}/SaveData.json");
            if(saveData == null)
            {
                gameObject.SetActive(false);
                
            }
            else if(!saveData.isLoadable)
            {
                gameObject.SetActive(false);   
            }
        }

        public override void Execute()
        {
            GameManager.instance.isLoadedGame = true;
            LoadingSceneManager.LoadScene("GameScene");
        }
    }
}

