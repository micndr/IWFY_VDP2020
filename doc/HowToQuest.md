# Quest System

## Quest Lock Component
* has a quest state integer.
* if global state == local state, then it's activated.
* has optional next state, it's used in main when activated by some source to update the global state.
* has a list of item requiremets.

## Quest Main
* activates / deactivates components based on state.
* when a popup or some other source (trigger collider for example) activates a questlock, 
* it checks the requirements of that lock. If satisfied, the items are used and the state
* incremented set to lock.nextstate

## Item Pickup
* has a list of items.
* when activated, the items are transferred to the invetory.