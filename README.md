# Aboob Photoboob
## High end image editing software on C# Windows Forms App (.NET Framework)
![image](https://user-images.githubusercontent.com/82185066/162346693-05942bf3-da61-4af8-8528-b0efdcce6bdd.png)
### Functionality:

1. Import image/folder of images, delete them from project, save
2. Blend modes:<br />
  •Normal<br />
  •Addition<br />
  •Multiply<br />
  •Average<br />
  •Darken (min)<br />
  •Lighten (max)<br />
  •Mask
3. Transparency slider
4. Switch between channels RGB / R / G / B / Brightness
5. Binarization algorithms:<br />
  •Gavrilov's method<br />
  •Otsu's method<br />
  •Niblack's method<br />
  •Sauvola's method<br />
  •Wulff's method<br />
  •Bradley's method<br />
  •Slider method
6. JPEG compression filter
7. Histogram RGB / R / G / B / Brightness
8. Dynamic curve

### Decorations:

1. Dark/Light theme
2. Can hide some windows
  
  
  
  
  
  #
> Optimizations on rendering 5 layers:

| Implamentation  | Time | Ratio |
| ------------- | ------------- | ------------- |
| Bitmap  | 9.1 sec  | 100%  |
| no switch in loop | 2.6 sec  | 28.57%  |
| byte[]  | 0.28 sec  | 3.08%  |
| byte[] + parallel  | 0.22 sec  | 2.42%  |
