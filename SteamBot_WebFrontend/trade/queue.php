<?php
function error()
{
	setcookie('queue_state',0,time() + 300,'/','.thectscommunity.com',false);
	$referer = $_SERVER['HTTP_REFERER'];
	header("Location:$referer");
}

	if ($_SERVER["HTTP_REFERER"] != "http://www.thectscommunity.com/trade/scrapbank.php")
	{
		die("You cannot directly access this page. You must go to <a href=\"http://www.thectscommunity.com/trade/bank.php\">this page</a> first.");
	}
	
	$host="HOST_NAME"; // Host name
	$username="USERNAME"; // Mysql username
	$password="PASSWORD"; // Mysql password
	$db_name="trade_bots"; // Database name
	$tbl_name="Queue"; // Table name

	// Connect to server and select database.
	mysql_connect("$host", "$username", "$password") or die(error()); //or die("cannot connect");
	mysql_select_db("$db_name") or die(error()); //or die("cannot select DB");

	// Get values from form
	$sid64=$_COOKIE['steamID64'];
	$processed=false;
	$avatar=$_COOKIE['avatar_url'];
	
	// Check if unprocessed user is already in database
	$sql="SELECT position,sid64,processed FROM $tbl_name";
	$result=mysql_query($sql);
	
	if(!$result)
	{
		die(error());
	}
	
	$found_match = false;
	$process_state = false;
	$position = 0;
	while($row = mysql_fetch_array($result, MYSQL_ASSOC))
	{
		if ($row['position'] > $position)
		{
			$position = $row['position'];
		}
		if ($row['sid64'] == $sid64)
		{
			$found_match = true;
			if ($row['processed'] == true)
			{
				$process_state = true;
			}
		}
	} 
	
	++$position;
	
	if ($found_match == true)
	{
		if ($process_state == false)
		{
			// Return already in queue error cookie
			setcookie('queue_state',3,time() + 60,'/','.thectscommunity.com',false);
		}
		else
		{
			// (UPDATE) Set db processed to false
			$sql="UPDATE $tbl_name SET processed = 0, position = $position WHERE sid64 = $sid64";
			$insert=mysql_query($sql);
			
			if($insert){
			setcookie('queue_state',1,time() + 60,'/','.thectscommunity.com',false);
			setcookie("alert_played","0",time()+60,'/','.thectscommunity.com',false);
			} else {
				setcookie('queue_state',0,time() + 60,'/','.thectscommunity.com',false);
			}
		}
	}
	else
	{
		// Insert data into mysql
		$sql="INSERT INTO $tbl_name(position, sid64, processed, avatar)VALUES('$position', '$sid64', '$processed', '$avatar')";
		$insert=mysql_query($sql);

		// if successfully insert data into database, displays message "Successful".
		if($insert){
			setcookie('queue_state',1,time() + 60,'/','.thectscommunity.com',false);
		} else {
			setcookie('queue_state',0,time() + 60,'/','.thectscommunity.com',false);
		}
	}
	
	// close connection
	mysql_close();
	
	$referer = $_SERVER['HTTP_REFERER'];
	header("Location:$referer"); 
?>