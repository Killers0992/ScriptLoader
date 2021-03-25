using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using SplitAndMerge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptLoader
{
    public class MainClass : Plugin<PluginConfig>
    {
        public override string Author { get; } = "Killers0992";
        public override string Name { get; } = "ScriptLoader";
        public override string Prefix { get; } = "scriptloader";
        public override Version RequiredExiledVersion { get; } = new Version(2, 8, 0);
        public override Version Version { get; } = new Version(1, 0, 0);
        public List<CoroutineHandle> scripts = new List<CoroutineHandle>();

        public string pluginDir;

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) return;
            base.OnEnabled();
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pluginDir = Path.Combine(folderPath, "EXILED", "Plugins", "ScriptLoader");
            if (!Directory.Exists(pluginDir))
                Directory.CreateDirectory(pluginDir);
            if (!Directory.Exists(Path.Combine(pluginDir, "scripts")))
                Directory.CreateDirectory(Path.Combine(pluginDir, "scripts"));
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += Server_SendingRemoteAdminCommand;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= Server_SendingRemoteAdminCommand;
            foreach (var script in scripts)
            {
                Timing.KillCoroutines(script);
            }
            scripts.Clear();
        }

        private void Server_SendingRemoteAdminCommand(Exiled.Events.EventArgs.SendingRemoteAdminCommandEventArgs ev)
        {
            switch (ev.Name.ToUpper())
            {
                case "SCRIPTLOADER":
                case "SL":
                    ev.IsAllowed = false;
                    if (!ev.Sender.CheckPermission("scriptloader") && !ev.CommandSender.FullPermissions)
                    {
                        ev.Sender.RemoteAdminMessage("No Permission", false, "ScriptLoader");
                        return;
                    }
                    if (ev.Arguments.Count == 0)
                    {
                        ev.Sender.RemoteAdminMessage(string.Concat("Commands: ",
                            Environment.NewLine,
                            " - scriptloader run <scriptname> "), false, "ScriptLoader");
                        return;
                    }
                    switch (ev.Arguments[0].ToUpper())
                    {
                        case "RUN":
                            string path = Path.Combine(pluginDir, "scripts", ev.Arguments[1] + ".cscs");
                            if (File.Exists(path))
                            {
                                scripts.Add(Timing.RunCoroutine(LoadScript(path)));
                                return;
                            }
                            ev.Sender.RemoteAdminMessage("File not found", true, "ScriptLoader");                           
                            break;
                    }
                    break;
            }
        }

        public IEnumerator<float> LoadScript(string script)
        {
            ProcessScript(script);
            yield break;
        }


        private static void ProcessScript(string filename)
        {
            string flName = Path.GetFileNameWithoutExtension(filename);
            Log.Info($"Execute script {flName}.cscs...");
            Variable result = null;
            try
            {
                result = System.Threading.Tasks.Task.Run(() =>
                Interpreter.Instance.ProcessFile(filename, true)).Result;
            }
            catch (Exception exc)
            {
                Log.Error($"Script {flName}.cscs executed with error, {Environment.NewLine}{exc.ToString()}");
                return;
            }
            Log.Info($"Script {flName}.cscs executed");
        }
    }
}
