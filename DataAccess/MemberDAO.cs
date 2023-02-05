using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        public static List<Member> GetMembers()
        {
            var listMembers = new List<Member>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listMembers = context.Members.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listMembers;
        }

        public static Member FindMemberById(int memId)
        {
            Member m = new Member();
            try
            {
                using (var context = new MyDbContext())
                {
                    m = context.Members.Include(m => m.Orders).SingleOrDefault(x => x.MemberId == memId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return m;
        }

        public static void SaveMember(Member m)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Members.Add(m);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateMember(Member m)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Member>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteMember(Member m)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var m1 = context.Members.SingleOrDefault(c => c.MemberId == m.MemberId);
                    context.Members.Remove(m1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Member Login(string email, string password)
        {
            Member m = new Member();
            try
            {
                using (var context = new MyDbContext())
                {
                    m = context.Members.SingleOrDefault(x => x.Email == email && x.Password == password);                   
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return m;
        }
        
    }
}

