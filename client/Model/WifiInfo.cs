namespace client.Model
{
    class WifiInfo
    {
        string _WifiName;
        string _WifiEnc;
        string _ChannelIntensity;  //信道强度
        string _WifiMac;
        int _ChannelNum;


        public WifiInfo(string WifiName, string WifiEnc, string ChannelIntensity, string WifiMac, int ChannelNum)
        {
            this.WifiName = WifiName;
            this.WifiEnc = WifiEnc;
            this.ChannelIntensity = ChannelIntensity;
            this.WifiMac = WifiMac;
            this.ChannelNum = ChannelNum;
        }

        public string WifiName { get => _WifiName; set => _WifiName = value; }
        public string WifiEnc { get => _WifiEnc; set => _WifiEnc = value; }
        public string ChannelIntensity { get => _ChannelIntensity; set => _ChannelIntensity = value; }
        public string WifiMac { get => _WifiMac; set => _WifiMac = value; }
        public int ChannelNum { get => _ChannelNum; set => _ChannelNum = value; }
    }
}
