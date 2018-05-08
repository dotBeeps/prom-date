using Mono.Cecil;
using System;
using System.IO;

namespace PromDate.Injector
{
    class Program
    {
        static void Main(string[] args)
        {
            string installLocation;
            if (args.Length == 0)
            {
                Console.WriteLine("TIP: You can drag your MonsterProm.exe onto this program to automatically inject.");
                Console.WriteLine("MonsterProm.exe location:");
                installLocation = Console.ReadLine();
            } else
            {
                installLocation = args[0];
            }
            string assemblyPath = Path.GetDirectoryName(installLocation) + @"\MonsterProm_Data\Managed\Assembly-CSharp.dll";
            string backupPath = Path.GetDirectoryName(installLocation) + @"\MonsterProm_Data\Managed\Assembly-CSharp.dll.backup";
            System.IO.File.Copy(assemblyPath, backupPath);
            
            Console.WriteLine("Patching Assembly-CSharp.dll at path " + assemblyPath);
            Injector.Inject(assemblyPath);
        }
    }
}
