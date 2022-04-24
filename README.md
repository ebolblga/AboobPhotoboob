# Aboob Photoboob
## "High end" image editing software on C# Windows Forms App (.NET Framework)
![image](https://user-images.githubusercontent.com/82185066/162630436-aa95d9ee-ccc6-4922-9af5-4883d7a4323e.png)
### Functionality:

1. Import image/folder of images, delete them from project, save (with watermark)
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
  - [x] •Average method<br />
  - [x] •Otsu's method<br />
  - [ ] •Niblack's method<br />
  - [ ] •Sauvola's method<br />
  - [ ] •Wulff's method<br />
  - [ ] •Bradley's method<br />
  - [x] •Slider method
  - [x] •My method (dynamic threshold while going through image)
6. JPEG compression filter
7. Histogram RGB / R / G / B / Brightness
8. Dynamic curve<br />
  •Rendered manually with math from wiki about cubic splines<br />
  •Can add and move points<br />
  •If 2 points have same X axis, only top one is added to rendering queue<br />
![ezgif-5-d4eb9d028c](https://user-images.githubusercontent.com/82185066/162632745-5ef4bffb-00e1-4832-93f7-163b4ef7281c.gif)
9. ASCII Filter
![image](https://user-images.githubusercontent.com/82185066/162750941-b4dd2244-9069-4729-b398-ff95708610e6.png)



### Decorations:

1. Can hide some windows
2. Additional curve markers
3. Dark/Light theme
![image](https://user-images.githubusercontent.com/82185066/162630450-43a9c3fb-7d27-4691-8df4-4e69d40bfc14.png)

  
  #
> Optimizations on rendering 5 layers:

| Implamentation  | Time | Ratio |
| ------------- | ------------- | ------------- |
| Bitmap  | 9.1 sec  | 100%  |
| no switch in loop | 2.6 sec  | 28.57%  |
| byte[]  | 0.28 sec  | 3.08%  |
| + parallel  | 0.22 sec  | 2.42%  |
| + pointers  | ?  | ?  |
| + release  | ?  | ?  |

## Dependencies
System.Drawing.Common by Microsoft

## License
This program is licensed under the GPL-3.0 License. Please read the License file to know about the usage terms and conditions.
