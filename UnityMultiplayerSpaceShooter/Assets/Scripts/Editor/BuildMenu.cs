using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class BuildMenu : MonoBehaviour
    {
        private static readonly string[] Scenes =
        {
            "Assets/Scenes/CombatScene.unity"
        };

        [MenuItem("Build/Build All")]
        public static void BuildAll()
        {
            BuildWindowsServer();
            BuildLinuxServer();
            BuildWindowsClient();
        }

        [MenuItem("Build/Build Server (Windows)")]
        public static void BuildWindowsServer()
        {
            var buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = Scenes,
                locationPathName = "../builds/Windows/Server/SpaceShooterServer.exe",
                target = BuildTarget.StandaloneWindows,
                options = BuildOptions.CompressWithLz4 | BuildOptions.EnableHeadlessMode
            };

            Console.WriteLine("Building Server (Windows)");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Console.WriteLine("Completed Build Server (Windows)");
        }

        [MenuItem("Build/Build Server (Linux)")]
        public static void BuildLinuxServer()
        {
            var buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = Scenes,
                locationPathName = "../builds/Linux/Server/SpaceShooterServer.x86_64",
                target = BuildTarget.StandaloneLinux64,
                options = BuildOptions.CompressWithLz4 | BuildOptions.EnableHeadlessMode
            };

            Console.WriteLine("Building Server (Linux)");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Console.WriteLine("Completed Build Server (Linux)");
        }

        [MenuItem("Build/Build Client (Windows)")]
        public static void BuildWindowsClient()
        {
            var buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = Scenes,
                locationPathName = "../builds/Windows/Client/SpaceShooterClient.exe",
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.CompressWithLz4HC
            };

            Console.WriteLine("Building Client (Windows)...");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Console.WriteLine("Completed Build Client (Windows).");
        }
    }
}