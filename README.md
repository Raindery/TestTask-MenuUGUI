# Task
Develop a two-level nested menu in Unity with the following specifications:
## Menu
- Initially, all menu items should be collapsed into a "burger" icon.
- When clicked, the data is fetched from a pseudo-API, then the items should expand.
- The menu should consist of two items named “Colors” and “Texts”.
- Each item contains sub-items that are not loaded initially. Upon selecting one item, the data for these sub-items should be fetched from a pseudo-API.
## Animation
- When the main menu and items are expanded, a smooth unfolding animation should play.
- Similarly, a corresponding animation should play when collapsing the menu or items.
## Control
- Each item should operate independently and can be collapsed/expanded by the user.
- When a sub-item is clicked, the text in the center of the screen should change its attributes according to the selection.
## Error handling
- Error Handling: If an error occurs, display a simple popup with the text “An error occurred.” 
- After closing the popup, the user can attempt to expand the item and request the data again.
## Implementation Requirements:
- Use Unity's built-in UI components (Canvas, Button, etc.).
- For animations, use the DoTween plugin.
- The text of the menu should be loaded through a service (API).
- All services should be implemented using ID containers via Zenject.
- Pay attention to the user experience, especially regarding the menu interactions and animations as the data of the menu is loaded dynamically.
# Detailed Description
## Pseudo-API 
This is a service that emulates an API, meaning in this task, it consists of two methods that return JSON responses with a 0.5- 2.5 -second delay. Request timeout is 1.5 seconds.
## JSON
### Colors
```json
{
  "colors": [
  {
    "name": "Red",
    "color": "C92C2C"
  },
  {
    "name": "Green",
    "color": "1C721E"
  },
  {
    "name": "Blue",
    "color": "2D6BC7"
  }
  ]
}
```
### Text
```json
{
  "texts": [
    "Text1",
    "Text2",
    "Text3"]
}
```

