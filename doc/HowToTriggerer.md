# How to Triggerer

## Triggerer
It's purpose is to activate the components when an input is given. It generalizes the need to have multiple specific calls to progress the story.
* when triggered, triggers the components assigned.
* it can autotrigger without input -> used for first dialogue
* it can autodetect the component attatched to the same gameobject for lazyness
* if a component is set, it's not autodetected.
* trigger can be called from anywhere

## Key Trigger
* triggers a triggerer when a key is pressed

## Volume Trigger
* triggers a triggerer when a volume is entered by a rigidbody (not yet filtered)

# How to use them
see worldhub scene as reference.