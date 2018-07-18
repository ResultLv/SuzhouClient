using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Model
{
    class Data
    {
        int _Fil;
        string _DtTm;
        int _Totl;
        string _MdAv;
        string _LstAv;
        string _MdEf;
        string _LstEf;
        int _CurPer;
        int _MdTm;
        string _Dis;

        public Data(int Fil, string DtTm, int Totl, string MdAv, string LstAv, string MdEf, string LstEf, int CurPer, int MdTm, string Dis)
        {
            this.Fil = Fil; 
            this.DtTm = DtTm;
            this.Totl = Totl;
            this.MdAv = MdAv;
            this.LstAv = LstAv;
            this.MdEf = MdEf;
            this.LstEf = LstEf;
            this.CurPer = CurPer;
            this.MdTm = MdTm;
            this.Dis = Dis;
        }

        public int Fil { get => _Fil; set => _Fil = value; }
        public string DtTm { get => _DtTm; set => _DtTm = value; }
        public int Totl { get => _Totl; set => _Totl = value; }
        public string MdAv { get => _MdAv; set => _MdAv = value; }
        public string LstAv { get => _LstAv; set => _LstAv = value; }
        public string MdEf { get => _MdEf; set => _MdEf = value; }
        public string LstEf { get => _LstEf; set => _LstEf = value; }
        public int CurPer { get => _CurPer; set => _CurPer = value; }
        public int MdTm { get => _MdTm; set => _MdTm = value; }
        public string Dis { get => _Dis; set => _Dis = value; }
    }
}
