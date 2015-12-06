using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Themes;
using System.Web.Mvc;
using Orchard.UI.Notify;

namespace DJO.PoolLeague.Controllers
{
    [ValidateInput(false), Themed, Authorize]
    public class PoolGameController : Controller, IUpdateModel
    {
        private readonly IOrchardServices _services;

        public PoolGameController(IOrchardServices services)
        {
            _services = services;
        }

        public Localizer T { get; set; }

        public ActionResult NewGame()
        {
            var poolGame = _services.ContentManager.New("PoolGame");
            dynamic shape = _services.ContentManager.BuildEditor(poolGame);
            return View((object)shape);
        }

        [HttpPost]
        public ActionResult NewGame(string returnUrl)
        {
            var poolGame = _services.ContentManager.New("PoolGame");
            dynamic shape = _services.ContentManager.UpdateEditor(poolGame, this);
            if (!ModelState.IsValid)
            {
                _services.TransactionManager.Cancel();
                return View("NewGame", (object)shape);
            }

            _services.ContentManager.Create(poolGame);
            _services.Notifier.Information(T("Your game has been saved."));

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Edit");
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}