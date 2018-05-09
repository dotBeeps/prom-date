using Newtonsoft.Json;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace PromDate.Installer
{
    public class Installer
    {
        private static List<Release> releases = new List<Release>();

        public static async System.Threading.Tasks.Task<List<Release>> GetReleasesAsync()
        {
            var client = new GitHubClient(new ProductHeaderValue("prom-date-installer"));
            releases = (await client.Repository.Release.GetAll("FoolishDave", "prom-date")).ToList();
            return releases;
        }

        public static void InstallPromDate(string path, Release downloadVersion)
        {
            UninstallPromDate(path);

            DirectoryInfo monsterPromDir = new DirectoryInfo(path);
            monsterPromDir.CreateSubdirectory("Installer");
            WebClient webClient = new WebClient();
            webClient.Headers["user-agent"] = "Mozilla / 5.0(Windows NT 6.1; WOW64; rv: 40.0) Gecko / 20100101 Firefox / 40.1";
            string downloadUrl = downloadVersion.Assets.First(asset => asset.Name == "PromDate.dll").BrowserDownloadUrl;
            webClient.DownloadFile(new Uri(downloadUrl), monsterPromDir + "/Installer/PromDate.dll");

            string managedPath = monsterPromDir + "/MonsterProm_Data/Managed";
            File.Copy(managedPath + "/Assembly-CSharp.dll", managedPath + "/Assembly-CSharp.dll.backup");
            Injector.Injector.Inject(managedPath + "/Assembly-CSharp.dll");
            if (!Directory.Exists(monsterPromDir + "/MonsterProm_Data/Mods"))
            {
                Directory.CreateDirectory(monsterPromDir + "/MonsterProm_Data/Mods");
            }
            File.Copy(monsterPromDir + "/Installer/PromDate.dll", monsterPromDir + "/MonsterProm_Data/Mods/PromDate.dll");

            Directory.Delete(monsterPromDir.FullName + "/Installer", true);
        }

        public static void UninstallPromDate(string path)
        {
            DirectoryInfo monsterPromDir = new DirectoryInfo(path);
            string assemPath = monsterPromDir + "/MonsterProm_Data/Managed/Assembly-CSharp.dll";
            string firstPassPath = monsterPromDir + "/MonsterProm_Data/Managed/Assembly-CSharp-firstpass.dll";
            if (File.Exists(assemPath + ".backup"))
            {
                File.Delete(assemPath);
                File.Move(assemPath + ".backup", assemPath);
            }
            if (File.Exists(firstPassPath + ".backup"))
            {
                File.Delete(firstPassPath);
                File.Move(firstPassPath + ".backup", firstPassPath);
            }
            if (File.Exists(monsterPromDir + "/MonsterProm_Data/Mods/PromDate.dll"))
                File.Delete(monsterPromDir + "/MonsterProm_Data/Mods/PromDate.dll");

        }
    }
}
