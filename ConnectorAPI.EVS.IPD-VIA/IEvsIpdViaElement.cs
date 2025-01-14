using System;
using Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA.Model;

namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA
{
	public interface IEvsIpdViaElement
	{
		string Name { get; }
		TimeSpan Timeout { get; }

		RecordingSession AddOrUpdateRecordingSession(RecordingSession recordingSession);
		void DeleteRecordingSession(string recordingSessionId);
	}
}