# CSGODrinkingGame
- [What do i need?](#what-do-i-need)
- [How to setup](#how-to-setup)
- [Common problems](#common-problems)

## What do i need?
(Don't worry for the links i provide i do **not** use any affiliate link)
1. Arduino (Nano)
1. Relay
1. [5V pump](https://www.amazon.de/gp/product/B07PNDTVS2/ref=ppx_yo_dt_b_asin_title_o00_s00?ie=UTF8&psc=1)
1. Some kind of ~5V powersupply (e.g. kabel or [battery box](https://www.amazon.de/gp/product/B0761VJMJJ/ref=ppx_yo_dt_b_asin_title_o08_s00?ie=UTF8&th=1))

## How to setup
1. Set up the Hardware
First you'll have to setup your Hardware. Therefor wireup an Arduino like follows:  
5V -> Relay 5V  
GND -> Relay Gnd  
D10* -> Relay Din  
    
   *Note that the pin for switching the arduino can be changed in the [Arduino script](https://github.com/MichaMican/CSGODrinkingGame/blob/daab39e428640dde0b8050e324d377751ba78d10/Arduino/app.ino#L1)
  ![circuit diagram](https://github.com/MichaMican/CSGODrinkingGame/blob/master/circuitDiagram.png?raw=true)
  
  Connect the powersupply and the pump so the relay is in serious and can act like a switch. Then dip the pump into the drink you want to have pumped. Note that you (obviously) can use this to trigger other events as well (You don't have to pump RobBubble into your mouth) e.g Triggering a light circuit, a motor, w/e realy the boundry is your imagination.
1. Flash the Arduino
Open the [Arduino/app.ino](https://github.com/MichaMican/CSGODrinkingGame/blob/master/Arduino/app.ino) in the Android IDE or in Visual Studio Code with the Android Plugin. Select your Arduino board type and port (remember the port identifier (COMXX) for later). Upload the sketch to the arduino.
1. Setup CSGO
Find your CSGO instalations folder then go to /csgo/cfg and copy and paste the [gamestate_integration_drinkGame.cfg](https://github.com/MichaMican/CSGODrinkingGame/blob/master/gamestate_integration_drinkGame.cfg) their.  
Note that this **isn't a cheat** and it **won't get detected as such**. I'm using an [official interface (gamestate integration)](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration) which provides the state of the player through http calls to a specified endpoint. For more information on the topic checkout the Valve developer docs on CSGO gamestate integration: https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration
1. Setup the appsettings  
Open the [NetCoreServer/compiledBinaries/appsettings.json](https://github.com/MichaMican/CSGODrinkingGame/blob/master/NetCoreServer/compiledBinaries/appsettings.json) and change the [ArduinoPort parameter](https://github.com/MichaMican/CSGODrinkingGame/blob/daab39e428640dde0b8050e324d377751ba78d10/NetCoreServer/compiledBinaries/appsettings.json#L10) to the port name the arduino is connected to (the identifier you remembered earlier). Note that if you shall connect your Arduino to a different port you will have to look up the new port name.  
*Optional: You can change the [thresholds](https://github.com/MichaMican/CSGODrinkingGame/blob/daab39e428640dde0b8050e324d377751ba78d10/NetCoreServer/compiledBinaries/appsettings.json#L11) where a drink penalty is triggered and the [max pump duration](https://github.com/MichaMican/CSGODrinkingGame/blob/daab39e428640dde0b8050e324d377751ba78d10/NetCoreServer/compiledBinaries/appsettings.json#L25). The time how long the relay, and therefore the pump, is activated is calculated by pupmDuration = pumpMultiplicator &ast; MaxPumpDuration*  
1. Start the NetcoreServer
execute the [NetCoreServer/compiledBinaries/CSGODrinkingGameServer.exe](https://github.com/MichaMican/CSGODrinkingGame/blob/master/NetCoreServer/compiledBinaries/CSGODrinkingGameServer.exe). If you don't trust me just open the [Code](https://github.com/MichaMican/CSGODrinkingGame/tree/master/NetCoreServer/CSGODrinkingGameServer) in Visual Studio or another C# IDE of your choice, check it 
thoroughly and compile it on your own. I recommend you to export it as a self contained application.
The application should open a cmd window and start.
1. Play the game
  Start csgo, queue into Matchmaking (or some other gamemode) and enjoy. Note: I only tested this in Wingman, (Competetive) Matchmaking and on Casual.

## Common problems
- Make sure that the port 4327 of localhost isn't used by another application. If you have something using that port, and closing the app or changing the port the app uses is not an option you may change the port of the Drinkinggame by changing the according values in the [gamestate_integration_drinkGame.cfg](https://github.com/MichaMican/CSGODrinkingGame/blob/daab39e428640dde0b8050e324d377751ba78d10/gamestate_integration_drinkGame.cfg#L3) and in the [NetCoreServer](https://github.com/MichaMican/CSGODrinkingGame/blob/daab39e428640dde0b8050e324d377751ba78d10/NetCoreServer/CSGODrinkingGameServer/Program.cs#L24). Note that you'll have to restart CSGO and recompile the NetCoreServer to apply the changes you've made.
