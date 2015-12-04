PongOut
====
====

Version 1 Spec
====

By Jeyan Oorjitham

PongOut is a game for learning about and gathering data from different machine learning algorithms.

This spec is incomplete and only applies to the version of PongOut to be submitted for CSCI-583B (Fall 2015).

Open Issue
----
Is the name <i>PongOut</i> currently taken? What other options are available? MindBreak? HiveBounce?

Marketing note
----
Icons and screenshots need to connote Pong...

Scenarios
----
----

Bobby
----
Bobby is a 15 year old high school student. He enjoys playing Android games on his phone while he's goofing off in class. While browsing the Play Store one day,

Suzie
----
Suzie is a 37 year old PhD. She's mentally exhausted and desperate to score a few points on her next Student Opinion Survey. Stumbling across a Web page demonstrating both the use of PongOut and the reams of score data it provides, she hastily scrribles together a quick lab exercise for her undergraduate Intro to Computing Class before falling asleep at her desk. The lab exercise is a hit with the students, who can both learn about basic Machine Learning algorithms and copy-paste the charts and graphs into their term papers to take up extra blank space.

Nongoals
----
----

   * The first version will have no help screen. Users will be expected to figure the game out.

   * The first version of the game will not have automated unit testing.

   * The first version of the game will not have a website.

   * The first version of the game will not support powerups.

   * The first version of the game will not explain its machine learning algorithms.

Outline
----
----

The outline below gives an incomplete, high-level picture of the game screens. 

1. Welcome

2. New Game

3. Gameplay

4. Game Over (Score Submission)

The *Welcome* screen displays some fancy art and a New Game button.

Marketing Note
----
Try to find a playful comic book font for dialogs.

The *New Game* screen allows the player to choose an opponent. The opponent can be either a second human player or algorithm. Algorithms have a name and a difficulty rating (in stars). Algorithms are not explained.
Once the player has chosen an opponent, play begins.

The *Gameplay* screen puts player 1 at the bottom and the opponent at the top.

   * Players can move their paddles and bounce the ball toward the procedurally generated bricks in the center.
   * Destroying a blue brick gives the player one point.
   * Destroying a red brick gives the player two points.

Players attempt to defend their goal (he area behind their paddle). If the ball hits their goal, the player loses a life and the opposing player receives ten points.

Once either player's lives are exhausted, the game is over.

The *Game Over* screen gives the players the option to upload their high score to a web-based high score database.

The window will present this message: "Thank you for playing! Would you like to upload your score?" with a "Yes" or "No" button.
If the player chooses not to upload their score, the *New Game* dialog will display.

If the player chooses to upload their score, a *Progress Bar* dialog will display as their data uploads.

The player will have the option to cancel their upload.
Once the upload is complete, a *Thank You* dialog will display.

