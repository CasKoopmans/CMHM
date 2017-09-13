using System;

namespace HMonServer
{
	public interface Command
	{
		void Execute(IClient client, DataPacket data);
	}
}

