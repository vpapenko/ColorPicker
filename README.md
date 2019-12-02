[![NuGet](http://img.shields.io/nuget/v/ColorPicker.Forms.svg)](https://www.nuget.org/packages/ColorPicker.Forms/)

![](https://github.com/vpapenko/ColorPicker/workflows/Tests/badge.svg)

# ColorPicker
ColorPicker is the pack of color pickers for Xamarin Forms.

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/TestApp.png" width="400">

# Supported platforms
- Android: Supported.
- iOs: Comming soon.

# Ready to use controls
ColorWheel

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/ColorWheel.png" width="300">


HSLSliders

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/HSLSliders.png" width="300">


RGBSliders

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/RGBSliders.png" width="300">


AlphaSlider

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/AlphaSlider.png" width="300">


# TestApp
TestApp provides a basic examples of how to use color pickers.

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/TestApp.png" width="300">

# How to use
Add this package both to Xamarin Forms and platform-specific projects.

Add color picker as any other Xamarin Forms control.

### Useful properties:

**SelectedColor.** It could be used to read selected color and to set the color to picker.
  
**ConnectedColorPicker.** Multiple color picker could be connected using this property. Bind color pickers one by one in chain.

**ShowAlphaSwitch.** Applied to slider pickers. Turn alpha switch slider on and off. For ColorWheel use separate AlphaSlider

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/ShowAlphaSwitchFalse.png" width="300">  <img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/ShowAlphaSwitchTrue.png" width="300">
