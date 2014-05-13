<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<!--
Design by Free CSS Templates
http://www.freecsstemplates.org
Released for free under a Creative Commons Attribution 3.0 License

Name       : Artifice
Description: A two-column, fixed-width design with a dark color scheme.
Version    : 1.0
Released   : 20120813
-->
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		<title>Trade Bots | The CTS Community</title>
		<link href="/css/bootstrap.css" rel="stylesheet">
		<link href="/css/bootstrap-responsive.css" rel="stylesheet">
		<link rel="stylesheet" type="text/css" href="style.css" />
		<script src="jquery.js" type="text/javascript"></script>
		<link rel="icon" 
		type="image/png" 
		href="/images/favicon.ico" />
	</head>
	<body>
	<div class="navbar navbar-inverse navbar-fixed-top">
      <div class="navbar-inner">
        <div class="container-fluid">
          <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </a>
          
          <div class="nav-collapse collapse">
            <ul class="nav nav-tab">
			<li><a href="/"><font size="4"><b>THE CTS COMMUNITY</b></font> </a></li>
			<li class="divider-vertical"></li>
            <li class="active"><a href="#">Trade Bots</a></li>
			<li><a href="scrapbank.php">Scrapbank</a></li>
            </ul>
			<ul class="nav pull-right">
			<li><?php
			include 'login.php';
			$steam_login_verify = SteamSignIn::validate();
			if($_COOKIE['steam_name']!='' && $_COOKIE['avatar_url']!='')
			{
				$name = $_COOKIE['steam_name'];
				$avatar_url = $_COOKIE['avatar_url'];
				echo "<a href=\"#\"><img class=\"avatar tiny-avatar\" src='$avatar_url' /> $name</a>";
			}
			else if(!empty($steam_login_verify))
			{
				get_steam_status($steam_login_verify);
				if ($steam_name == null)
				{
					$steam_sign_in_url = SteamSignIn::genUrl();
					echo "<a href=\"$steam_sign_in_url\">Error connecting to Steam. Click to try again.</a>";
				}
				else
				{
					echo "<a href=\"#\"><img class=\"avatar tiny-avatar\" src='$avatar_url' /> $steam_name</a>";
					setcookie('steamID64',$steam_login_verify,time() + 86400,'/','.thectscommunity.com',false); // 86400 = 1 day
					setcookie('steam_name',$steam_name,time() + 86400,'/','.thectscommunity.com',false);
					setcookie('avatar_url',$avatar_url,time() + 86400,'/','.thectscommunity.com',false);
				}
			}
			else
			{
				$steam_sign_in_url = SteamSignIn::genUrl();
				echo "<a href=\"$steam_sign_in_url\"><img src='http://cdn.steamcommunity.com/public/images/signinthroughsteam/sits_small.png' /></a>";
			}
			?></li>
			<a href="#"><button class="btn btn-success">Bank</button></a>
          </div><!--/.nav-collapse -->
        </div>
      </div>
    </div>
	
		<div id="wrapper">
			<div id="page">
			<br class="clearfix" />
					<center>
					<div class="page-header">
					<h1>CTS Automated Trade Bots</small></h1>
					</div>
					<h3>Keybanking Bot</h3>
					Our keybanking bot buys and sells keys quickly and efficiently.
					Just add him and send him a trade!<br>
					<a href="http://steamcommunity.com/id/KlausKey"><img src="http://steamsignatures.com/img.php?id=KlausKey"></a><a href='steam://friends/add/76561198065829815'><img src='http://www.thectscommunity.com/images/AddFriend.png'></a><br>
					<br>
					<h3>Scrapbanking Bot</h3>
					<p>
					Our scrapbanking bot will buy any two craftable weapons from you for 1 scrap.<br>
					He will also sell you any two weapons from 1 scrap.<br>
					However, there is a limit of how many weapons you can buy or sell at a time, based on the number of trades you have done with him.</p>
					<a href="http://steamcommunity.com/id/ScrapsTheScrapbanker"><img src="http://steamsignatures.com/img.php?id=ScrapsTheScrapbanker"></a><a href='steam://friends/add/76561198065838051'><img src='http://www.thectscommunity.com/images/AddFriend.png'></a><br>
					<p>
					<u>Tier 1 (less than 10 trades)</u><br>
					Can buy and sell: 8 weapons.<br>
					<u>Tier 2 (10 to 24 trades)</u><br>
					Can buy and sell: 12 weapons.<br>
					<u>Tier 3 (25 to 49 trades)</u><br>
					Can buy and sell: 18 weapons.<br>
					<u>Tier 4 (50 to 99 trades)</u><br>
					Can buy and sell: 24 weapons.<br>
					<u>Tier 5 (more than 100 trades)</u><br>
					Can buy and sell: 40 weapons.<br>
					<br>
					You can also donate 1 refined (by adding it to the trade) to raise your current limit by an additional 8 weapons. Each refined you donate will permanantly add 8 weapons to the number of weapons you can buy or sell from the bot.
					</p>
					</center>
				<br class="clearfix" />
			</div>
		</div>
		<div id="footer">
			&copy; 2012-2013 The CTS Community | Design by <a href="http://www.freecsstemplates.org/">FCT</a> | Banner design by <a href="http://steamcommunity.com/profiles/76561198040902904/">Zaxxon</a>
		</div>
		<!-- jQuery via Google + local fallback, see h5bp.com -->
		<script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
		<script>window.jQuery || document.write('<script src="/js/jquery-1.7.1.min.js"><\/script>')</script>

		<!-- Bootstrap jQuery Plugins, compiled and minified -->
		<script src="/js/bootstrap.min.js"></script>
	</body>
</html>

<?php
function get_steam_status($steamID64)
{
	$json_object=file_get_contents("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=120F03A1543C9F22AA9BF4C7B6442154&steamids=$steamID64");
	$json_decoded = json_decode($json_object);
	$GLOBALS['steam_name'] = $json_decoded->response->players[0]->personaname;
	$GLOBALS['avatar_url'] = $json_decoded->response->players[0]->avatar;
}
?>