using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace Volo.CmsKit.ViewComponents
{
    
    [Widget]
    [ViewComponent(Name = "Format")]
    public class FormatViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/ViewComponents/Today/Format.cshtml",
             new FormatViewModel());
        }
    }

    public class FormatViewModel
    {
        [DisplayName("Format your date in the component")]
        public string Format { get; set; } = "yyyy-dd-mm HH:mm:ss";
    }
}