using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(Member m) => MemberDAO.DeleteMember(m);

        public Member GetMemberById(int id) => MemberDAO.FindMemberById(id);


        public List<Member> GetMembers() => MemberDAO.GetMembers();


        public void SaveMember(Member p) => MemberDAO.SaveMember(p);


        public void UpdateMember(Member p) => MemberDAO.UpdateMember(p);

        public Member Login(string email, string password) => MemberDAO.Login(email, password);
    }
}
