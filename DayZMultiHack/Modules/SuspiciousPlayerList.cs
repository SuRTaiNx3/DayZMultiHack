using ExternalD3D11;
using ExternalD3D11.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayZMultiHack.Modules
{
    public class SuspiciousPlayerList : BaseModule
    {
        #region Globals

        // Base
        private const string fileName = "suspiciousPlayers.bin";
        public List<string> SuspiciousPlayers;


        // Commands
        private ConsoleCommand removePlayerCommand;
        private ConsoleCommand addPlayerCommand;

        #endregion

        #region Constructor

        public SuspiciousPlayerList(Main main, UI ui, Menu menu)
            : base(main, ui, menu)
        {
            SuspiciousPlayers = LoadListFromFile();
            InitializeCommands();
        }

        #endregion

        #region Methods

        #region Commands

        private void InitializeCommands()
        {
            DirectXUI.Console.Commands.Add(new ConsoleCommand("listsusplayer", new ConsoleCommand.OnCommandEventHandler(ListPlayersCommandExecuted), "Lists all players in the suspicious list.", "listsusplayer"));
            DirectXUI.Console.Commands.Add(new ConsoleCommand("clearsusplayer", new ConsoleCommand.OnCommandEventHandler(ClearPlayerCommandExcuted), "Clears the suspicious list.", "clearsusplayer"));

            addPlayerCommand = new ConsoleCommand();
            addPlayerCommand.Command = "addsusplayer";
            addPlayerCommand.Description = "Adds the given player to the suspicious list (not case sensetive).";
            addPlayerCommand.Syntax = "addsusplayer <name>";
            addPlayerCommand.Receiver = new ConsoleCommand.OnCommandEventHandler(AddPlayerCommandExecuted);
            DirectXUI.Console.Commands.Add(addPlayerCommand);

            removePlayerCommand = new ConsoleCommand();
            removePlayerCommand.Command = "removesusplayer";
            removePlayerCommand.Description = "Removes a player from the suspicious list (use lower case).";
            removePlayerCommand.Syntax = "removesusplayer <name>";
            removePlayerCommand.Receiver = new ConsoleCommand.OnCommandEventHandler(RemovePlayerCommandExecuted);
            removePlayerCommand.AutoCompleteCollection = SuspiciousPlayers;
            DirectXUI.Console.Commands.Add(removePlayerCommand);
        }

        private void ListPlayersCommandExecuted(ConsoleCommandArgs e)
        {
            string text = "Player currently in list:\n";
            foreach (string player in SuspiciousPlayers)
                text += "-" + player + "\n";

            e.Callback(text);
        }

        private void AddPlayerCommandExecuted(ConsoleCommandArgs e)
        {
            if (e.CommandData.Length != 1)
            {
                e.Callback("Syntax: " + e.Command.Syntax);
            }
            else
            {
                if (SuspiciousPlayers.Contains(e.CommandData[0].ToLower()))
                {
                    e.Callback(string.Format("Player '{0}' is already in the list!", e.CommandData[0]));
                }
                else
                {
                    SuspiciousPlayers.Add(e.CommandData[0].ToLower());
                    SaveListToFile();
                    e.Callback(string.Format("Player '{0}' was successfully added!", e.CommandData[0]));
                }
            }

            removePlayerCommand.AutoCompleteCollection = SuspiciousPlayers;
        }

        private void RemovePlayerCommandExecuted(ConsoleCommandArgs e)
        {
            if (e.CommandData.Length != 1)
            {
                e.Callback("Syntax: " + e.Command.Syntax);
            }
            else
            {
                SuspiciousPlayers.Remove(e.CommandData[0]);
                SaveListToFile();
                e.Callback(string.Format("Player '{0}' was successfully removed!", e.CommandData[0]));
            }
        }

        private void ClearPlayerCommandExcuted(ConsoleCommandArgs e)
        {
            SuspiciousPlayers.Clear();
            SaveListToFile();
            e.Callback("Suspicouis player list was successfully cleared!");
        }

        #endregion

        public void Update()
        {
            addPlayerCommand.AutoCompleteCollection = MainModule.ScoreboardTable.Select(p => p.Value).ToList();
        }

        private void SaveListToFile()
        {
            using (Stream stream = File.Open(fileName, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, SuspiciousPlayers);
            }
        }

        private List<string> LoadListFromFile()
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                return new List<string>();
            }

            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (List<string>)bformatter.Deserialize(stream);
            }
        }

        #endregion
    }
}
