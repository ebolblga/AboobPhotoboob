# Aboob Photoboob
## High end image editing software on C# WinForms

### Functionality:

1. Import image/folder of images
2. Blend modes:
  •Normal
  •Addition
  •Multiply
  •Average
  •Darken (min)
  •Lighten (max)
  •Mask
3. Transparency slider
4. Switch between channels RGB / R / G / B / Brightness
5. Binarization algorythms:
  •Gavrilov's method
  •Otsu's method
  •Niblack's method
  •Sauvola's method
  •Wulff's method
  •Bradley's method
  •Slider method
6. JPEG compression filter
7. Dark/Light theme
8. Histogram RGB / R / G / B / Brightness
  
  
  
  
  
  
Optimizations on rendering 5 layers:
bitmap 9.1сек 100%
byte[] 0.28сек 3.077%
parallel 0.22сек 2.417%
