[![NuGet](http://img.shields.io/nuget/v/ColorPicker.Xamarin.Forms.svg)](https://www.nuget.org/packages/ColorPicker.Xamarin.Forms/)

![](https://github.com/vpapenko/ColorPicker/workflows/Tests/badge.svg)

# ColorPicker
ColorPicker is the pack of color pickers for Xamarin Forms.

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/TestApp.gif" width="400">

# Supported platforms
- Android: Supported.
- iOs: Comming soon.

# Clor pickers
ColorWheel

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/ColorWheel.png" width="300">


HSLSliders

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/HSLSliders.png" width="300">


RGBSliders

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/RGBSliders.png" width="300">


This library contains several base classes for custom color pickers.


# TestApp
TestApp provides basic examples of how to use color pickers.

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/TestApp.png" width="300">

# How to use
Add this package both to Xamarin Forms and platform-specific projects.

Add color picker as any other Xamarin Forms control.

## Useful properties

### Common properties
**SelectedColor.** It could be used to read selected color and to set the color to picker.
  
**ConnectedColorPicker.** Multiple color picker could be connected using this property. Bind color pickers one by one in chain.

**ShowAlphaSlider.**

### Color wheel
**ShowLuminosityWheel**

**ShowLuminositySlider**
