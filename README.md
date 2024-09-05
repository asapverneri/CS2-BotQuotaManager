## 🎮 CS2 BotQuotaManager

This plugin handles bot_quota depending on playercount on the server. It has english language available for now, but if you want you can translate it to any language.
Tested on Windows, but should work on Linux aswell.


![GitHub tag (with filter)](https://img.shields.io/github/v/tag/asapverneri/CS2-BotQuotaManager?style=for-the-badge&label=Version)

> [!CAUTION]  
> This is WIP. Tested alone & for sure not perfect

---

## 📦 Installion

- Install [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) & [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
- Download the latest release from the releases tab and copy it into the counterstrikesharp plugins folder.
The config is generated after the first start of the plugin.

---

## 💻 Usage

To edit permission and database settings please edit config file.
Located in the folder `counterstrikesharp/configs/plugins/BotQuotaManager`

You can eiter use automatic botquota option or manage bots manually via management menu.

**Example config(Automatic):**
```json
{
  "EnableAutoQuota": true, // Automatic bot quota depending on "MaxPlayersWithBots"
  "MaxPlayersWithBots": 1, // if playercount goes above this number, bots will be kicked
  "BotQuota": "6", // How many bots
  "BalanceTeams": true, // Should teams be balanced
  "LimitTeams": true,
  "MenuFlag": "@css/generic", // Flag for management menu
  "MenuCommand": "css_bqm", // Command for management menu
  "ConfigVersion": 1
}
```

**Example config(Manual):**
```json
{
  "EnableAutoQuota": false, // This will totally disable automatic botquota 
  "MaxPlayersWithBots": 1, 
  "BotQuota": "6", 
  "BalanceTeams": false, 
  "LimitTeams": false,
  "MenuFlag": "@css/generic", 
  "MenuCommand": "css_bqm", 
  "ConfigVersion": 1
}
```
---

## 📫 Contact

<div align="center">
  <a href="https://discordapp.com/users/367644530121637888">
    <img src="https://img.shields.io/badge/Discord-7289DA?style=for-the-badge&logo=discord&logoColor=white" alt="Discord" />
  </a>
  <a href="https://steamcommunity.com/id/vvernerii/">
    <img src="https://img.shields.io/badge/Steam-000000?style=for-the-badge&logo=steam&logoColor=white" alt="Steam" />
  </a>
</div>

---