using System;
using System.Collections.Generic;
using System.Linq;
using DAL_LibraryManagement;
using DTO_LibraryManagement;
namespace BLL_LibraryManagement
{
    public class MemberService
    {
        public enum Mode {AddNew=0,Update=1}
        public int MemberID { get; private set; }
        public int PersonID { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public PersonService PersonInfo { get;}

        public Mode _Mode { get; private set; }

        public MemberService(int memberID, int personID, DateTime joinDate,bool isActive)
        {
            MemberID = memberID;
            PersonID = personID;
            JoinDate = joinDate;
            IsActive = isActive;
            PersonInfo = PersonService.FindPersonByID(PersonID);

            _Mode = Mode.Update;
        }

        public MemberService()
        {
            MemberID = -1;
            PersonID = -1;
            JoinDate = DateTime.Now;
            IsActive = true;
            _Mode = Mode.AddNew;
        }
        

        

        public static List<MemberService> GetAllMembers()
        {
           
            var membersList = MemberRepository.GetAllMembers();
           
            if (membersList == null) return null;

            var members = membersList.Select( 
              m => new MemberService(m.MemberID, m.PersonID, m.JoinDate, m.IsActive)
             ).ToList();

            return members;
        }

        private MemberDTO _FillMemberDTO()
        {
            return new MemberDTO
            {
                MemberID = MemberID,
                PersonID = PersonID,
                JoinDate = JoinDate,
                IsActive = IsActive,
            };
        }
        private bool _AddNewMember()
        {
           this.MemberID =  MemberRepository.AddNewMember(this.PersonID,this.JoinDate,this.IsActive);
            return MemberID != -1;    
        }
        private bool _UpdateMember()
        {
            var dataMember = _FillMemberDTO();
            return MemberRepository.UpdateMember(dataMember);
        }
        public static MemberService FindMemberByID(int id)
        {
            var memberDTO = MemberRepository.FindMemberByID(id);

            if (memberDTO == null) return null;

            return new MemberService(memberDTO.MemberID,memberDTO.PersonID,memberDTO.JoinDate,memberDTO.IsActive);
        }

        public OperationResultBLL Save()
         {
            switch (_Mode)
            {
                case Mode.AddNew:

                    if (_AddNewMember())
                    {
                        _Mode = Mode.Update;
                        return OperationResultBLL.Ok("Member has been added successfully .");
                    }
                    break;

                case Mode.Update:

                    if(_UpdateMember())
                    {
                        return OperationResultBLL.Fail("Member added failed!");
                    }
                    break;
            }
            return OperationResultBLL.Fail("an unexpected error occurred !");
        }

        public static MemberService FindMemberByPersonID(int id)
        {
            MemberDTO member = MemberRepository.FindMemberByPersonID(id);
            if (member != null)
                return new MemberService(member.MemberID,member.PersonID,member.JoinDate,member.IsActive);
          
            return null;
        }

        public bool DesactiveMember(int id)
        {
            int rowsAffected = MemberRepository.SetMemberActiveStatus(id,false);
            return rowsAffected > 0;
        }
        public bool ActiveMember(int id)
        {
            int rowsAffected = MemberRepository.SetMemberActiveStatus(id, true);
            return rowsAffected > 0;
        }

        public static bool IsMemberExist(int id)
        {
            var member = FindMemberByID(id);
            return member != null;
        }
        public static bool IsPersonAsMember(int id)
        {
            var member = FindMemberByPersonID(id);
            return (member != null);
        }
    }
}
