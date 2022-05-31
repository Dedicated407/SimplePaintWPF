using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimplePaintWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    #region Properties

    private ColorRGB ColorRGB { get; set; }
    private Color Color { get; set; }

    #endregion

    public MainWindow()
    {
        InitializeComponent();
        ColorRGB = new ColorRGB();
        lbl1.Background = new SolidColorBrush(Color.FromRgb(ColorRGB.Red, ColorRGB.Green, ColorRGB.Blue));
    }

    private void btn_Save_Click(object sender, RoutedEventArgs e)
    {
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        dlg.FileName = "image"; // Имя по умолчанию
        dlg.DefaultExt = ".jpg"; // Тип файла
        dlg.Filter = "Image (.jpg)|*.jpg"; 

        bool? result = dlg.ShowDialog();
        
        if (result == true)
        {
            // Сохранение
            string filename = dlg.FileName;
            int width = (int)inkCanvas.ActualWidth;
            int height = (int)inkCanvas.ActualHeight;
            
            // Рендер картинки
            RenderTargetBitmap rtb =
                new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
            rtb.Render(inkCanvas);

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                encoder.Save(fs);    
            }
        }
    }
    
    private void btn_Select_Click(object sender, RoutedEventArgs e)
    {
        inkCanvas.EditingMode = InkCanvasEditingMode.Select;
    }

    private void btn_Draw_Click(object sender, RoutedEventArgs e)
    {
        inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
    }

    private void btn_Erase_Click(object sender, RoutedEventArgs e)
    {
        inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
    }

    private void sld_Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        var slider = sender as Slider;
        string crlName = slider.Name; //Определяем имя контрола, который покрутили
        double value = slider.Value; // Получаем значение контрола
        
        //В зависимости от выбранного контрола, меняем ту или иную компоненту и переводим ее в тип byte
        
        if (crlName.Equals("SldRedColor")) 
        {
            ColorRGB.Red = Convert.ToByte(value);
        }
        if (crlName.Equals("SldGreenColor"))
        {
            ColorRGB.Green = Convert.ToByte(value);
        }
        if (crlName.Equals("SldBlueColor"))
        {
            ColorRGB.Blue = Convert.ToByte(value);
        }

        //Задаем значение переменной класса clr 
        Color = Color.FromRgb(ColorRGB.Red, ColorRGB.Green, ColorRGB.Blue);
        //Устанавливаем фон для контрола Label 
        lbl1.Background = new SolidColorBrush(Color.FromRgb(ColorRGB.Red, ColorRGB.Green, ColorRGB.Blue));
        // Задаем цвет кисти для контрола InkCanvas
        inkCanvas.DefaultDrawingAttributes.Color = Color;
    }
    
    private void btn_Size_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        string name = button.Name;

        if (name.Equals("btnSizeUp") && inkCanvas.DefaultDrawingAttributes.Width < 100)
        {
            inkCanvas.DefaultDrawingAttributes.Width++;
            inkCanvas.DefaultDrawingAttributes.Height++;
        }

        if (name.Equals("btnSizeDown") && inkCanvas.DefaultDrawingAttributes.Width > 2)
        {
            inkCanvas.DefaultDrawingAttributes.Width--;
            inkCanvas.DefaultDrawingAttributes.Height--;
        }
    }
}