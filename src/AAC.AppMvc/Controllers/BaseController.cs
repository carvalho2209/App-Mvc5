using System;
using System.Web.Mvc;
using AAC.Business.Core.Notifications;

namespace AAC.AppMvc.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificator _notificator;

        public BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation()
        {
            if (!_notificator.HasNotification())
                return true;

            var notifications = _notificator.GetNotifications();

            notifications.ForEach(c => ViewData.ModelState.AddModelError(String.Empty, c.Message));

            return false;
        }
    }
}