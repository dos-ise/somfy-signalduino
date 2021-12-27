using smARTsoftware.SomfyRtsLib;
using System;
using System.IO.Ports;
using System.Threading;

namespace smARTsoftware.SomfyRts
{
  class Program
  {
    private const string cLight = "Light";

    static void Main(string[] args)
    {


      string[] ports = SerialPort.GetPortNames();

      Console.WriteLine("Please enter number of COM Port to connect");
      Console.WriteLine("The following serial ports were found:");

      int i = 0;
      // Display each port name to the console.
      foreach (string port in ports)
      {
          Console.WriteLine(++i + ": " + port);
      }

      var comPortNumber = Console.ReadKey();
      if(!int.TryParse(comPortNumber.KeyChar.ToString(), out int n) || n > ports.Length)
      {
          Console.WriteLine("Invalid Selection!");
          return;
      }

      string dev = ports[--n];

      if (args.Length > 0 && args[0].StartsWith("/"))
      {
          dev = args[0];
      }

      SomfyRtsController controller = SomfyRtsController.CreateFromFile();
      controller.SignalDuinoAddress = dev;
      do
      {
        var key = Console.ReadKey();
        switch(key.Key)
        {
          case ConsoleKey.A:
            controller.AddDevice(cLight, 180004);break;
          case ConsoleKey.P: 
            controller.SendCommand(cLight, SomfyRtsButton.Prog);break;
          case ConsoleKey.U:
            controller.SendCommand(cLight, SomfyRtsButton.Up);break;
          case ConsoleKey.D:
            controller.SendCommand(cLight, SomfyRtsButton.Down); break;
          case ConsoleKey.F:
            controller.SendCommand(cLight, SomfyRtsButton.My); break;
          case ConsoleKey.C:
            controller.Close(); break;
          case ConsoleKey.O:
            controller.Open(); break;
        }
        controller.Save();    
      } while (true);

    }
  }
}
