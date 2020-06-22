using System;
using System.Linq;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using ColorPicker.Effects;
using ColorPicker.UWP.Effects;

[assembly: ResolutionGroupName("ColorPickerPlatformEffect")]
[assembly: ExportEffect(typeof(ColorPickerTouchEffectUWP), "ColorPickerTouchEffect")]

namespace ColorPicker.UWP.Effects
{
    public class ColorPickerTouchEffectUWP : PlatformEffect
    {
        FrameworkElement frameworkElement;
        ColorPickerTouchEffect effect;
        Action<Element, ColorPickerTouchActionEventArgs> onTouchAction;

        protected override void OnAttached()
        {
            // Get the Windows FrameworkElement corresponding to the Element that the effect is attached to
            frameworkElement = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            effect = (ColorPickerTouchEffect)Element.Effects.
                        FirstOrDefault(e => e is ColorPickerTouchEffect);

            if (effect != null && frameworkElement != null)
            {
                // Save the method to call on touch events
                onTouchAction = effect.OnTouchAction;

                // Set event handlers on FrameworkElement
                frameworkElement.PointerEntered += OnPointerEntered;
                frameworkElement.PointerPressed += OnPointerPressed;
                frameworkElement.PointerMoved += OnPointerMoved;
                frameworkElement.PointerReleased += OnPointerReleased;
                frameworkElement.PointerExited += OnPointerExited;
                frameworkElement.PointerCanceled += OnPointerCancelled;
            }
        }

        protected override void OnDetached()
        {
            if (onTouchAction != null)
            {
                // Release event handlers on FrameworkElement
                frameworkElement.PointerEntered -= OnPointerEntered;
                frameworkElement.PointerPressed -= OnPointerPressed;
                frameworkElement.PointerMoved -= OnPointerMoved;
                frameworkElement.PointerReleased -= OnPointerReleased;
                frameworkElement.PointerExited -= OnPointerEntered;
                frameworkElement.PointerCanceled -= OnPointerCancelled;
            }
        }

        void OnPointerEntered(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, ColorPickerTouchActionType.Entered, args);
        }

        void OnPointerPressed(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, ColorPickerTouchActionType.Pressed, args);

            // Check setting of Capture property
            if (effect.Capture)
            {
                (sender as FrameworkElement).CapturePointer(args.Pointer);
            }
        }

        void OnPointerMoved(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, ColorPickerTouchActionType.Moved, args);
        }

        void OnPointerReleased(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, ColorPickerTouchActionType.Released, args);
        }

        void OnPointerExited(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, ColorPickerTouchActionType.Exited, args);
        }

        void OnPointerCancelled(object sender, PointerRoutedEventArgs args)
        {
            CommonHandler(sender, ColorPickerTouchActionType.Cancelled, args);
        }

        void CommonHandler(object sender, ColorPickerTouchActionType touchActionType, PointerRoutedEventArgs args)
        {
            PointerPoint pointerPoint = args.GetCurrentPoint(sender as UIElement);
            Windows.Foundation.Point windowsPoint = pointerPoint.Position;

            onTouchAction(Element, new ColorPickerTouchActionEventArgs(args.Pointer.PointerId,
                                                            touchActionType,
                                                            new Point(windowsPoint.X, windowsPoint.Y),
                                                            args.Pointer.IsInContact));
        }
    }
}