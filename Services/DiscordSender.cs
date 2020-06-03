using Discord;
using Discord.Webhook;
using LostArkEng.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAE.Services
{
    public class DiscordSender
    {
        private readonly DiscordWebhookClient _discordWebHookClient;
        private readonly IFormatProvider _formatProvider;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public DiscordSender(ulong webhookId, string webhookToken, IFormatProvider formatProvider)
        {
            _discordWebHookClient = new DiscordWebhookClient(webhookId, webhookToken);

            _formatProvider = formatProvider;

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
        public void Emit(string Leader, string MemberTwo, string MemberThree, string MemberFour, string EventName)
        {
            var message = new EmbedBuilder()
                    .WithAuthor("LAE Discord Bot")
                    .WithTitle("EventName")
                    .WithTimestamp(DateTimeOffset.UtcNow)
                    .WithColor(Color.Red);

            message.AddField(new EmbedFieldBuilder()
                    .WithIsInline(false)
                    .WithName("Group Members")
                    .WithValue("Event created by: @" + Leader + " for " + EventName + " is now full! @" + MemberTwo + ", @" + MemberThree + ", @" + MemberFour + ""));


            _discordWebHookClient.SendMessageAsync(string.Empty, embeds: new[] { message.Build() }, username: "LAE Discord Bot");
        }

        public void Dispose()
            => _discordWebHookClient.Dispose();
    }


}
