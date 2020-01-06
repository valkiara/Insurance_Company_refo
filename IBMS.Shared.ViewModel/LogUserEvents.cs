using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
   public class LogUserEvents
    {
        private string _TableID;

        public string TableID
        {
            get { return _TableID; }
            set { _TableID = value; }
        }
        private string _DocumentID;

        public string DocumentID
        {
            get { return _DocumentID; }
            set { _DocumentID = value; }
        }

        private string _Messages;

        public string Messages
        {
            get { return _Messages; }
            set { _Messages = value; }
        }

        private DateTime _CreateDate;

        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
        private DateTime _UpdateDate;

        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
        private int _CreateUser;

        public int CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private int _UpdateUser;

        public int UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }
    }
}
