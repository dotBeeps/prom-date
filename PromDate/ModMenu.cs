using PromDate.EventLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PromDate
{
    class ModMenu : MonoBehaviour
    {
        GameObject ModMenuButton;

        void Start()
        {
            var settingsButton = GameObject.Find("SettingsLabel");
            ModMenuButton = Instantiate(settingsButton, settingsButton.transform.parent);
            ModMenuButton.name = "ModsLabel";
            Destroy(ModMenuButton.GetComponent<UIButton>());
            ModMenuButton.GetComponent<UI2DSprite>().sprite2D = SpriteHelper.LoadSpriteFromFile(Application.dataPath + "/../PromDate/UI/ModsButton.png", "ModsLabel");
            ModMenuButton.AddComponent<UIButton>();
            ModMenuButton.transform.position = new Vector3(1.2f, -0.8f, 0f);
        }

        void Update()
        {

        }
    }
}
