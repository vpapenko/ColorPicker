[![NuGet](http://img.shields.io/nuget/v/ColorPicker.Xamarin.Forms.svg)](https://www.nuget.org/packages/ColorPicker.Xamarin.Forms/)

![](https://github.com/vpapenko/ColorPicker/workflows/Tests/badge.svg)

[![CodeFactor](https://www.codefactor.io/repository/github/vpapenko/colorpicker/badge/master)](https://www.codefactor.io/repository/github/vpapenko/colorpicker/overview/master)

# ColorPicker
ColorPicker is the multi-platform pack of color pickers for Xamarin Forms.

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/TestApp.gif" width="400">

# Supported platforms
- Android
- iOs

# Clor pickers

```ColorWheel```: color circle with alpha slider and luminosity slider.

```ColorCircle```: only color circle.

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/ColorWheel.png" width="300">


```ColorTriangle```

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/ColorTriangle.png" width="300">


```HSLSliders```

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/HSLSliders.png" width="300">


```RGBSliders```

<img src="https://github.com/vpapenko/ColorPicker/blob/dev/Assets/RGBSliders.png" width="300">



This library contains several base classes for custom color pickers.


# TestApp
TestApp provides basic examples of how to use color pickers.

# How to use
Add this package both to Xamarin Forms and platform-specific projects.

For iOS project: add ```ColorPickerEffects.Init()``` to ```AppDelegate.FinishedLaunching```

Add color picker as any other Xamarin Forms control.

## Useful properties

### Common properties
**```SelectedColor```** It could be used to read selected color and to set the color to picker.
  
**```ConnectedColorPicker```** Multiple color pickers could be connected using this property. Bind color pickers one by one in chain.

**```WheelBackgroundColor```** For Color wheel and Color Triangle. Background inside outer ring.

**```Vertical```** Vertical slider.

**```ShowAlphaSlider```**

**```PickerRadiusScale```**

### Color wheel
**```ShowLuminosityWheel```**

**```ShowLuminositySlider```**
