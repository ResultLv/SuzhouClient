namespace client.Model
{
    class DeviceStatus
    {
        string _AP;
        string _Svr;
        string _Batt;
        string _Adh;
        string _Disarm;
        string _DtTm;
        string _CardCap;

        public DeviceStatus()
        {

        }

        public DeviceStatus(string AP, string Svr, string Batt, string Adh, string Disarm, string DtTm, string CardCap)
        {
            this.AP = AP;
            this.Svr = Svr;
            this.Batt = Batt;
            this.Adh = Adh;
            this.Disarm = Disarm;
            this.DtTm = DtTm;
            this.CardCap = CardCap;
        }

        public string AP { get => _AP; set => _AP = value; }
        public string Svr { get => _Svr; set => _Svr = value; }
        public string Batt { get => _Batt; set => _Batt = value; }
        public string Adh { get => _Adh; set => _Adh = value; }
        public string Disarm { get => _Disarm; set => _Disarm = value; }
        public string DtTm { get => _DtTm; set => _DtTm = value; }
        public string CardCap { get => _CardCap; set => _CardCap = value; }
    }
}
