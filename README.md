# highasakite
This is a game about flying a kite, with wiimotes. The aim for the player is to get it real high.

## Setup Instructions
### Bluetooth
1. Install [Bluesoleil 6 driver](https://github.com/indusninja/highasakite/releases/download/v1.0/highasakite-BluetoothDriver.rar)
1. Plug in bluetooth dongle.
1. Start the Bluesoleil client.
1. The first time you insert the bluetooth dongle, Bluesoleil client will ask to register the device. Click `OK` on the screen that appears.
1. Turn the wii-remote discoverable, by holding down buttons 1 and 2 on the wiimote at the same time. (the 4 LED lights on the wiimote will flash to symbolize that its in discovery mode).
1. Double-click the orange button to check for bluetooth devices in range or press <kbd>F5</kbd> on the keyboard.
1. The wiimote should show up as `Nintendo RVL-CNT4-01`.
1. Double-click on the icon of the wiimote, the icon will turn yellow.
1. Right click the wiimote icon in the bluesoleil window, select `connect` and `bluetooth human interface device service`.
1. Done! (to disconnect, turn off wiimote by pressing its power button)

### Game
1. Install [XNA Redistributable](https://www.microsoft.com/en-us/download/details.aspx?id=20914).
1. Run `High_as_a_Kite.exe`.
1. To quit game, press <kbd>Esc</kbd>.

## How To Play
1. At the instruction menu, press <kbd>A</kbd> on the Wii-Remote to start the game.
1. You start controlling the kite mid-air, by pulling the wiimote towards yourself, or any direction for that matter, will make the kite move in the direction the kite is pointing.
1. The wind will make the kite move in different directions, and the player is supposed to time the wiimote movement, so the kite moves in the desired direction.
1. If the kite falls too low towards the horizon, you will fail the game and end up on the "you lose" screen. Press <kbd>A</kbd> to return to the instructions menu.
1. The goal is to try to get the Kite as high as possible, by reaching the stars you will win the game, and end up on the `you win` screen. Press <kbd>A</kbd> to return to the instructions menu.
