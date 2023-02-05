using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMemberRepository
    {
        void SaveMember(Member m);
        Member GetMemberById(int id);
        void DeleteMember(Member m);
        void UpdateMember(Member m);
        List<Member> GetMembers();
        Member Login(string email, string password);
    }
}
