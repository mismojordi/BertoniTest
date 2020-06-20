using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BertoniTest.Extensions
{
    public static class ControllerExtensions
    {
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewname,TModel model,bool partial=false)
        {
            if (string.IsNullOrEmpty(viewname))
            {
                viewname = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using(var writer = new StringWriter())
            {
                IViewEngine viewengin = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewEngineResult = viewengin.FindView(controller.ControllerContext, viewname, !partial);

                if(viewEngineResult.Success == false)
                {
                    return $"Vista no encontrada {viewname}";
                }

                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewEngineResult.View, controller.ViewData, controller.TempData, writer,
                    new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions());
                await viewEngineResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
