# Aboob Photoboob
## High end image editing software on C# WinForms
![image](https://user-images.githubusercontent.com/82185066/161241798-f5b9a86c-8a4d-4ec1-b142-b04fbadf8152.png)
### Functionality:

1. Import image/folder of images, delete them from project, save
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
7. Histogram RGB / R / G / B / Brightness
8. Dynamic curve

### Decorations:

1. Dark/Light theme
2. Can hide some windows
  
  
  
  
  
  
### Optimizations v1-3 on rendering 5 layers:
| fdfdf | vvfd |
| "---" | ---" |
| fdf | fdf |
Bitmap &emsp; 9.1 sec &emsp; 100%

byte[] &emsp; 0.28 sec &emsp; 3.077%

byte[] + parallel &emsp; 0.22 sec &emsp; 2.417%
