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
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberController(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetMembers()
        {
            var m = _memberRepository.GetMembers();
            var mDTO = _mapper.Map<IEnumerable<MemberDTO>>(m);
            return Ok(mDTO);
        }
        
        [HttpGet("{id}")]
        public ActionResult FindMemberById(int id)
        {
            var m = _memberRepository.GetMemberById(id);
            var mDTO = _mapper.Map<MemberDTO>(m);
            return Ok(m);
        }

        [HttpPost]
        public ActionResult<MemberDTO> SaveMember(MemberDTO m)
        {
            var member = _mapper.Map<Member>(m);
            _memberRepository.SaveMember(member);
            return Ok();
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateMember(MemberDTO m)
        {
            var member = _mapper.Map<Member>(m);
            _memberRepository.UpdateMember(member);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMember(int id)
        {
            var member = _memberRepository.GetMemberById(id);
            if (member == null)
                return NotFound();
            _memberRepository.DeleteMember(member);
            return NoContent();
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginDTO l)
        {
            /*var login = _mapper.Map<MemberDTO>(l);*/
            var member = _mapper.Map<MemberDTO>(_memberRepository.Login(l.Email, l.Password));
            if (member == null) return BadRequest("Incorrect email or password!");
            return Ok(member);
        }
    }
}
