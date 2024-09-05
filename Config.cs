using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace BQM
{
    public class BQMConfig : BasePluginConfig
    {

        [JsonPropertyName("EnableAutoQuota")]
        public bool EnableAutoBotQuota { get; set; } = true;

        [JsonPropertyName("MaxPlayersWithBots")]
        public int MaxPlayersWithBots { get; set; } = 1;

        [JsonPropertyName("BotQuota")]
		public string BotQuota { get; set; } = "6";

        [JsonPropertyName("BalanceTeams")]
        public bool BalanceTeams { get; set; } = true;

        [JsonPropertyName("LimitTeams")]
        public bool LimitTeams { get; set; } = true;

        [JsonPropertyName("MenuFlag")]
        public string MenuFlag { get; set; } = "@css/generic";

        [JsonPropertyName("MenuCommand")]
        public string MenuCommand { get; set; } = "css_bqm";

    }
}
