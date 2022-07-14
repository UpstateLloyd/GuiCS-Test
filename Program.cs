using GuiCs.Models;
using GuiCs.Services;
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

            //Change terminal colors to green text on black background
            Colors.Base.Normal = Application.Driver.MakeAttribute(Color.Green, Color.Black);
            var top = Application.Top;

            //Adding generic api service/model for testing
            IPService _ipservice = new IPService();
            IPAddressModel _ipaddress = new IPAddressModel();

            //This label appears outside the window
            var login = new Label("Login: ") { X = 0, Y = 0 };
            //added directly to the top level
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
            
            var d = new Dialog("Dialog")
            {
                X = Pos.Percent(40),
                Y = Pos.Percent(40),
                Width = 15,
                Height = 16,                
            };
            
            var dExit = new Button("Exit") { X = 1, Y = 2 };
            dExit.Clicked += () => top.Remove(d);
            d.Add(dExit);      

            //button 2 click event closes application
            Button2.Clicked += () => Application.RequestStop();

            var OK = new Button("Ok")
            {
                X = 3,
                Y = 14
            };

            OK.Clicked  += () => top.Add(d); 

            win.Add(
                Button1,Button2,
                OK,
                new Button(10, 14, "Cancel")
            );

            var text = new TextField("Yo") { X = 1, Y = 1, Width = Dim.Fill() };
            var text2 = new Label("Yo") { X = 1, Y = 2, Width = Dim.Fill() };

            win2.Add(
                text,
                text2             
                );

            //Button 1 click event overrides text in text field and inserts "Hello"
            Button1.Clicked += async () =>
            {
                //when button clicked call api
                _ipaddress = await _ipservice.GetIPAsync();

                //showing difference between using label and text field for inserting text
                text2.Text = _ipaddress.IP;
                text.DeleteAll();
                text.InsertText(_ipaddress.IP);
            };
            var close = new Button("Close");
            close.Clicked += () => Application.RequestStop();
            var container = new Dialog("KeyDown", 80, 20, close)
            {
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            //Example updating a secondary window based on inputs in primary window
            //As the user cycles through available buttons the NextButton function tracks the next value from the "Buttons" list
            var Buttons = new List<string>() { "A generic greeting", "A generic farewell", "Approve", "Disapprove" };

            int currentPosition = 0;

            string NextButton(string direction)
            {                
                switch(direction)
                {
                    case "Down":
                        if (currentPosition == Buttons.Count - 1)
                        {
                            currentPosition = 0;
                        }
                        else
                        {
                            currentPosition += 1;
                        }
                        break;
                    case "Up":
                        if (currentPosition == 0)
                        {
                            currentPosition = Buttons.Count - 1;
                        }
                        else
                        {
                            currentPosition -= 1;
                        }
                        break;
                }    
                return Buttons[currentPosition];
            }

            var list = new List<string>();
            list.Add("A generic greeting");
            var listView = new ListView(list)
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill() - 1,
                Height = Dim.Fill() - 2
            };
            listView.ColorScheme = Colors.TopLevel;
            container.Add(listView);

            void KeyDownPressUp(KeyEvent keyEvent, string updown)
            {
                const int ident = -5;                
                switch (updown.Split(new Char[] {'-'}, StringSplitOptions.RemoveEmptyEntries)[1])
                {
                    case "CursorDown":
                        list.Clear();
                        list.Add(NextButton("Down"));
                        break;
                    case "CursorRight":
                        list.Clear();
                        list.Add(NextButton("Down"));
                        break;
                    case "CursorUp":
                        list.Clear();
                        list.Add(NextButton("Up"));
                        break;
                    case "CursorLeft":
                        list.Clear();
                        list.Add(NextButton("Up"));
                        break;
                }
            }
            
            win.KeyDown += (e) => KeyDownPressUp(e.KeyEvent, e.KeyEvent.ToString());

            //win2.Add(container);

            Application.Run();
        }
    }
}