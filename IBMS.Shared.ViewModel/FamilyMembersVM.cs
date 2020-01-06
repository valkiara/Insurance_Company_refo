using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
   public class FamilyMembersVM
    {
        public int FamilyMemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberDOB { get; set; }

        public string JoinDate { get; set; }

       
        public string NIC { get; set; }
        public string ContactNo { get; set; }
        public Nullable<int> ClientID { get; set; }
        //other name
        public string ExtraText1 { get; set; }
        public int MemberCountryID { get; set; }

        public int MemberResCountryID { get; set; }
        public List<GroupFamilyMembersVM> GroupMemberDetails { get; set; }
        public string MembershipID { get; set; }

        //ExtraInt1
        public int RelationShipID { get; set; }
        public string Relationship { get; set; }


        //ExtraInt2
        public int GenderID { get; set; }
        public string Gender { get; set; }

        public int TitleID { get; set; }
        public string TitleName { get; set; }

        public string MemberOtherName { get; set; }

        public string InceptionDate { get; set; }

        public int PremiumID { get; set; }
        public string PremiumName { get; set; }

        public int CurrencyID { get; set; }

        public string CurrencyName { get; set; }

        public float Exclusion { get; set; }
        public string OptionalCover { get; set; }
        public string Exclu
        { get; set; }
        public int IsActive { get; set; }

        public int HomeCountryID { get; set; }
        public int ResidentCountryID { get; set; }

        public int SchemeID { get; set; }

        public string Year { get; set; }
        public string SeqNo { get; set; }
        public string SNo { get; set; }
        public string SeqSubNo { get; set; }


        //other name

    }

    public class GroupFamilyMembersVM
    {
        public int MemberID { get; set; }
        public int FamilyMemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberDOB { get; set; }
        public string NIC { get; set; }
        public string ContactNo { get; set; }

        public int? countryID { get; set; }
        public Nullable<int> ClientID { get; set; }

        public int RelationShipID { get; set; }
        public string Relationship { get; set; }


        //ExtraInt1
        public int GenderID { get; set; }
        public string Gender { get; set; }
        //ExtraInt2
        public int TitleID { get; set; }
        public string TitleName { get; set; }

        public string MemberOtherName { get; set; }

        public string InceptionDate { get; set; }

        public int PremiumID { get; set; }
        public string PremiumName { get; set; }

        public int CurrencyID { get; set; }

        public string CurrencyName { get; set;}

        public string ExtraText1 { get; set; }

        public string JoinDate { get; set;  }

        public string MembershipID { get; set; }

        public int? MemberResCountryID { get; set; }

        public string  Exclu         { get; set; }
        public int MemberCountryID { get; set; }
        public int ResidentCountryID { get; set; }

        public int IsActive { get; set; }
        public string OptionalCover { get; set; }
        public string Year { get; set; }
        public string SeqNo { get; set; }
        public string SNo { get; set; }
        public string SeqSubNo { get; set; }
    }
}

