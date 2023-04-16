using Microsoft.AspNetCore.Mvc;


namespace T1TeenFit.Web.Areas.Admin.Controllers
{
    public enum AlertType { success, warning, information }

    public class BaseController : Controller
    {
        // Alert notification message to be stored as TempData, information enum set as default alert type if not specified  
        public void Alert(string notification, AlertType type = AlertType.information)
        {
            TempData["Alert.Message"] = notification;
            TempData["Alert.Type"] = type.ToString();
        }
    }
}