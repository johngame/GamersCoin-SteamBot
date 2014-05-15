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
            Bot.log.Success(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " (" + OtherSID.ToString() + ") added me!");
            string userinfo = Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " (" + OtherSID.ToString() + ")";
            Bot.log.Success(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " (" + OtherSID.ToString() + ") added me!");
            // Using a timer here because the message will fail to send if you do it too quickly
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
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "!info");
            }

            else if (message.Contains("hi"))
            {
                string userinfo = Bot.SteamFriends.GetFriendPersonaName(OtherSID);
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "You're welcome!  " + userinfo);
            }

            else if (message.Contains("!wallet"))
            {
                //Enter GamersCoin Demon Settings into app.config
                //Basic Setup Wallet Connector
                IBaseBtcConnector baseBtcConnector = new BaseBtcConnector(true); // Use Primary Wallet
                
                //Bot.SteamFriends.SendChatMessage(OtherSID, type, "Connecting to Gamerscoin daemon: " + ConfigurationManager.AppSettings["ServerIp"] + "...");

                Bot.SteamFriends.SendChatMessage(OtherSID, type, "##################################################################################");
                string woot = baseBtcConnector.GetAccountAddress(OtherSID + "");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Send Your GamersCoins to : " + woot + " to buy Items with GamersCoins");

                decimal myBalance = baseBtcConnector.GetBalance(OtherSID + "");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "My balance: " + myBalance + " GMC");
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "##################################################################################");
            }
            //Get New Wallet Address
            else if (message.Contains("!getwallet"))
            {
                IBaseBtcConnector baseBtcConnector = new BaseBtcConnector(true); // Get New Wallet for User with SteamID
                //Bot.SteamFriends.SendChatMessage(OtherSID, type, "Connecting to Gamerscoin daemon: " + ConfigurationManager.AppSettings["ServerIp"] + "...");
                string woot = baseBtcConnector.GetAccountAddress("" + OtherSID);
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Your New GamersCoin Address : " + woot);
            }

            // Withdraw All GamersCoin from Wallet
            else if (message.StartsWith("!withdraw G"))
            {
                // Create new instance with string array.

                string lineOfText = message.ToString();
                string[] wordArray = lineOfText.Split(' ');
                string account = OtherSID.ToString();
                string address = wordArray[1].ToString();
                decimal amount = Convert.ToDecimal(wordArray[2]);

                IBaseBtcConnector baseBtcConnector = new BaseBtcConnector(true); // Withdraw GamersCoins from Wallet for User
                decimal myBalance = baseBtcConnector.GetBalance(OtherSID + "");


                if (myBalance >= amount)
                {
                    baseBtcConnector.SendFrom(account, address, amount);
                    Thread.Sleep(2000);
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "##################################################################################");
                    //Bot.SteamFriends.SendChatMessage(OtherSID, type, "Connecting to Gamerscoin daemon: " + ConfigurationManager.AppSettings["ServerIp"] + "...");      
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "You withdraw " + amount + " GamersCoins to " + address + " address.");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "Withdraw Done !!!");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "Thanks for using our Fairtradebot !!!");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "Have a nice Day ....");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "Thanks for Freedom !!!");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "Your New Wallet balance: " + myBalance + " GMC");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "##################################################################################");
                    Bot.log.Success(Bot.SteamFriends.GetFriendPersonaName(OtherSID) + " (" + OtherSID.ToString() + ") withdraw " + address + " " + amount);

                }
                else
                {
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "##################################################################################");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "Your withdraw amount is to high");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "Your Wallet balance: " + myBalance + " GMC");
                    Bot.SteamFriends.SendChatMessage(OtherSID, type, "##################################################################################");
                }
            }

            else if (message.Contains("!withdraw"))
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Please use : !withdraw gamerscoinaddress amount-to-paypout");
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
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "You're steamname and steamid is  " + userinfo);
            }

            else if (message == "!buy")
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Please type that into the TRADE WINDOW, not here!");
            }

            else if (message == "!sell")
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Please type that into the TRADE WINDOW, not here!");
            }
            else if (message == "!info")
            {
                Bot.SteamFriends.SendChatMessage(OtherSID, type, "Bot verion 1.0");
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

