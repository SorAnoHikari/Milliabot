﻿using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Commands.Permissions.Userlist;
using Discord.Modules;
using DiscordBot.Modules;
using DiscordBot.Modules.Admin;
using DiscordBot.Modules.Colors;
using DiscordBot.Modules.Feeds;
using DiscordBot.Modules.Github;
using DiscordBot.Modules.Modules;
using DiscordBot.Modules.Public;
using DiscordBot.Modules.Twitch;
using DiscordBot.Services;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMusicBot
{
    class Program
    {
        #region Login Info
        public static readonly string EMAIL = "tony.cheng300@gmail.com";
        public static readonly string PASSWORD = "Gardevoir3";
        public static readonly string TL_SOKU_INVITE_CODE = "https://discord.gg/0jh83Qzd7N5HqLXq";
        #endregion

        static void Main(string[] args)
        {
            var inoriClient = new DiscordClient();

            //Display all log messages in the console
            inoriClient.LogMessage += (s, e) => Console.WriteLine($"[{e.Severity}] {e.Source}: {e.Message}");

            .UsingAudio(new AudioServiceConfig
            {
                Mode = AudioMode.Outgoing,
                EnableMultiserver = false,
                EnableEncryption = true,
                Bitrate = 512,
                BufferLength = 10000
            })
            

            //Convert our sync method to an async one and block the Main function until the bot disconnects
            Connect(inoriClient);
        }

        private static async void EchoMessage(DiscordClient client)
        {
            client.MessageReceived += async (s, e) =>
            {
                if (!e.Message.IsAuthor)
                    await client.SendMessage(e.Channel, e.Message.Text);
            };
        }

        private static async void Connect(DiscordClient inoriClient)
        {
            inoriClient.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await inoriClient.Connect(EMAIL, PASSWORD);
                        await inoriClient.SetGame(1);
                        inoriClient.JoinVoiceServer()
                        break;
                    }
                    catch (Exception ex)
                    {
                        inoriClient.LogMessage += (s, e) => Console.WriteLine(String.Concat("Login Failed", ex));
                        await Task.Delay(inoriClient.Config.FailedReconnectDelay);
                    }
                }
            });
        }
    }
}