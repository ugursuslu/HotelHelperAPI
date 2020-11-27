using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstrack;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var hotels = _hotelService.GetAllHotel();
            return Ok(hotels); //200 + data
        }
        /// <summary>
        /// Get Hotel Bey Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]          //api/hotels/GetHotelById/2
        public IActionResult GetHotelById(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel !=null)
            {
                return Ok(hotel);
            }
            return NotFound();  //404
        }

        /// <summary>
        /// Get Hotel By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{name}")]    //api/hotels/GetHotelByName/salamis
        public IActionResult GetHotelByName(string name)
        {
            var hotel = _hotelService.GetHotelByName(name);
            if (hotel !=null)
            {
                return Ok(hotel);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]/{id}/{name}")]    //api/hotels/GetHotelByName/salamis
        public IActionResult GetHotelByIdAndName(int id, string name)
        {
            return Ok();
        }

        /// <summary>
        /// Create an Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {  
           var createHotel = _hotelService.CreateHotel(hotel);
           return CreatedAtAction("GET", new {id=createHotel.Id, createHotel }); //201 + hotel data           
        }
        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateHotel([FromBody] Hotel hotel)
        {
            if (_hotelService.GetHotelById(hotel.Id) !=null)
            {
                return Ok(_hotelService.UpdateHotel(hotel)); //200+data
            }
            return NotFound();
        }
        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult DeleteHotel(int id)
        {
            if (_hotelService.GetHotelById(id) != null)
            {
                _hotelService.DeleteHotel(id);
                return Ok(); //200+data
            }
            return NotFound();
            
        }


    }

}
