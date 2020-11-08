# How to Correctly Make IwfyReticlePointer Interact with objects

 - Make sure [you configured Main Camera correctly](HowToMainCamera.md)
 - Add IwfyClickableObject.cs to the object you wish to make interactable
 - Link drag the IwfyReticlePointer onto the ReticlePointer property of the script
 - For every script that has to use the OnPointerEnter, OnPointerExit and OnPointerClick just inherit from IwfyClickableObject.cs!

 DONE! Now the reticle pointer will animate when pointing to a ClickableObject.cs and the object will receive the events sent by IwfyCameraPointer.cs!

 Not the most elegant solution I know, so if you modify the innerworkings of this please just update this document! :)
