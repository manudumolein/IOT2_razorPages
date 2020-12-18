using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Device.Gpio;


namespace MyApp.Namespace
{
    public class AboutModel : PageModel
    {
        private GpioController controller;

        public AboutModel(GpioController controller)
        {
            this.controller=controller;
            //input inlezen
            this.Input1 = (controller.Read(24)==PinValue.High);
            this.Input2 = (controller.Read(25)==PinValue.High);
            this.Input3 = (controller.Read(12)==PinValue.High);

            //output schrijven naar de waarden die zijn aangeduid op de pagina bij het opstarten
            controller.Write(13, Output1?PinValue.High:PinValue.Low);
            controller.Write(19, Output2?PinValue.High:PinValue.Low);
            controller.Write(26, Output3?PinValue.High:PinValue.Low);
           
        }
        //titel
        public string Message = "Raspberry PI GPIO";
        
        public void OnGet()
        {
           
        }
        
        //output binding
        [BindProperty]
        public bool Output1 { get; set; }
        [BindProperty]
        public bool Output2 { get; set; }
        [BindProperty]
        public bool Output3 { get; set; }

        //inputs
        public bool Input1 { get; set; }
        public bool Input2 { get; set; }
        public bool Input3 { get; set; }

        public void OnPost()
        {           
            //als form word gepost
            controller.Write(13, Output1?PinValue.High:PinValue.Low);
            controller.Write(19, Output2?PinValue.High:PinValue.Low);
            controller.Write(26, Output3?PinValue.High:PinValue.Low);
            this.Input1 = (controller.Read(24)==PinValue.High);
            this.Input2 = (controller.Read(25)==PinValue.High);
            this.Input3 = (controller.Read(12)==PinValue.High); 

            //debug
            Console.WriteLine($"Output 1 is {Output1}");
            Console.WriteLine($"Output 2 is {Output2}");
            Console.WriteLine($"Output 3 is {Output3}");
            Console.WriteLine();
            Console.WriteLine($"Input 1 is {Input1}");
            Console.WriteLine($"Input 2 is {Input2}");
            Console.WriteLine($"Input 3 is {Input3}");
            Console.WriteLine();
        }
    }
}
