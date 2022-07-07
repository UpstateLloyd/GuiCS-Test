using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace GuiCs_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            var top = Application.Top;

            var login = new Label("Login: ") { X = 0, Y = 0 };
            top.Add(login);

            var win = new Window("MyApp")
            {
                X = 0,
                Y = 1, // Leave one row for the toplevel menu

                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Percent(50f),
                Height = Dim.Fill()
            };
            var win2 = new Window("MySlap")
            {
                X = Pos.Right(win),
                Y = 1,

                Width = Dim.Percent(50f),
                Height = Dim.Fill()
            };
            top.Add(win, win2);

            var Button1 = new Button("Hello") { X = 3, Y = 2 };
            var Button2 = new Button("Goodbye") { X = Pos.Left(Button1), Y = Pos.Top(Button1) + 1 };

            win.Add(
                //Button1,Button2,
                new Button(3, 14, "Ok"),
                new Button(10, 14, "Cancel")
            );

            win2.Add(Button1 , Button2);

            

            Application.Run();
        }
    }
}
