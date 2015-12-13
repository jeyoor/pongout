<?php 
        $db = mysql_connect('mysql_host', 'mysql_user', 'mysql_password') or die('Could not connect: ' . mysql_error()); 
        mysql_select_db('my_database') or die('Could not select database');
 
        // Strings must be escaped to prevent SQL injection attack.
        $playerName = mysql_real_escape_string($_GET['player_name'], $db);
        $playerScore = mysql_real_escape_string($_GET['player_score'], $db);
        $playerType = mysql_real_escape_string($_GET['player_name'], $db);
        $hash = $_GET['hash']; 
 
        $secretKey="mySecretKey"; # Change this value to match the value stored in the client javascript below 

        $realHash = md5($playerName . $playerScore . $secretKey); 
        if($realHash == $hash) { 
            // Send variables for the MySQL database class. 
            $query = "insert into scores(player_name, player_score, player_type) values ('$playerName', '$playerScore', '$playerType');"; 
            $result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
        } 
?>