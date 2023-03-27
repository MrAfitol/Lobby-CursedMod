# Lobby
[![GitHub release](https://flat.badgen.net/github/release/MrAfitol/Lobby-CursedMod)](https://github.com/MrAfitol/Lobby-CursedMod/releases/)
![GitHub downloads](https://flat.badgen.net/github/assets-dl/MrAfitol/Lobby-CursedMod)


A plugin that adds a lobby when waiting for players
## How download ?
   - *1. Find the SCP SL server config folder*
   
   *("C:\Users\\(user name)\AppData\Roaming\SCP Secret Laboratory\" for windows, "/home/(user name)/.config/SCP Secret Laboratory/" for linux)*
  
   - *2. Find the "PluginAPI" folder there, it contains the "plugins" folder.*
  
   - *3. Select the folder where CursedMod is downloaded (global or (server port)), and go to the path CursedMod\Plugins, and move the plugin to this folder*
## View
https://user-images.githubusercontent.com/76150070/208076431-7e7a98e3-d1b3-4365-a989-a09e7fa7f639.mp4


## Config
```yml
# Main text ({seconds} - Either it shows how much is left until the start, or the server status is "Server is suspended", "Round starting")
title_text: <size=50><color=#F0FF00><b>Waiting for players, {seconds}</b></color></size>
# Text showing the number of players ({players} - Text with the number of players)
player_count_text: <size=40><color=#FFA600><i>{players}</i></color></size>
# What will be written if the lobby is locked?
server_pause_text: Server is suspended
# What will be written when there is a second left?
second_left_text: '{seconds} second left'
# What will be written when there is more than a second left?
seconds_left_text: '{seconds} seconds left'
# What will be written when the round starts?
round_start_text: Round starting
# What will be written when there is only one player on the server?
player_join_text: player joined
# What will be written when there is more than one player on the server?
players_join_text: players joined
# What is the movement boost intensity?
movement_boost_intensity: 50
# What role will people play in the lobby?
lobby_player_role: Tutorial
# Allow people to talk over the intercom?
allow_icom: true
# Display text on Intercom? (Works only when lobby Intercom type)
display_in_icom: true
# What size will the text be in the Intercom? (The larger the value, the smaller it will be)
icom_text_size: 20
# What items will be given when spawning a player in the lobby? (Leave blank to keep inventory empty)
lobby_inventory:
- Coin
# In what locations can people spawn? (If it is less than 1, a random one will be selected)
lobby_location:
- Tower
- Intercom
- GR18
- SCP173
```

## LobbyLocationType
```
Tower,
Intercom,
GR18,
SCP173
```
