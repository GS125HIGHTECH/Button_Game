using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

        public IActionResult ShowOneButton(int buttonNumber)
        {
            buttons.ElementAt(buttonNumber).ButtonState = (buttons.ElementAt(buttonNumber).ButtonState + 1) % 4;
            string buttonString = RenderRazorViewToString(this, "ShowOneButton", buttons.ElementAt(buttonNumber));

            bool DidWinYet = true;
            for(int i = 0; i < buttons.Count; i++)
            {
                if(buttons.ElementAt(i).ButtonState != buttons.ElementAt(0).ButtonState)
                {
                    DidWinYet = false;
                }
            }

            string messageString = "";

            if(DidWinYet)
            {
                messageString = "<p>Congratulations. All of the buttons are the same color</p>";
            }
            else
            {
                messageString = "<p>Not all buttons are the same color. See if you can make them all match.</p>";
            }

            var package = new { part1 = buttonString, part2 = messageString };

            return Json(package);
        }

        public IActionResult RightClickOneButton(int buttonNumber)
        {
            buttons.ElementAt(buttonNumber).ButtonState = 1;
            string buttonString = RenderRazorViewToString(this, "ShowOneButton", buttons.ElementAt(buttonNumber));

            bool DidWinYet = true;
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons.ElementAt(i).ButtonState != buttons.ElementAt(0).ButtonState)
                {
                    DidWinYet = false;
                }
            }

            string messageString = "";

            if (DidWinYet)
            {
                messageString = "<p>Congratulations. All of the buttons are the same color</p>";
            }
            else
            {
                messageString = "<p>Not all buttons are the same color. See if you can make them all match.</p>";
            }

            var package = new { part1 = buttonString, part2 = messageString };

            return Json(package);
        }

        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine =
                    controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as
                        ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
