using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CustomActionFilter.Controllers.Utils
{
    public class TrackExecutionTimeController : ActionFilterAttribute, IExceptionFilter
    {

        /// <summary>
        /// Método del decorador TrackExecutionTimeController que se ejecuta en primer instancia 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                " -> " + filterContext.ActionDescriptor.ActionName + " -> " + this.GetMethodName(new StackTrace()) + " \t " +
                DateTime.Now.ToString() + " \n";            

            LogExecutionTime(message);
        }

        /// <summary>
        /// Método del decorador TrackExecutionTimeController que se al terminar el metodo OnActionExecuting 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                " -> " + filterContext.ActionDescriptor.ActionName + " -> " + this.GetMethodName(new StackTrace()) + " \t " +
                DateTime.Now.ToString() + " \n";

            LogExecutionTime(message);
        }

        /// <summary>
        /// Método del decorador TrackExecutionTimeController que se al terminar correctamente el método OnActionExecuted
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString() +
                " -> " + filterContext.RouteData.Values["action"].ToString() + " -> " + this.GetMethodName(new StackTrace()) + " \t " +
                DateTime.Now.ToString() + " \n";

            LogExecutionTime(message);
        }

        /// <summary>
        /// Método del decorador TrackExecutionTimeController que se al terminar el método OnResultExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString() +
                " -> " + filterContext.RouteData.Values["action"].ToString() + " -> " + this.GetMethodName(new StackTrace()) + " \t " +
                DateTime.Now.ToString() + " \n";

            LogExecutionTime(message);
            LogExecutionTime("-------------------------- \n");
        }

        /// <summary>
        /// Implementación interface IExceptionFilter, se lanza automatizamente al ocurrí una exception
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString() +
                " -> " + filterContext.RouteData.Values["action"].ToString() + " -> " + this.GetMethodName(new StackTrace()) + " \t " +
                DateTime.Now.ToString() + " \n";

            LogExecutionTime(message);
            LogExecutionTime("-------------------------- \n");
        }

        /// <summary>
        /// Método para registrar en el Logs 
        /// </summary>
        /// <param name="data"> Mensaje a registrar </param>
        private void LogExecutionTime(string data)
        {
            string rutaLog = System.Configuration.ConfigurationManager.AppSettings["RutaLog"];
            File.AppendAllText(HttpContext.Current.Server.MapPath(rutaLog), data);
        }

        /// <summary>
        /// Método que devuelve el nombre del método que lo invoca
        /// </summary>
        /// <returns> Nombre del método que lo invoca </returns>
        private string GetMethodName(StackTrace st) 
        {
            StackFrame sf = st.GetFrame(0);
            MethodBase currentMethodName = sf.GetMethod();

            return currentMethodName.Name;
        }
    }
}