<html>
<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<link href="/css/bootstrap.css" rel="stylesheet">
		<link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
<div id="queue">
<?php
echo "<center>
			<table class=\"table table-striped\">
				<thead>
				  <tr>
					<th align=\"right\">Position</th>
					<th>User</th>
				  </tr>
				</thead>
				<tbody>";
						display_queue();
				echo "</tbody>
			</table>
			</center>";
			
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
	$user_inqueue = false;
	$position = 1;
	$sid64=$_COOKIE['steamID64'];
	while($row = mysql_fetch_array($get_queue, MYSQL_ASSOC))
	{
		echo "<tr>".
			 "<td align=\"right\">".str_pad($position, 4, "000", STR_PAD_LEFT)."</td>".
			 "<td><img src=\"{$row['avatar']}\"> {$row['sid64']} ";
			 if ($sid64 == $row['sid64'])
			{
				echo "<span class=\"label label-success\">You!</span>";
				$user_inqueue = true;
				if ($position == 1)
				{
					echo "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class=\"label label-warning\">Estimated time to wait: $position minute.</span>";
					if ($_COOKIE['alert_played'] != 1)
					{
						$file='alert.mp3';
						echo "<embed src =\"$file\" hidden=\"true\" autostart=\"true\"></embed>";
						setcookie("alert_played","1",time()+60,'/','.thectscommunity.com',false);
					}
				}
				else
				{
					echo "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class=\"label label-warning\">Estimated time to wait: ($position*5) minutes.</span>";
				}
			}
			 echo "</td>".
			 "</tr>";
		$position++;
	}
	
	if ($user_inqueue == false)
	{
		setcookie("queue_state","",time()-60000,'/','.thectscommunity.com',false);
	}
	else
	{
		setcookie('queue_state',1,time() + 60,'/','.thectscommunity.com',false);
	}
	
	mysql_close($conn);
}
?>
</div>
</body>
</html>