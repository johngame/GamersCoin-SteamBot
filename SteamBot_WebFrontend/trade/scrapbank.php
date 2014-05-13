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
		<title>Trade - Scrapbanking | The CTS Community</title>
		<link href="/css/bootstrap.css" rel="stylesheet">
		<link href="/css/bootstrap-responsive.css" rel="stylesheet">
		<link rel="stylesheet" type="text/css" href="style.css" />
		<script src="jquery.js" type="text/javascript"></script>
		<link rel="icon" 
		type="image/png" 
		href="/images/favicon.ico" />
		<script type="text/javascript"><!--
		var reloadInterval = 15000;
		function init() {
		 setTimeout('reload()',reloadInterval);
		}
		function reload() {
		 var iframe = document.getElementById('queue');
		 if (!iframe) return false;
		 iframe.src = iframe.src;
		 setTimeout('reload()',reloadInterval);
		}
		window.onload = init;
		--></script>
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
            <li><a href="/trade/">Trade Bots</a></li>
			<li class="active"><a href="#">Scrapbank</a></li>
            </ul>
			<ul class="nav pull-right">
			<li><?php
			// Steam Sign In
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
					setcookie('steamID64',$steam_login_verify,time() + 86400); // 86400 = 1 day
					setcookie('steam_name',$steam_name,time() + 86400);
					setcookie('avatar_url',$avatar_url,time() + 86400);
					header("Location:http://www.thectscommunity.com/trade/bank.php"); 
				}
			}
			else
			{
				$steam_sign_in_url = SteamSignIn::genUrl();
				echo "<a href=\"$steam_sign_in_url\"><img src='http://cdn.steamcommunity.com/public/images/signinthroughsteam/sits_small.png' /></a>";
			}
			?></li>
			<li><button class="btn btn-success">Bank</button></li>
          </div><!--/.nav-collapse -->
        </div>
      </div>
    </div>
		<div id="wrapper">
			<div id="page">
			<br class="clearfix" />
			<h3>Scrapbanking Bot - Skip to the bottom to add yourself to queue</h3>
					<p>
					Our scrapbanking bot will buy any two craftable weapons from you for 1 scrap.<br>
					He will also sell you any two weapons from 1 scrap.<br>
					However, there is a limit of how many weapons you can buy or sell at a time, based on the number of trades you have done with him.</p>
					<center><p><u>Tier 1 (less than 10 trades)</u><br>
					Can buy and sell: 8 weapons.<br>
					<u>Tier 2 (10 to 24 trades)</u><br>
					Can buy and sell: 12 weapons.<br>
					<u>Tier 3 (25 to 49 trades)</u><br>
					Can buy and sell: 18 weapons.<br>
					<u>Tier 4 (50 to 99 trades)</u><br>
					Can buy and sell: 24 weapons.<br>
					<u>Tier 5 (more than 100 trades)</u><br>
					Can buy and sell: 40 weapons.<br></p></center>
					<p>You can also donate 1 refined (by adding it to the trade) to raise your current limit by an additional 8 weapons. Each refined you donate will permanantly add 8 weapons to the number of weapons you can buy or sell from the bot.
					</p>
					<p>
					<b>PLEASE READ IF YOU ARE BUYING WEAPONS:</b>
					Each trade has a limit of 5 minutes max. If you spend more than 5 minutes in a trade you will be kicked.<br>
					Please figure out what weapons you want first before you trade with the bot.<br>
					To see what weapons Scraps has in stock, check his backpack <a href="http://backpack.tf/id/ScrapsTheScrapbanker">here</a>.<br>
					To buy weapons, type "buy" in the trade window and then type the name of the weapon you want. Scraps will find the weapon for you and add it automatically.<br>
					If he has added the wrong weapon, type "/remove" and then the weapon name to remove it from the trade.
					</p>
			<p>
			<?php
			if (isset($_COOKIE["steam_name"]))
			{
				if (isset($_COOKIE["queue_state"]))
				{
					if ($_COOKIE["queue_state"] == 1)
					{
						echo "<div class=\"alert alert-success\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button><strong>Note:</strong> You are currently in queue.</div>";
					}
					else if ($_COOKIE["queue_state"] == 0)
					{
						echo "<div class=\"alert alert-error\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button><strong>Error:</strong> Something went wrong; please try again!</div>";
					}
					else if ($_COOKIE["queue_state"] == 3)
					{
						echo "<div class=\"alert alert-warning\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button><strong>Note:</strong> You are already in queue.</div>";
					}
				}
				echo "<a href=\"queue.php\"><button class=\"btn btn-large btn-success\">Add to Queue</button></a>";
			}
			else
			{
				echo "<div class=\"alert alert-error\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button><strong>Error:</strong> You are not logged in! You will be unable to join the queue until you log in.</div>";
				echo "<button class=\"btn btn-large btn-success\">Add to Queue</button>";
			}
			?>
			<br>
			<?php
			if ($_COOKIE["queue_state"] == 1 || $_COOKIE["queue_state"] == 3)
			{
				echo "<center><iframe id=\"queue\" src=\"display.php\" width=\"440\" height=\"350\" frameBorder=\"0\"></iframe></center>";
			}
			?>
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

function error()
{
	setcookie('queue_state',0,time() + 300,'/','.thectscommunity.com',false);
	$referer = $_SERVER['HTTP_REFERER'];
	header("Location:$referer"); 
}

function display_queue()
{
	$host="HOST_NAME"; // Host name
	$username="USERNAME"; // Mysql username
	$password="PASSWORD"; // Mysql password
	$db_name="trade_bots"; // Database name
	$tbl_name="Queue"; // Table name

	// Connect to server and select database.
	mysql_connect("$host", "$username", "$password") or error(); //or die("cannot connect");
	mysql_select_db("$db_name") or error(); //or die("cannot select DB");
	
	$sql = "SELECT sid64,avatar FROM $tbl_name  WHERE processed = 0 AND position != 0 ORDER BY position";

	$get_queue = mysql_query($sql);
	if(!$get_queue)
	{
	  die('Could not get data: ' . mysql_error());
	}
	$position = 1;
	while($row = mysql_fetch_array($get_queue, MYSQL_ASSOC))
	{
		echo "<tr>".
			 "<td>".str_pad($position, 4, "000", STR_PAD_LEFT)."</td>".
			 "<td><img src=\"{$row['avatar']}\">  {$row['sid64']}</td>".
			 "</tr>";
		$position++;
	} 
	mysql_close($conn);
}
?>