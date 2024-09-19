/*	Event Manager - Scriptable Objects
 *	
 *	Package includes a rudimentary event manager ("Event Manager" folder), the scripts to create, raise, and 
 *	listen ("Game Event Scripts" Folder) and a test event Scriptable Object ("Event Scriptable Objects" folder).
 *	
 *	The InspectorButtonAttribute script allows for the creation of a debug button in the inspector to raise the event
 *	from within the inspector.
 *	
 *	1. Right click and create the event that will be used using the "Game Event" Object.
 *	2. Place the "EventRaiser" Script on  the object in the scene that will raise the event and place the event(s)
 *	   that will be raised into the list.
 *	3. Place the "EventListener" Script on the object that will respond to the method call and setup the
 *	   recquired responses.
 */