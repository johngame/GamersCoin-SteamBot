using SteamKit2;
using System.Collections.Generic;
using SteamTrade;
//gmc inc
using System;
using System.Configuration;
using System.Globalization;
using GamerscoinWrapper.Wrapper;
using GamerscoinWrapper.Wrapper.Interfaces;
using System.Threading;
using System.Timers;

namespace SteamBot
{
    public class SimpleUserHandler : UserHandler
    {
        public int ScrapPutUp;

        public SimpleUserHandler (Bot bot, SteamID sid) : base(bot, sid) {}

        public override bool OnGroupAdd()
        {
            return false;
        }

        public override bool OnFriendAdd () 
        {
            return true;
        }

        public override void OnLoginCompleted()
        {
        }

        public override void OnChatRoomMessage(SteamID chatID, SteamID sender, string message)
        {
            Log.Info(Bot.SteamFriends.GetFriendPersonaName(sender) + ": " + message);
            base.OnChatRoomMessage(chatID, sender, message);
        }

        public override void OnFriendRemove () {}
        
        public override void OnMessage (string message, EChatEntryType type)
        {
            message = message.ToLower();

            //REGULAR chat commands
            if (message.Contains("!help"))
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Available Bot Commands :");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!wallet");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!getwallet");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!withdraw");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!trade");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!getid");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!buy");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!sell");
            }

            else if (message.Contains("!wallet"))
            {
                //Enter GamersCoin Demon Settings into app.config
                //Basic Setup Wallet Connector
                IBaseBtcConnector baseBtcConnector = new BaseBtcConnector(true); // Use Primary Wallet
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Connecting to Gamerscoin daemon: " + ConfigurationManager.AppSettings["ServerIp"] + "...");
                string myBalance = baseBtcConnector.GetReceivedByAccount("" + OtherSID);
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "My balance: " + myBalance + " GMC");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "<3");
                string woot = baseBtcConnector.GetAccountAddress("bot");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Send Your GamersCoins to : " + woot + " to buy Items with GamersCoins");
            }
            //Get New Wallet Address
            else if (message.Contains("!getwallet"))
            {
                IBaseBtcConnector baseBtcConnector = new BaseBtcConnector(true); // Get New Wallet for User with SteamID
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Connecting to Gamerscoin daemon: " + ConfigurationManager.AppSettings["ServerIp"] + "...");
                string woot = baseBtcConnector.GetAccountAddress("" + OtherSID);
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Your New GamersCoin Address : " + woot);
            }
            // Withdraw All GamersCoin from Wallet
            else if (message.StartsWith("!withdraw"))
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Thanks for Freedom !!!");

                //IBaseBtcConnector baseBtcConnector = new BaseBtcConnector(true); // Withdraw GamersCoins from Wallet for User
                //Bot.SteamFriends.SendChatMessage(OtherSID, type, "Connecting to Gamerscoin daemon: " + ConfigurationManager.AppSettings["ServerIp"] + "...");

                //decimal amount = 50;
                //string woot = baseBtcConnector.SendFrom("" + OtherSID,"todo read address from chat and amount to send" , amount );
                //Bot.SteamFriends.SendChatMessage(OtherSID, type, "Your New GamersCoin Address : " + woot);
            }

            else if (message.Contains("!trade"))
            {
                Bot.SteamTrade.Trade(OtherSID);
            }

            else if (message.Contains("fuck") || message.Contains("suck") || message.Contains("dick") || message.Contains("cock") || message.Contains("tit") || message.Contains("boob") || message.Contains("pussy") || message.Contains("vagina") || message.Contains("cunt") || message.Contains("penis"))
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Sorry, but as a robot I cannot perform sexual functions.");
            }

            else if (message.Contains("!getid"))
            {
                string userinfo = Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " (" + OtherSID.ToString() + ")";
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "You're welcome!  " + userinfo);
                Bot.log.Success(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " (" + OtherSID.ToString() + ") added me!");
            }

            else if (message == "!buy")
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Please type that into the TRADE WINDOW, not here!");
            }

            else if (message == "!sell")
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Please type that into the TRADE WINDOW, not here!");
            }


            // ADMIN commands
            else if (message == "self.restart")
            {
                if (IsAdmin)
                {
                    // Starts a new instance of the program itself
                    var filename = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    System.Diagnostics.Process.Start(filename);

                    // Closes the current process

                }
            }
            else if (message == ".canceltrade")
            {
                if (IsAdmin)
                {
                    Trade.CancelTrade();

                }
            }
            else
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, Bot.ChatResponse);
            }
        }

        public override bool OnTradeRequest() 
        {
            return true;
        }
        
        public override void OnTradeError (string error) 
        {
            Bot.SteamFriends.SendChatMessage (OtherSID, 
                                              EChatEntryType.ChatMsg,
                                              "Oh, there was an error: " + error + "."
                                              );
            Bot.log.Warn (error);
        }
        
        public override void OnTradeTimeout () 
        {
            Bot.SteamFriends.SendChatMessage (OtherSID, EChatEntryType.ChatMsg,
                                              "Sorry, but you were AFK and the trade was canceled.");
            Bot.log.Info ("User was kicked because he was AFK.");
        }
        
        public override void OnTradeInit() 
        {
            Trade.SendMessage ("Success. Please put up your items.");
        }
        
        public override void OnTradeAddItem (Schema.Item schemaItem, Inventory.Item inventoryItem) {}
        
        public override void OnTradeRemoveItem (Schema.Item schemaItem, Inventory.Item inventoryItem) {}
        
        public override void OnTradeMessage (string message) {}
        
        public override void OnTradeReady (bool ready) 
        {
            if (!ready)
            {
                Trade.SetReady (false);
            }
            else
            {
                if(Validate ())
                {
                    Trade.SetReady (true);
                }
                Trade.SendMessage ("Scrap: " + ScrapPutUp);
            }
        }

        public override void OnTradeSuccess()
        {
            // Trade completed successfully
            Log.Success("Trade Complete.");
        }

        public override void OnTradeAccept() 
        {
            if (Validate() || IsAdmin)
            {
                //Even if it is successful, AcceptTrade can fail on
                //trades with a lot of items so we use a try-catch
                try {
                    if (Trade.AcceptTrade())
                        Log.Success("Trade Accepted!");
                }
                catch {
                    Log.Warn ("The trade might have failed, but we can't be sure.");
                }
            }
        }

        public bool Validate ()
        {            
            ScrapPutUp = 0;
            
            List<string> errors = new List<string> ();
            
            foreach (ulong id in Trade.OtherOfferedItems)
            {
                var item = Trade.OtherInventory.GetItem (id);
                if (item.Defindex == 5000)
                    ScrapPutUp++;
                else if (item.Defindex == 5001)
                    ScrapPutUp += 3;
                else if (item.Defindex == 5002)
                    ScrapPutUp += 9;
                else
                {
                    var schemaItem = Trade.CurrentSchema.GetItem (item.Defindex);
                    errors.Add ("Item " + schemaItem.Name + " is not a metal.");
                }
            }
            
            if (ScrapPutUp < 1)
            {
                errors.Add ("You must put up at least 1 scrap.");
            }
            
            // send the errors
            if (errors.Count != 0)
                Trade.SendMessage("There were errors in your trade: ");
            foreach (string error in errors)
            {
                Trade.SendMessage(error);
            }
            
            return errors.Count == 0;
        }
        
    }
 
}

