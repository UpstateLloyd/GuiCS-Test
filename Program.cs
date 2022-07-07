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
                // width is 25%
                Width = Dim.Percent(25f),
                Height = Dim.Fill()
            };

            //second window on right half of screen
            var win2 = new Window("MySlap")
            {
                //position on right side of win
                X = Pos.Right(win),
                Y = 1,

                //width dimension is 75%
                Width = Dim.Percent(75f),
                Height = Dim.Fill()
            };
            top.Add(win, win2);

            var Button1 = new Button("Hello") { X = 3, Y = 2 };
            var Button2 = new Button("Goodbye") { 
                X = Pos.Left(Button1), 
                Y = Pos.Top(Button1) + 1,                
            };

            //button 2 click event closes application
            Button2.Clicked += () => Application.RequestStop();
                       
            win.Add(
                Button1,Button2,
                new Button(3, 14, "Ok"),
                new Button(10, 14, "Cancel")
            );

            var text = new TextField("Yo") { X = 1, Y = 1, Width = Dim.Fill() };

            win2.Add(
                text
                ); ;

            //Button 1 click event overrides text in text field and inserts "Hello"
            Button1.Clicked += () =>
            {
                text.DeleteAll();
                text.InsertText("Hello");
            };

            Application.Run();
        }
    }
}
