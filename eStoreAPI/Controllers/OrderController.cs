using AutoMapper;
using BusinessObject;
using DataAccess.Repositories;
using eStoreAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetOrder()
        {
            var p = _orderRepository.GetOrders();
            var pDTO = _mapper.Map<IEnumerable<OrderDTO>>(p);
            return Ok(pDTO);
        }

        [HttpGet("{id}")]
        public ActionResult FindOrderById(int id)
        {
            var p = _orderRepository.GetOrderById(id);
            var pDTO = _mapper.Map<OrderDTO>(p);
            return Ok(pDTO);
        }

        [HttpPost]
        public ActionResult SaveOrder(OrderDTO o)
        {
            var order = _mapper.Map<Order>(o);
            _orderRepository.SaveOrder(order);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(OrderDTO o)
        {
            var order = _mapper.Map<Order>(o);
            _orderRepository.UpdateOrder(order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
                return NotFound();
            _orderRepository.DeleteOrder(order);
            return NoContent();
        }

        [HttpGet("GetOrdersStatistic")]
        public ActionResult<List<Order>> GetOrdersStatistic(DateTime startDate, DateTime endDate)
        {
            if (!checkStatisticDate(startDate, endDate)) return BadRequest("Date Time input invalid!");

            var orders = _orderRepository.Filter(startDate, endDate);
            return Ok(orders);
        }

        private bool checkStatisticDate(DateTime startDate, DateTime endDate)
        {
            var result = DateTime.Compare(startDate, endDate);
            if (result == -1)
            {
                return true;
            }
            else if (result == 0)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
