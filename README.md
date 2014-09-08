###GamersCoin SteamTradingBot Poc

Open Source Project Supported by http://game4commit.gamers-coin.org
Get Gamerscoins for contributes.
[![tip for next commit](http://game4commit.gamers-coin.org/projects/6.svg)](http://game4commit.gamers-coin.org/projects/6)

Steam Trading Bot Working with GamersCoin Gaming Cryptocurrency.

**Just a Proof of Concept**
#
#
How it Works ?

Debian / Ubuntu
Download and Start GamersCoin Daemon Client

git clone https://github.com/johngame/Gamerscoin-Auto-Daemon-Install-Script
chmod 0777 && ./gamerscoin-easy.sh

Read your randonm Username and Password from gamerscoin.conf  WINDOWS (~/APPDATE/GAMERSCOIN) LINUX (~/.gamerscoin/)

Edit app.config to your Random Secure Username and Password
```
  <appSettings>
    <add key="PrimaryUsername" value="username"/>
    <add key="PrimaryPassword" value="masterpassword"/>
    <add key="PrimaryServerIp" value="http://192.168.178.33:40001"/>
  </appSettings>
```

###Goal
Better Buy/Sell System with Gamerscoin with bot escrow service.

-	No More scam.<br>
-	Send items to bot with the price,bot hold the item/s until some one buy it.<br>
-	User x got GamersCoin User y got Items.<br>
-	Perfect FairTrade System without 15 % Steam Market Fee.<br>
-	Now user x can simple exchange GamersCoins to euro or $ or others on any GamersCoin Exchanges.<br>
-	Or User x buy new items with it.<br>

###Working
Inculde GamersCoin Wrapper.
Send GamersCoin to Wallet.
Withdraw GamersCoin from Wallet.
Get new Wallet Address.
Each user got a wallet with her steam_id.

###Info

**SteamBot** is a bot written in C# for the purpose of interacting with Steam Chat and Steam Trade.  As of right now, about 8 contributors have all added to the bot.  The bot is publicly available under the MIT License. Check out [LICENSE] for more details.

There are several things you must do in order to get SteamBot working:

1. Download the source.
2. Compile the source code.
3. Configure the bot (username, password, etc.).
4. *Optionally*, customize the bot by changing the source code.

## Getting the Source

Retrieving the source code should be done by following the [installation guide] on the wiki. The install guide covers the instructions needed to obtain the source code as well as the instructions for compiling the code.

## Configuring the Bot

See the [configuration guide] on the wiki. This guide covers configuring a basic bot as well as creating a custom user handler.

## Bot Administration

While running the bots you may find it necessary to do some basic operations like shutting down and restarting a bot. The console will take some commands to allow you to do some this. See the [usage guide] for more information.

## More help?
If it's a bug, open an Issue; if you have a fix, read [CONTRIBUTING.md] and open a Pull Request.  If it is a question about how to use SteamBot with your own bots, visit our subreddit at [/r/SteamBot](http://www.reddit.com/r/SteamBot). Please use the issue tracker only for bugs reports and pull requests. The subreddit should be used for all other  discussions.


A list of contributors (add yourself if you want to):

- [Jessecar96](http://steamcommunity.com/id/jessecar) (project lead)
- [geel9](http://steamcommunity.com/id/geel9)
- [cwhelchel](http://steamcommunity.com/id/cmw69krinkle)
- [Lagg](http://lagg.me)

## Wanna Contribute?
Please read [CONTRIBUTING.md].


   [installation guide]: https://github.com/Jessecar96/SteamBot/wiki/Installation-Guide
   [CONTRIBUTING.md]: https://github.com/Jessecar96/SteamBot/blob/master/CONTRIBUTING.md
   [LICENSE]: https://github.com/Jessecar96/SteamBot/blob/master/LICENSE
   [configuration guide]: https://github.com/Jessecar96/SteamBot/wiki/Configuration-Guide
   [usage guide]: https://github.com/Jessecar96/SteamBot/wiki/Usage-Guide
