using Microsoft.Maui.Controls;

namespace DentalApp.Controls;

// public class ZoomImageView : Grid
// {
//     private readonly Image _image;
//
//     private double currentScale = 1;
//     private double startScale = 1;
//
//     private double xOffset;
//     private double yOffset;
//     
//     public event Action<bool>? ZoomStateChanged;
//
//     public static readonly BindableProperty SourceProperty =
//         BindableProperty.Create(
//             nameof(Source),
//             typeof(ImageSource),
//             typeof(ZoomImageView),
//             default(ImageSource),
//             propertyChanged: OnSourceChanged);
//     
//     public static readonly BindableProperty IsZoomedProperty =
//         BindableProperty.Create(
//             nameof(IsZoomed),
//             typeof(bool),
//             typeof(ZoomImageView),
//             false,
//             BindingMode.TwoWay);
//
//     public bool IsZoomed
//     {
//         get => (bool)GetValue(IsZoomedProperty);
//         set => SetValue(IsZoomedProperty, value);
//     }
//
//     public ImageSource Source
//     {
//         get => (ImageSource)GetValue(SourceProperty);
//         set => SetValue(SourceProperty, value);
//     }
//
//     public ZoomImageView()
//     {
//         BackgroundColor = Colors.Black;
//
//         _image = new Image
//         {
//             Aspect = Aspect.AspectFit
//         };
//
//         Children.Add(_image);
//
//         var pinch = new PinchGestureRecognizer();
//         pinch.PinchUpdated += PinchUpdated;
//
//         var pan = new PanGestureRecognizer();
//         pan.PanUpdated += PanUpdated;
//
//         GestureRecognizers.Add(pinch);
//         GestureRecognizers.Add(pan);
//     }
//
//     private static void OnSourceChanged(
//         BindableObject bindable,
//         object oldValue,
//         object newValue)
//     {
//         var control = (ZoomImageView)bindable;
//
//         control._image.Source = (ImageSource)newValue;
//     }
//
//     private void PinchUpdated(object? sender, PinchGestureUpdatedEventArgs e)
//     {
//         switch (e.Status)
//         {
//             case GestureStatus.Started:
//
//                 startScale = _image.Scale;
//
//                 AnchorX = 0;
//                 AnchorY = 0;
//
//                 break;
//
//             case GestureStatus.Running:
//
//                 currentScale = Math.Max(1, startScale * e.Scale);
//                 ZoomStateChanged?.Invoke(currentScale <= 1.01);
//
//                 _image.Scale = currentScale;
//
//                 break;
//
//             case GestureStatus.Completed:
//
//                 xOffset = _image.TranslationX;
//                 yOffset = _image.TranslationY;
//
//                 break;
//         }
//         IsZoomed = currentScale > 1.01;
//     }
//
//     private void PanUpdated(object? sender, PanUpdatedEventArgs e)
//     {
//         if (currentScale <= 1)
//             return;
//
//         switch (e.StatusType)
//         {
//             case GestureStatus.Running:
//
//                 _image.TranslationX = xOffset + e.TotalX;
//                 _image.TranslationY = yOffset + e.TotalY;
//
//                 break;
//
//             case GestureStatus.Completed:
//
//                 xOffset = _image.TranslationX;
//                 yOffset = _image.TranslationY;
//
//                 break;
//         }
//     }
// }
public class ZoomImageView : Grid
{
    private readonly Image _image;
    private readonly PanGestureRecognizer _pan;
 
    private double currentScale = 1;
    private double startScale = 1;
 
    private double xOffset;
    private double yOffset;
   
    public event Action<bool>? ZoomStateChanged;
 
    public static readonly BindableProperty SourceProperty =
        BindableProperty.Create(
            nameof(Source),
            typeof(ImageSource),
            typeof(ZoomImageView),
            default(ImageSource),
            propertyChanged: OnSourceChanged);
   
    public static readonly BindableProperty IsZoomedProperty =
        BindableProperty.Create(
            nameof(IsZoomed),
            typeof(bool),
            typeof(ZoomImageView),
            false,
            BindingMode.TwoWay,
            propertyChanged: OnIsZoomedChanged);
 
    public bool IsZoomed
    {
        get => (bool)GetValue(IsZoomedProperty);
        set => SetValue(IsZoomedProperty, value);
    }
 
    public ImageSource Source
    {
        get => (ImageSource)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
 
    public ZoomImageView()
    {
        BackgroundColor = Colors.Black;
 
        _image = new Image
        {
            Aspect = Aspect.AspectFit
        };
 
        Children.Add(_image);
 
        var pinch = new PinchGestureRecognizer();
        pinch.PinchUpdated += PinchUpdated;
 
        _pan = new PanGestureRecognizer();
        _pan.PanUpdated += PanUpdated;
 
        GestureRecognizers.Add(pinch);
    }
 
    private static void OnIsZoomedChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (ZoomImageView)bindable;
        var zoomed = (bool)newValue;
 
        if (zoomed)
        {
            if (!control.GestureRecognizers.Contains(control._pan))
                control.GestureRecognizers.Add(control._pan);
        }
        else
        {
            control.GestureRecognizers.Remove(control._pan);
 
            control._image.TranslationX = 0;
            control._image.TranslationY = 0;
            control.xOffset = 0;
            control.yOffset = 0;
        }
    }
 
    private static void OnSourceChanged(
        BindableObject bindable,
        object oldValue,
        object newValue)
    {
        var control = (ZoomImageView)bindable;
 
        control._image.Source = (ImageSource)newValue;
    }
 
    private void PinchUpdated(object? sender, PinchGestureUpdatedEventArgs e)
    {
        switch (e.Status)
        {
            case GestureStatus.Started:
 
                startScale = _image.Scale;
 
                AnchorX = 0;
                AnchorY = 0;
 
                break;
 
            case GestureStatus.Running:
 
                currentScale = Math.Max(1, startScale * e.Scale);
                ZoomStateChanged?.Invoke(currentScale <= 1.01);
 
                _image.Scale = currentScale;
 
                break;
 
            case GestureStatus.Completed:
 
                xOffset = _image.TranslationX;
                yOffset = _image.TranslationY;
 
                break;
        }
        IsZoomed = currentScale > 1.01;
    }
 
    private void PanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        if (currentScale <= 1)
            return;
 
        switch (e.StatusType)
        {
            case GestureStatus.Running:
 
                _image.TranslationX = xOffset + e.TotalX;
                _image.TranslationY = yOffset + e.TotalY;
 
                break;
 
            case GestureStatus.Completed:
 
                xOffset = _image.TranslationX;
                yOffset = _image.TranslationY;
 
                break;
        }
    }
}