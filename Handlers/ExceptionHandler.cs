using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace TVMazeWebAPIApp.Handlers
{
    /// <summary>
    /// Defines the <see cref="ExceptionHandler" />.
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// Defines the next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class.
        /// </summary>
        /// <param name="next">The next<see cref="RequestDelegate"/>.</param>
        public ExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// The Invoke.
        /// </summary>
        /// <param name="context">The context<see cref="HttpContext"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task Invoke(HttpContext context)
        {

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/text";
                await context.Response.WriteAsync(ex.Message.ToString());
            }
        }
    }
}
