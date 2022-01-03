using ConsoleApp1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TVMazeWebAPIApp.Interfaces;

namespace TVMazeWebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVMazeShowController : ControllerBase
    {
        //private readonly IConfiguration _configuration;
        private readonly IShowRepository _showRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="ShowController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public TVMazeShowController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        /// <summary>
        /// The GetAllShows.
        /// </summary>
        /// <returns>The <see cref="JsonResult"/>.</returns>
        [Route("getShowsWithCast")]
        [HttpGet]
        public async Task<JsonResult> GetAllShows(int pageStart, int pageEnd)
        {
            //Uncomment Next Line to trigger global error handler.
            //throw new UnauthorizedAccessException();
            var dt =  await _showRepository.GetShowWithCast(pageStart, pageEnd);
            var setJson = ConvertToList<Root>(dt);
           
            return new JsonResult(setJson);
      
        }

        /// <summary>
        /// The ConvertToList.
        /// </summary>
        /// <typeparam name="Root">.</typeparam>
        /// <param name="dt">The dt<see cref="DataTable"/>.</param>
        /// <returns>The <see cref="List{Root}"/>.</returns>
        public static List<Root> ConvertToList<Root>(DataTable dt)

            //global deserializer from datatable to object list
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(Root).GetProperties();
            return dt.AsEnumerable().Select(x => (Root)JsonConvert.DeserializeObject(
                x.ItemArray[0].ToString(),
                typeof(Root))).Select(xx =>
                {
                    return xx;
                }).ToList();
        }

        /// <summary>
        /// The GetShowById.
        /// </summary>
        /// <param name="showId">The showId<see cref="int"/>.</param>
        /// <returns>The <see cref="JsonResult"/>.</returns>
        [Route("getShowByIdWithCast")]
        [HttpGet]
        public async Task<JsonResult> GetShowById(int showId)
        {
                var dt = await _showRepository.GetShowWithCast(showId);
                var setJson = ConvertToList<Root>(dt);
                return new JsonResult(setJson);
        }
    }
}
