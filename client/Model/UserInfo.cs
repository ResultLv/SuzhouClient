using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Model
{
    class UserInfo : DeviceInfo
    {
        string _UserName;
        string _PassWord;

        public UserInfo()
        {

        }

        public UserInfo(string UserName, string PassWord)
        {
            _UserName = UserName;
            _PassWord = PassWord;
        }

        public string UserName { get => _UserName; set => _UserName = value; }
        public string PassWord { get => _PassWord; set => _PassWord = value; }
    }
}
