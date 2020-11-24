# Quest System

## Global State
This is the monolithic interdimensional variable holder.
* completedQuest list: when a quest is done, it adds itself to this list.
* onLoad: when a level is loaded, quest that are completed are set to their completed states.

## Quest Lock Component
It's linked to a QuestMain, it searches the scene if none is specified.
* has a quest state integer.
* if global state == local state, then it's activated.
* has optional next state, it's used in main when activated by some source to update the global state.
* has a list of item requiremets.
* invert: if true, the condition that checks activation is inverted. (It's used to make object appear after a certain state and remain active or deactivate an object for just one frame)

## Quest Main
activates / deactivates components based on state.
* nameQuest: every quest is named to ensure correct behavior on load level and to separate one progression line to another.
* stateNames is a map from state to the quest reminder text in the ui
* when a popup or some other source (trigger collider for example) activates a questlock, it checks the requirements of that lock. If satisfied, the items are used and the state incremented set to lock.nextstate

## Item Pickup
* has a list of items.
* when activated, the items are transferred to the invetory.