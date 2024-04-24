namespace Fantasy.Core.Network
{
	// Route协议定义(需要定义1000以上、因为1000以内的框架预留)	
	public enum RouteType : long
	{
		GateRoute = 1001, // Gate
		ChatRoute = 1002, // Chat
	}
}
