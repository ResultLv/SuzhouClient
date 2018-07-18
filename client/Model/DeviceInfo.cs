namespace client.Model
{
    class DeviceInfo
    {
        string _ID;
        string _sRev;
        string _hRev;
        string _cmp;

        public DeviceInfo()
        {

        }

        public DeviceInfo(string ID, string sRev, string hRev, string cmp)
        {
            _ID = ID;
            _sRev = sRev;
            _hRev = hRev;
            _cmp = cmp;
        }

        public string ID { get => _ID; set => _ID = value; }
        public string sRev { get => _sRev; set => _sRev = value; }
        public string hRev { get => _hRev; set => _hRev = value; }
        public string cmp { get => _cmp; set => _cmp = value; }
    }
}
