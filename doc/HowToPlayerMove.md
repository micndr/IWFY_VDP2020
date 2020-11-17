# Player Movement and Gravity

Prerequisites: A GameObject in the scene with tag Planet and component GravityAttractor
Place a PlayerMove and a GravityBody component on the player object, as well as a rigidbody.
The reference to the camera is the object tagged MainCamera. (by Camera.main)

To setup other rigidbodies just attach to them the GravityBody component. This way they stick to the planet.