using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace PromDate.Mod
{
    class CombinationMod : IMod
    {
        IEnumerable<IMod> mods;

        public CombinationMod(IEnumerable<IMod> mods)
        {
            this.mods = mods;
        }

        private delegate void CallDelegate(IMod mod);

        public string Name => throw new NotImplementedException();

        public string Author => throw new NotImplementedException();

        public Version Version => throw new NotImplementedException();

        public void Start()
        {
            Call(mod => mod.Start());
        }

        public void ApplicationQuit()
        {
            Call(mod => mod.ApplicationQuit());
        }

        public void Awake()
        {
            Call(mod => mod.Awake());
        }

        public void FixedUpdate()
        {
            Call(mod => mod.FixedUpdate());
        }

        public void LateUpdate()
        {
            Call(mod => mod.LateUpdate());
        }

        public void SceneLoaded(Scene loaded)
        {
            Call(mod => mod.SceneLoaded(loaded));
        }

        public void Update()
        {
            Call(mod => mod.Update());
        }

        private void Call(CallDelegate callDelegate)
        {
            foreach (IMod mod in mods)
            {
                try
                {
                    if (!ModManager.IsModDisabled(mod))
                        callDelegate(mod);
                } catch (Exception ex)
                {
                    GeneralManager.Instance.LogToFileOrConsole("[PromDate] " + mod.Name + " threw " + ex);
                }
            }
        }

        public void OnDisable()
        {
            throw new NotImplementedException();
        }

        public void OnEnable()
        {
            throw new NotImplementedException();
        }
    }
}
