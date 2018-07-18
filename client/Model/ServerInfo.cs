namespace client.Model
{
    class ServerInfo
    {
        string _IP;
        string _Port;

        public ServerInfo()
        {

        }
        public ServerInfo(string IP, string Port)
        {
            this.IP = IP;
            this.Port = Port;
        }

        public string IP { get => _IP; set => _IP = value; }
        public string Port { get => _Port; set => _Port = value; }
    }
}
