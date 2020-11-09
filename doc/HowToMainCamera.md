# How to Correctly Setup the Main Camera

 - Make sure the component Main Camera is at the root of the Hierarchy.
 - GameObject > XR > Convert Main Camera To XR Rig
 - Make sure the following structure appeared
    - XRRig (contains CameraOffset.cs script)
      - Camera Offset
        - Main Camera (contains components Camera, Audio Listener, Tracked Pose Driver)
 - Delete any scripts on Main Camera, if any.
 - Add IwfyCameraPointer.cs script to Main Camera -> This is the script that makes the raycasting and sends events to the peeked object! The events are OnPointerEnter (when pointer sees object), OnPointerExit (when pointer stops seeing object) and OnPointerClick (when either Cardboard button, or mouse left button is clicked)
 - Add PCMapping.cs script to Main Camera (DISABLE THIS WHEN COMPILING FOR MOBILE) -> This enables the use of the mouse. The mouse left click is intercepted by IwfyCameraPointer.cs.
 - Add IwfyReticlePointer prefab as child to Main Camera.

 DONE!

 [See how to make an object interact with IwfyReticlePointer](HowToReticlePointer.md)

 ### TODO
 - Change the animation of the IwfyReticlePointer prefab
 - Tweak the timings of the animations transitions.
 - Understand why PCMapping.cs blocks android input
