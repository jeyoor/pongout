<?php 

    $passwordString = file_get_contents("database.json");
    $passwordObj = json_decode($passwordString, true);

    $db = mysql_connect($passwordObj['host'], $passwordObj['user'], $passwordObj['password']) or die('Could not connect: ' . mysql_error()); 
    mysql_select_db($passwordObj['database']) or die('Could not select database');

    // Strings must be escaped to prevent SQL injection attack. 
    $playerOneName = mysql_real_escape_string($_GET['player_one_name'], $db); 
    $playerOneScore = mysql_real_escape_string($_GET['player_one_score'], $db); 
    $hash = $_GET['hash']; 

    $secretKey=$passwordObj['secret']; # Change this value to match the value stored in the Unity client code

    $realHash = md5($playerOneName . $playerOneScore . $secretKey); 
    if($realHash == $hash) { 
        // Send variables for the MySQL database class. 
        $query = "insert into scores(player_one_name, player_one_score, player_one_type, player_two_name, player_two_score, player_two_type) values ('$playerOneName', '$playerOneScore', '$playerOneType', '$playerTwoName', '$playerTwoScore', '$playerTwoType');"; 
        $result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
    } 
?>