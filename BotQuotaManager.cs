using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Menu;
using Microsoft.Extensions.Logging;

namespace BQM;

[MinimumApiVersion(260)]
public class BQM : BasePlugin, IPluginConfig<BQMConfig>
{
    public override string ModuleName => "[CS2-BQM]";
    public override string ModuleDescription => "Bot Quota Manager for CS2";
    public override string ModuleAuthor => "verneri";
    public override string ModuleVersion => "1.2";

    public BQMConfig Config { get; set; } = new();

    public void OnConfigParsed(BQMConfig config)
	{
        Config = config;
    }

    public override void Load(bool hotReload)
    {
        bool balance = Config.BalanceTeams;
        bool Limitteams = Config.LimitTeams;
        if (balance) { Server.ExecuteCommand($"mp_autoteambalance 1"); } else { Server.ExecuteCommand($"mp_autoteambalance 0"); };
        if (Limitteams) { Server.ExecuteCommand($"mp_limitteams 1"); } else { Server.ExecuteCommand($"mp_limitteams 0"); };

        AddCommand($"{Config.MenuCommand}", "Open management menu", OnCommandMenu);

        Logger.LogInformation($"Loaded (version {ModuleVersion})");
    }


    [GameEventHandler]
    public HookResult OnRoundPreStart(EventRoundPrestart @event, GameEventInfo info)
    {
        if (@event == null) return HookResult.Continue;

        if (Config.EnableAutoBotQuota)
        {
            AddBots();
        }

        return HookResult.Continue;
    }

    private void AddBots()
    {
        

        if (GetPlayersCount() <= Config.MaxPlayersWithBots)
        {
            Server.ExecuteCommand($"bot_quota {Config.BotQuota}");
            Logger.LogInformation($"Server has {GetPlayersCount()} players. Executing 'bot_quota {Config.BotQuota}'");
        }
        else
        {
            Server.ExecuteCommand("bot_quota 0");
            Logger.LogInformation($"Server has {GetPlayersCount()} players. Executing 'bot_quota 0'");
        }
		
    }


    public void OnCommandMenu(CCSPlayerController? player, CommandInfo command)
    {
        if (!player.IsValid)
        {
            return;
        }

        if(!AdminManager.PlayerHasPermissions(player, Config.MenuFlag))
        {
            command.ReplyToCommand($"{Localizer["no.access"]}");
            return;
        }


        CenterHtmlMenu menu = new CenterHtmlMenu(Localizer["mainmenu.title"], this);
        menu.AddMenuOption(Localizer["mainmenu.BotMenu"], (client, option) =>
        {
			MenuManager.CloseActiveMenu(player);
			BotAddMenu(player);
        });
        menu.AddMenuOption(Localizer["mainmenu.CvarMenu"], (client, option) =>
        {
            MenuManager.CloseActiveMenu(player);
            CvarMenu(player);
        });
        menu.ExitButton = true;
        MenuManager.OpenCenterHtmlMenu(this, player, menu);


    }
	
	private void BotAddMenu(CCSPlayerController player)
    {
		if (!player.IsValid)
        {
            return;
        }
		
		CenterHtmlMenu menu = new CenterHtmlMenu(Localizer["BotMenu.title"], this);
		menu.AddMenuOption(Localizer["BotMenu.addct"], (client, option) =>
        {
            Server.ExecuteCommand("bot_add ct");
            player.PrintToChat($"{Localizer["BotAddCt"]}");
        });
		menu.AddMenuOption(Localizer["BotMenu.addt"], (client, option) =>
        {
            Server.ExecuteCommand("bot_add t");
            player.PrintToChat($"{Localizer["BotAddt"]}");
        });
		menu.AddMenuOption(Localizer["BotMenu.removect"], (client, option) =>
        {
            Server.ExecuteCommand("bot_kick ct");
            player.PrintToChat($"{Localizer["Botremovect"]}");
        });
		menu.AddMenuOption(Localizer["BotMenu.removet"], (client, option) =>
        {
            Server.ExecuteCommand("bot_kick t");
            player.PrintToChat($"{Localizer["Botremovet"]}");
        });
		menu.AddMenuOption(Localizer["BotMenu.removeall"], (client, option) =>
        {
            Server.ExecuteCommand("bot_kick all");
            Server.ExecuteCommand("bot_quota 0");
            player.PrintToChat($"{Localizer["Botremoveall"]}");
        });
        menu.ExitButton = true;
        MenuManager.OpenCenterHtmlMenu(this, player, menu);
		
	}

    private void CvarMenu(CCSPlayerController player)
    {
        if (!player.IsValid)
        {
            return;
        }

        CenterHtmlMenu menu = new CenterHtmlMenu(Localizer["CvarMenu.title"], this);
        menu.AddMenuOption(Localizer["CvarMenu.Limitteams.on"], (client, option) =>
        {
            Server.ExecuteCommand("mp_limitteams 1");
            player.PrintToChat($"{Localizer["limitteamstrue"]}");
        });
        menu.AddMenuOption(Localizer["CvarMenu.Limitteams.off"], (client, option) =>
        {
            Server.ExecuteCommand("mp_limitteams 0");
            player.PrintToChat($"{Localizer["limitteamsfalse"]}");
        });
        menu.ExitButton = true;
        MenuManager.OpenCenterHtmlMenu(this, player, menu);

    }


    //Credits NockyCZ
    private static int GetPlayersCount()
    {
        return Utilities.GetPlayers().Where(p => !p.IsHLTV && !p.IsBot && p.PlayerPawn.IsValid && p.Connected == PlayerConnectedState.PlayerConnected && p.SteamID.ToString().Length == 17).Count();
    }
}