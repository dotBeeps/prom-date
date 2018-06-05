using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AutoSkip
{
    public class AutoSkipMod : IMod
    {
        public string Name { get { return "AutoSkip"; } }

        public string Author { get { return "FoolishDave"; } }

        public Version Version { get; } = new Version(0, 0, 1);

        public void ApplicationQuit()
        {
            
        }

        public void Awake()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void LateUpdate()
        {
            
        }

        public void OnDisable()
        {
            
        }

        public void OnEnable()
        {
            
        }

        public void SceneLoaded(UnityEngine.SceneManagement.Scene loaded)
        {
            
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            if (Input.GetKey("k"))
            {
                EventManager.Instance.AdvanceStepCurrentEvent(false);
            }
        }
    }
}
