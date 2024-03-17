using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;

namespace ButtonGrid.Controllers
{
    public class ButtonController : Controller
    {
        static List<ButtonModel> buttons = new List<ButtonModel>();
        readonly Random random = new Random();
        const int GRID_SIZE = 25;
        public IActionResult Index()
        {
            if(buttons.Count < GRID_SIZE)
            {
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    buttons.Add(new ButtonModel { Id = i, ButtonState = random.Next(4) });
                }
            }
           
            return View("Index", buttons);
        }

        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int Id = int.Parse(buttonNumber);
            buttons.ElementAt(Id).ButtonState = (buttons.ElementAt(Id).ButtonState + 1) % 4;

            return View("Index", buttons);
        }
    }
}
