CREATE TABLE scores (
   id INT(10) UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
   player_one_name VARCHAR(150) NOT NULL DEFAULT 'anonymous',
   player_one_score INT(10) UNSIGNED NOT NULL DEFAULT '0',
   player_one_type VARCHAR(150) NOT NULL DEFAULT 'Manual',
   player_two_name VARCHAR(150) NOT NULL DEFAULT 'anonymous',
   player_two_score INT(10) UNSIGNED NOT NULL DEFAULT '0',
   player_two_type VARCHAR(150) NOT NULL DEFAULT 'Manual',
);