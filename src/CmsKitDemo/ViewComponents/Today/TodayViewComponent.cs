using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace Volo.CmsKit.ViewComponents
{
    [Widget]
    [ViewComponent(Name = "CmsToday")]
    public class TodayViewComponent : AbpViewComponent
    {
        public string Format { get; set; }
        public IViewComponentResult Invoke(string format)
        {
            return View("~/ViewComponents/Today/Today.cshtml", new TodayViewComponent() { Format = format });
        }
    }
   
}