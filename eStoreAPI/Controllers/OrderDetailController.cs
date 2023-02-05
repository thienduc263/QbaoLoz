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
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult GetOrderDetail()
        {
            var p = _orderDetailRepository.GetOrderDetails();
            var pDTO = _mapper.Map<IEnumerable<OrderDetailDTO>>(p);
            return Ok(pDTO);
        }

        [HttpGet("{ordId}/{proId}")]
        public ActionResult FindOrderById(int ordId, int proId)
        {
            var p = _orderDetailRepository.GetOrderDetailById(ordId, proId);
            var pDTO = _mapper.Map<OrderDetailDTO>(p);
            return Ok(pDTO);
        }

        [HttpPost]
        public ActionResult SaveOrder(OrderDetailDTO o)
        {
            var orderDetail = _mapper.Map<OrderDetail>(o);
            _orderDetailRepository.SaveOrderDetail(orderDetail);
            return Ok();
        }

        [HttpPut("{ordId}/{proId}")]
        public ActionResult UpdateOrderDetail(OrderDetailDTO o, int ordId, int proId)
        {
            var orderDetail1 = _mapper.Map<OrderDetail>(o);
            var orderDetail = _orderDetailRepository.GetOrderDetailById(ordId, proId);
            if (orderDetail == null)
                return NotFound();
            _orderDetailRepository.UpdateOrderDetail(orderDetail1, ordId, proId);
            return NoContent();
        }

        [HttpDelete("{ordId}/{proId}")]
        public ActionResult DeleteOrder(int ordId, int proId)
        {
            var orderDetail = _orderDetailRepository.GetOrderDetailById(ordId, proId);
            if (orderDetail == null)
                return NotFound();
            _orderDetailRepository.DeleteOrderDetail(orderDetail);
            return NoContent();
        }

        [HttpGet("GetOrderDetailListByListOrder")]
        public ActionResult<List<OrderDetail>> GetOrderDetailListByListOrder(DateTime startDate, DateTime endDate)
        {
            var listOrderDetail = _orderDetailRepository.GetOrderDetailListByListOrder(_orderRepository.Filter(startDate, endDate));
            return Ok(listOrderDetail);
        }

        [HttpGet("GetOrderDetailStatistic")]
        public double GetOrderDetailStatistic(DateTime startDate, DateTime endDate)
        {
            var orderList = _orderRepository.Filter(startDate, endDate);
            var statisticResult = _orderDetailRepository.GetStatistic(orderList);
            return statisticResult;
        }
    }
}
