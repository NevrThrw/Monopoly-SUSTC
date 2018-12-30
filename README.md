# Monopoly-Sustc
  
  We implement online function and can play through LAN by inputint server ip address.The game supports at most four players.
  
  
## Begin Scene
  The Screen Shoot of the beginning scene.
  ![Begin.PNG](
        https://github.com/NevrThrw/Monopoly-SUSTC/blob/master/ScreenShoots/Begin.PNG
      )

## Play Scene
  The Screen Shoot of the main playing scene.
  ![Play.PNG](https://github.com/NevrThrw/Monopoly-SUSTC/blob/master/ScreenShoots/Play.PNG)

## Function implementation
  
  ### Map
    We try our best to build the game map as the same as the real structure of SUSTC.We have teaching buildings,researching buildings,LyChi,Joyland,etc.And we add hills and a small lake to imitate the terrian of SUSTC.
  ### AI
    We implement AI to help or replace you playing game, you can just activate the function and watch how AI plays game.(The logic of AI is very simple :-))
  ### Character Behaviors
    The character behaviors are very similar to tranditional monopoly game,we just transform them to fit the theme of school life.We have study,research(aviliable after you are grade three),relax,go out,part-time jobbing.We also have special card that offer buff to characters.
  ### Music
    We add music to begin,main,end scenes.All the three pieces of music are from WOW.
  ### Online Playing
    We implement the online playing function by the Unet component of Unity,which can realize the LAN connection.But the delay is a bit large so you can notice obvious out-sync.
