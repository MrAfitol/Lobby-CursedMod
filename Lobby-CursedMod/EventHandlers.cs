namespace Lobby_CursedMod
{
    using CustomPlayerEffects;
    using Lobby_CursedMod.API.Enums;
    using Lobby_CursedMod.API;
    using MEC;
    using PlayerRoles;
    using PlayerRoles.Voice;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using CursedMod.Events.Arguments.Player;
    using CursedMod.Features.Logger;
    using CursedMod.Features.Wrappers.Player;
    using CursedMod.Events.Arguments.Facility.Doors;
    using CursedMod.Events.Arguments.Items;
    using CursedMod.Features.Wrappers.Round;
    using MapGeneration;

    public class EventHandlers
    {
        private CoroutineHandle lobbyTimer;

        private string text;

        private LobbyLocationType curLobbyLocationType;

        public void OnWaitingForPlayers()
        {
            try
            {
                LobbyLocationHandler.Point = new GameObject("LobbyPoint");
                SpawnManager();

                Timing.CallDelayed(0.1f, () => {
                    GameObject.Find("StartRound").transform.localScale = Vector3.zero;

                    if (lobbyTimer.IsRunning)
                    {
                        Timing.KillCoroutines(lobbyTimer);
                    }

                    if (curLobbyLocationType == LobbyLocationType.Intercom && Plugin.Instance.Config.DisplayInIcom) lobbyTimer = Timing.RunCoroutine(LobbyIcomTimer());
                    else lobbyTimer = Timing.RunCoroutine(LobbyTimer());
                });
            }
            catch (Exception e)
            {
                CursedLogger.LogError("[Lobby] [Event: OnWaitingForPlayers] " + e.ToString());
            }
        }

        public void OnPlayerJoined(PlayerJoinedEventArgs ev)
        {
            try
            {
                if (CursedRound.IsInLobby && (GameCore.RoundStart.singleton.NetworkTimer > 1 || GameCore.RoundStart.singleton.NetworkTimer == -2))
                {
                    Timing.CallDelayed(0.2f, () =>
                    {
                        ev.Player.SetRole(Plugin.Instance.Config.LobbyPlayerRole);

                        ev.Player.HasGodMode = true;

                        if (Plugin.Instance.Config.LobbyInventory.Count > 0)
                        {
                            foreach (var item in Plugin.Instance.Config.LobbyInventory)
                            {
                                ev.Player.AddItem(item);
                            }
                        }

                        Timing.CallDelayed(0.3f, () =>
                        {
                            ev.Player.Position = LobbyLocationHandler.Point.transform.position;
                            ev.Player.Rotation = LobbyLocationHandler.Point.transform.rotation.eulerAngles;

                            ev.Player.EnableEffect<MovementBoost>();
                            ev.Player.ChangeState<MovementBoost>(Plugin.Instance.Config.MovementBoostIntensity);
                        });
                    });
                }
            }
            catch (Exception e)
            {
                CursedLogger.LogError("[Lobby] [Event: OnPlayerJoin] " + e.ToString());
            }
        }

        public void SpawnManager()
        {
            try
            {
                if (Plugin.Instance.Config.LobbyLocation.Count <= 0)
                {
                    LobbyLocationHandler.TowerLocation();
                    return;
                }

                curLobbyLocationType = Plugin.Instance.Config.LobbyLocation.RandomItem();

                switch (curLobbyLocationType)
                {
                    case LobbyLocationType.Tower:
                        LobbyLocationHandler.TowerLocation();
                        break;
                    case LobbyLocationType.Intercom:
                        LobbyLocationHandler.IntercomLocation();
                        break;
                    case LobbyLocationType.GR18:
                        LobbyLocationHandler.GRLocation();
                        break;
                    case LobbyLocationType.SCP173:
                        LobbyLocationHandler.SCP173Location();
                        break;
                    default:
                        LobbyLocationHandler.TowerLocation();
                        break;
                }
            }
            catch (Exception e)
            {
                CursedLogger.LogError("[Lobby] [Method: SpawnManager] " + e.ToString());
            }
        }

        public void OnRoundStarted()
        {
            try
            {
                if (!string.IsNullOrEmpty(IntercomDisplay._singleton.Network_overrideText)) IntercomDisplay._singleton.Network_overrideText = "";

                foreach (var player in CursedPlayer.List)
                {
                    player.SetRole(RoleTypeId.Spectator);

                    Timing.CallDelayed(0.1f, () =>
                    {
                        player.HasGodMode = false;
                        player.DisableEffect<MovementBoost>();
                    });
                }
            }
            catch (Exception e)
            {
                CursedLogger.LogError("[Lobby] [Event: OnRoundStarted] " + e.ToString());
            }
        }

        public void OnPlayerInteractDoor(PlayerInteractingDoorEventArgs ev) { if (CursedRound.IsInLobby) ev.IsAllowed = false; }

        public void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs ev) { if (CursedRound.IsInLobby) ev.IsAllowed = false; }

        public void OnPlayerDroppedItem(PlayerDroppingItemEventArgs ev) { if (CursedRound.IsInLobby) ev.IsAllowed = false; }

        public void OnPlayerThrowingItem(PlayerThrowingItemEventArgs ev) { if (CursedRound.IsInLobby) ev.IsAllowed = false; }

        //public void OnPlayerUsingIntercom(Intercom) { if (IsLobby) ev.IsAllowed = false; }

        private IEnumerator<float> LobbyTimer()
        {
            while (CursedRound.IsInLobby)
            {
                text = string.Empty;

                text += Plugin.Instance.Config.TitleText;

                text += "\n" + Plugin.Instance.Config.PlayerCountText;

                short NetworkTimer = GameCore.RoundStart.singleton.NetworkTimer;

                switch (NetworkTimer)
                {
                    case -2: text = text.Replace("{seconds}", Plugin.Instance.Config.ServerPauseText); break;

                    case -1: text = text.Replace("{seconds}", Plugin.Instance.Config.RoundStartText); break;

                    case 1: text = text.Replace("{seconds}", Plugin.Instance.Config.SecondLeftText.Replace("{seconds}", NetworkTimer.ToString())); break;

                    case 0: text = text.Replace("{seconds}", Plugin.Instance.Config.RoundStartText); break;

                    default: text = text.Replace("{seconds}", Plugin.Instance.Config.SecondsLeftText.Replace("{seconds}", NetworkTimer.ToString())); break;
                }

                if (CursedPlayer.List.Count() == 1)
                {
                    text = text.Replace("{players}", $"{CursedPlayer.List.Count()} " + Plugin.Instance.Config.PlayerJoinText);
                }
                else
                {
                    text = text.Replace("{players}", $"{CursedPlayer.List.Count()} " + Plugin.Instance.Config.PlayersJoinText);
                }

                if (25 != 0 && 25 > 0)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        text += "\n";
                    }
                }

                foreach (CursedPlayer ply in CursedPlayer.List)
                {
                    ply.ShowHint(text.ToString(), 1);
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }

        private IEnumerator<float> LobbyIcomTimer()
        {
            while (CursedRound.IsInLobby)
            {
                text = string.Empty;

                text += Plugin.Instance.Config.TitleText;

                text += "\n" + Plugin.Instance.Config.PlayerCountText;

                short NetworkTimer = GameCore.RoundStart.singleton.NetworkTimer;

                switch (NetworkTimer)
                {
                    case -2: text = text.Replace("{seconds}", Plugin.Instance.Config.ServerPauseText); break;

                    case -1: text = text.Replace("{seconds}", Plugin.Instance.Config.RoundStartText); break;

                    case 1: text = text.Replace("{seconds}", Plugin.Instance.Config.SecondLeftText.Replace("{seconds}", NetworkTimer.ToString())); break;

                    case 0: text = text.Replace("{seconds}", Plugin.Instance.Config.RoundStartText); break;

                    default: text = text.Replace("{seconds}", Plugin.Instance.Config.SecondsLeftText.Replace("{seconds}", NetworkTimer.ToString())); break;
                }

                if (CursedPlayer.List.Count() == 1)
                {
                    text = text.Replace("{players}", $"{CursedPlayer.List.Count()} " + Plugin.Instance.Config.PlayerJoinText);
                }
                else
                {
                    text = text.Replace("{players}", $"{CursedPlayer.List.Count()} " + Plugin.Instance.Config.PlayersJoinText);
                }

                if (25 != 0 && 25 > 0)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        text += "\n";
                    }
                }

                IntercomDisplay._singleton.Network_overrideText = $"<size={Plugin.Instance.Config.IcomTextSize}>" + text + "</size>";

                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
