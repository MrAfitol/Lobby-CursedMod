﻿namespace Lobby_CursedMod
{
    using Lobby_CursedMod.API.Enums;
    using PlayerRoles;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Config
    {
        [Description("Main text ({seconds} - Either it shows how much is left until the start, or the server status is \"Server is suspended\", \"Round starting\")")]
        public string TitleText { get; set; } = "<size=50><color=#F0FF00><b>Waiting for players, {seconds}</b></color></size>";

        [Description("Text showing the number of players ({players} - Text with the number of players)")]
        public string PlayerCountText { get; set; } = "<size=40><color=#FFA600><i>{players}</i></color></size>";

        [Description("What will be written if the lobby is locked?")]
        public string ServerPauseText { get; set; } = "Server is suspended";

        [Description("What will be written when there is a second left?")]
        public string SecondLeftText { get; set; } = "{seconds} second left";

        [Description("What will be written when there is more than a second left?")]
        public string SecondsLeftText { get; set; } = "{seconds} seconds left";

        [Description("What will be written when the round starts?")]
        public string RoundStartText { get; set; } = "Round starting";

        [Description("What will be written when there is only one player on the server?")]
        public string PlayerJoinText { get; set; } = "player joined";

        [Description("What will be written when there is more than one player on the server?")]
        public string PlayersJoinText { get; set; } = "players joined";

        [Description("What is the movement boost intensity?")]
        public byte MovementBoostIntensity { get; set; } = 50;

        [Description("What role will people play in the lobby?")]
        public RoleTypeId LobbyPlayerRole { get; set; } = RoleTypeId.Tutorial;

        [Description("Allow people to talk over the intercom?")]
        public bool AllowIcom { get; set; } = true;

        [Description("Display text on Intercom? (Works only when lobby Intercom type)")]
        public bool DisplayInIcom { get; set; } = true;

        [Description("What size will the text be in the Intercom? (The larger the value, the smaller it will be)")]
        public int IcomTextSize { get; set; } = 20;

        [Description("What items will be given when spawning a player in the lobby? (Leave blank to keep inventory empty)")]
        public List<ItemType> LobbyInventory { get; set; } = new List<ItemType>()
        {
            ItemType.Coin
        };

        [Description("In what locations can people spawn? (If it is less than 1, a random one will be selected)")]
        public List<LobbyLocationType> LobbyLocation { get; set; } = new List<LobbyLocationType>()
        {
            LobbyLocationType.Tower,
            LobbyLocationType.Intercom,
            LobbyLocationType.GR18,
            LobbyLocationType.SCP173
        };
    }
}
