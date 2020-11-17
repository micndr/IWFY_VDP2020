# Details on the interaction between reticle pointer and clickable objects with popup
 - IwfyClickableObject.cs contains a private object[] called "id". It is intended as *all the information that the clickable object passes to the reticle pointer when the latest hovers on the former*.
  - id[0] -> String containing the name of the clickable object to be displayed in the pointer's incorporated tooltip
  - id[1] -> Color of the tooltip for that clickable object. Why is this useful? White in dark environments, dark in the lighted environments, zero computational cost.
  - ADD HERE IF YOU ADD ANY PARAMETER
  - Remember that the script that receives the data is IwfyReticlePointer.cs
  - *Remember to cast the object into its type:*
    - id[0].toString()
    - (Color)id[1]
    - Add here yours

NOTE IF YOU DO NOT NEED THE POPUP AND JUST NEED THE POINTER TO REACT TO AN OBJECT USE IwfyClickableObjectNoPopup.cs, which implements just the pointer reaction (IwfyClickableObject.cs and PopupLogic.cs inherit from this script)
