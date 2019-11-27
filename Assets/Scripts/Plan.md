## Plan for the level scripting
* Enum of events that can be triggered per level.
* Performing an action calls a performer function, taking an action enum.
* Switch on the enum for the actions to perform.
* Derive from a base performer for each level, to keep it tidier.