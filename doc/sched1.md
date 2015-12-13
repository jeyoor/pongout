PongOut
====
====

Version 1 Schedule
====


Sprint 1
----
----

2015-12-03 pm
   * Establish second player class
   * Establish default second player controls

Sprint 2
----
----

2015-12-10 am

   * Establish lives for second player
   * Establish basic "game start" and "game over" screens

Sprint 3
----
----

2015-12-12 pm

   * Implement score for second player
   * Refactor start game UI two Player 1 col and Player 2 col
   * Implement a basic linear computer opponent
   * figure out "end of level" logic (rebuild new level & increase speed)
   * Implement score sharing (include opponent type)
   * Hi score submission graphing
   * Submit a first draft


Sprint 4
----
----

2015-12-15 pm

   * Wrap up functionality
   * Sound effects
   * get better graphics, fonts, colors
   * Write up user-guide documentation
   * Submit second draft


Future work
----
----

   * Automated test suite + TravisCI
   * Implement advanced opponent(s)
      * Implement a physics-based opponent (see if it can win even at lower speeds)
      * Implement a neuroevolution-based opponent
      * Bayesian?
   * Minor fixups
      * Use password json file along with unity text assets  http://docs.unity3d.com/ScriptReference/TextAsset.html
      * Switch web code to sha1? http://stackoverflow.com/questions/28858226/sha1-hash-is-different-in-the-unity3d-editor-and-on-the-iphone6-device