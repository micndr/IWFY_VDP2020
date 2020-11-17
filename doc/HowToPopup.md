# How to setup a clickable object with popup
 - Make a new script that inherits from IwfyClickableObject.cs
 - Override method OnPopupClick for any behaviour to be executed from the object itself.
 - Place the script on the object you with to generate a popup from.
 - Modify text message and background color from the inspector, under the script.

DONE! IwfyClickableObject manages the communication to the popup, and just calls OnPopupClick when the popup is clicked

External functionality
 - set QuestLock to a lock for the lock to be activated when the popup is pressed.
 - set ItemPickup to a itempickup instance to get the items when the popup is pressed.
