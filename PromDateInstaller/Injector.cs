using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace PromDate.Injector
{
    public static class Injector
    {
        public static void Inject(string path)
        {
            using (var unityGame = ModuleDefinition.ReadModule(path, new ReaderParameters { ReadWrite = true }))
            {
                TypeDefinition generalManager = unityGame.GetType("", "GeneralManager");
                if (generalManager == null)
                    throw new Exception("Could not find GeneralManager! Did you provide the correct path?");

                MethodDefinition managerStart = generalManager.Methods.First(meth => meth.Name == "Start");
                if (managerStart == null)
                    throw new Exception("Could not find GeneralManager.Start method!");

                var processor = managerStart.Body.GetILProcessor();
                var instructions = processor.Body.Instructions;
                instructions.Insert(0, processor.Create(OpCodes.Ldarg_0));
                instructions.Insert(1, processor.Create(OpCodes.Call, Util.ImportMethod<Component>(unityGame, "get_gameObject")));
                instructions.Insert(2, processor.Create(OpCodes.Call, Util.ImportMethod<Application>(unityGame, "get_dataPath")));
                instructions.Insert(3, processor.Create(OpCodes.Ldstr, "/Mods/PromDate.dll"));
                instructions.Insert(4, processor.Create(OpCodes.Call, Util.ImportMethod<string>(unityGame, "Concat", typeof(string), typeof(string))));
                instructions.Insert(5, processor.Create(OpCodes.Call, Util.ImportMethod<Assembly>(unityGame, "LoadFrom", typeof(string))));
                instructions.Insert(6, processor.Create(OpCodes.Ldstr, "Modloader"));
                instructions.Insert(7, processor.Create(OpCodes.Callvirt, Util.ImportMethod<Assembly>(unityGame, "GetType", typeof(string))));
                instructions.Insert(8, processor.Create(OpCodes.Callvirt, Util.ImportMethod<GameObject>(unityGame, "AddComponent", typeof(Type))));
                instructions.Insert(9, processor.Create(OpCodes.Pop));

                Console.WriteLine("Writing instructions...");
                unityGame.Write();
            }
        }
    }

    public static class Util
    {
        public static MethodReference ImportMethod<T>(ModuleDefinition module, string name)
        {
            return module.ImportReference(typeof(T).GetMethod(name, Type.EmptyTypes));
        }

        public static MethodReference ImportMethod<T>(ModuleDefinition module, string name, params Type[] types)
        {
            return module.ImportReference(typeof(T).GetMethod(name, types));
        }

        public static MethodReference ImportMethod(ModuleDefinition module, string type, string method, params Type[] types)
        {
            TypeReference reference = module.Types.First(t => t.Name == type);
            return module.ImportReference(reference.Resolve().Methods.First(m => m.Name == method));
        }
    }
}
