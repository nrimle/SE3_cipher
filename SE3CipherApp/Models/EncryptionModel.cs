using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE3CipherApp.Models
{
    public class EncryptionModel
    {
        public string EncryptText { get; set; }
        public string DecryptText { get; set; }
        public string Key { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
