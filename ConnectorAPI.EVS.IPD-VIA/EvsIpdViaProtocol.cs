namespace Skyline.DataMiner.ConnectorAPI.EVS.IPD_VIA
{
    /// <summary>
    /// Defines constants related to the EVS IPD-VIA protocol.
    /// </summary>
    public static class EvsIpdViaProtocol
    {
        /// <summary>
        /// The name of the EVS IPD-VIA protocol.
        /// </summary>
        public static readonly string Name = "EVS IPD-VIA";

        /// <summary>
        /// Constants related to the Recorders table.
        /// </summary>
        public static class RecordersTable
        {
            /// <summary>
            /// PID of the Recorders table.
            /// </summary>
            public static readonly int TablePid = 1000;

            /// <summary>
            /// Index for Recorder Names.
            /// </summary>
            public static class Idx
            {
                /// <summary>
                /// Index for Recorder Names.
                /// </summary>
                public static readonly int RecordersName = 1;
            }
        }

        /// <summary>
        /// Constants related to the Targets table.
        /// </summary>
        public static class TargetsTable
        {
            /// <summary>
            /// PID of the Targets table.
            /// </summary>
            public static readonly int TablePid = 1100;

            /// <summary>
            /// Index for Target Names.
            /// </summary>
            public static class Idx
            {
                /// <summary>
                /// Index for Target Names.
                /// </summary>
                public static readonly int TargetsName = 1;
            }
        }

        /// <summary>
        /// Constants related to the Recording Sessions table.
        /// </summary>
        public static class RecordingSessionsTable
        {
            /// <summary>
            /// PID of the Recording Sessions table.
            /// </summary>
            public static readonly int TablePid = 1400;

            /// <summary>
            /// PID of the Recording Sessions Name.
            /// </summary>
            public static readonly int RecordingSessionsName = 1402;

            /// <summary>
            /// PID of the Recording Sessions Status.
            /// </summary>
            public static readonly int RecordingSessionsStatus = 1403;

            /// <summary>
            /// PID of the Recording Sessions Start.
            /// </summary>
            public static readonly int RecordingSessionsStart = 1404;

            /// <summary>
            /// PID of the Recording Sessions End.
            /// </summary>
            public static readonly int RecordingSessionsEnd = 1406;

            /// <summary>
            /// PID of the Recording Sessions Recorder.
            /// </summary>
            public static readonly int RecordingSessionsRecorder = 1408;

            /// <summary>
            /// Indexes for the Recording Sessions table.
            /// </summary>
            public static class Idx
            {
                /// <summary>
                /// Index for Recording Sessions Instance.
                /// </summary>
                public static readonly int RecordingSessionsInstanceIdx = 0;

                /// <summary>
                /// Index for Recording Sessions Name.
                /// </summary>
                public static readonly int RecordingSessionsNameIdx = 1;

                /// <summary>
                /// Index for Recording Sessions Status.
                /// </summary>
                public static readonly int RecordingSessionsStatusIdx = 2;

                /// <summary>
                /// Index for Recording Sessions Start.
                /// </summary>
                public static readonly int RecordingSessionsStartIdx = 3;

                /// <summary>
                /// Index for Recording Sessions End.
                /// </summary>
                public static readonly int RecordingSessionsEndIdx = 4;

                /// <summary>
                /// Index for Recording Sessions Recorder.
                /// </summary>
                public static readonly int RecordingSessionsRecorderIdx = 5;
            }
        }

        /// <summary>
        /// Constants related to the Profile Fields table.
        /// </summary>
        public static class ProfileFieldsTable
        {
            /// <summary>
            /// PID of the Profile Fields table.
            /// </summary>
            public static readonly int TablePid = 1900;

            /// <summary>
            /// Profile Fields PIDs.
            /// </summary>
            public static class Pid
            {
                /// <summary>
                /// PID for Profile Fields Instance.
                /// </summary>
                public static readonly int ProfileFieldsInstance = 1901;

                /// <summary>
                /// PID for Profile Fields Key.
                /// </summary>
                public static readonly int ProfileFieldsKey = 1902;

                /// <summary>
                /// PID for Profile Fields Label.
                /// </summary>
                public static readonly int ProfileFieldsLabel = 1903;

                /// <summary>
                /// PID for Profile Fields Type.
                /// </summary>
                public static readonly int ProfileFieldsType = 1904;

                /// <summary>
                /// PID for Profile Fields Required.
                /// </summary>
                public static readonly int ProfileFieldsRequired = 1905;

                /// <summary>
                /// PID for Profile Fields Value Constraints.
                /// </summary>
                public static readonly int ProfileFieldsValueConstraints = 1906;

                /// <summary>
                /// PID for Profile Fields Predefined Value.
                /// </summary>
                public static readonly int ProfileFieldsPredefinedValue = 1907;

                /// <summary>
                /// PID for Profile Fields Profile FQN.
                /// </summary>
                public static readonly int ProfileFieldsProfileFqn = 1908;

                /// <summary>
                /// PID for Profile Fields Profile Name.
                /// </summary>
                public static readonly int ProfileFieldsProfileName = 1909;

                /// <summary>
                /// PID for Profile Fields Display Key.
                /// </summary>
                public static readonly int ProfileFieldsDisplayKey = 1910;

                /// <summary>
                /// PID for Profile Fields FQN Constraints.
                /// </summary>
                public static readonly int ProfileFieldsFqnContraints = 1911;
            }

            /// <summary>
            /// Indexes for the Profile Fields table.
            /// </summary>
            public static class Idx
            {
                /// <summary>
                /// Index for Profile Fields Instance.
                /// </summary>
                public static readonly int ProfileFieldsInstance = 0;

                /// <summary>
                /// Index for Profile Fields Key.
                /// </summary>
                public static readonly int ProfileFieldsKey = 1;

                /// <summary>
                /// Index for Profile Fields Label.
                /// </summary>
                public static readonly int ProfileFieldsLabel = 2;

                /// <summary>
                /// Index for Profile Fields Type.
                /// </summary>
                public static readonly int ProfileFieldsType = 3;

                /// <summary>
                /// Index for Profile Fields Required.
                /// </summary>
                public static readonly int ProfileFieldsRequired = 4;

                /// <summary>
                /// Index for Profile Fields Value Constraints.
                /// </summary>
                public static readonly int ProfileFieldsValueConstraints = 5;

                /// <summary>
                /// Index for Profile Fields Predefined Value.
                /// </summary>
                public static readonly int ProfileFieldsPredefinedValue = 6;

                /// <summary>
                /// Index for Profile Fields Profile FQN.
                /// </summary>
                public static readonly int ProfileFieldsProfileFqn = 7;

                /// <summary>
                /// Index for Profile Fields Profile Name.
                /// </summary>
                public static readonly int ProfileFieldsProfileName = 8;

                /// <summary>
                /// Index for Profile Fields Display Key.
                /// </summary>
                public static readonly int ProfileFieldsDisplayKey = 9;

                /// <summary>
                /// Index for Profile Fields FQN Constraints.
                /// </summary>
                public static readonly int ProfileFieldsFqnContraints = 10;
            }
        }

        /// <summary>
        /// Constants related to the Recording Sessions Targets table.
        /// </summary>
        public static class RecordingSessionsTargetsTable
        {
            /// <summary>
            /// PID of the Recording Sessions Targets table.
            /// </summary>
            public static readonly int TablePid = 1500;

            /// <summary>
            /// Recording Sessions Targets PIDs.
            /// </summary>
            public static class Pid
            {
                /// <summary>
                /// PID for Recording Sessions Targets Instance.
                /// </summary>
                public static readonly int RecordingSessionsTargetsInstance = 1501;

                /// <summary>
                /// PID for Recording Sessions Targets Recording Session Display Key.
                /// </summary>
                public static readonly int RecordingSessionsTargetsRecordingSessionDisplayKey = 1502;

                /// <summary>
                /// PID for Recording Sessions Targets Recording Session Instance.
                /// </summary>
                public static readonly int RecordingSessionsTargetsRecordingSessionInstance = 1503;

                /// <summary>
                /// PID for Recording Sessions Targets Recording Session.
                /// </summary>
                public static readonly int RecordingSessionsTargetsRecordingSession = 1504;

                /// <summary>
                /// PID for Recording Sessions Targets Target Instance.
                /// </summary>
                public static readonly int RecordingSessionsTargetsTargetInstance = 1505;

                /// <summary>
                /// PID for Recording Sessions Targets Target.
                /// </summary>
                public static readonly int RecordingSessionsTargetsTarget = 1506;
            }

            /// <summary>
            /// Indexes for the Recording Sessions Targets table.
            /// </summary>
            public static class Idx
            {
                /// <summary>
                /// Index for Recording Sessions Targets Instance.
                /// </summary>
                public static readonly int RecordingSessionsTargetsInstance = 0;

                /// <summary>
                /// Index for Recording Sessions Targets Recording Session Display Key.
                /// </summary>
                public static readonly int RecordingSessionsTargetsRecordingSessionDisplayKey = 1;

                /// <summary>
                /// Index for Recording Sessions Targets Recording Session Instance.
                /// </summary>
                public static readonly int RecordingSessionsTargetsRecordingSessionInstance = 2;

                /// <summary>
                /// Index for Recording Sessions Targets Recording Session.
                /// </summary>
                public static readonly int RecordingSessionsTargetsRecordingSession = 3;

                /// <summary>
                /// Index for Recording Sessions Targets Target Instance.
                /// </summary>
                public static readonly int RecordingSessionsTargetsTargetInstance = 4;

                /// <summary>
                /// Index for Recording Sessions Targets Target.
                /// </summary>
                public static readonly int RecordingSessionsTargetsTarget = 5;
            }
        }

        /// <summary>
        /// Constants related to the Recording Sessions Metadata Values table.
        /// </summary>
        public static class RecordingSessionsMetadataValuesTable
        {
            /// <summary>
            /// PID of the Recording Sessions Metadata Values table.
            /// </summary>
            public static readonly int TablePid = 1700;

            /// <summary>
            /// Recording Sessions Metadata Values PIDs.
            /// </summary>
            public static class Pid
            {
                /// <summary>
                /// PID for Recording Sessions Metadata Values Instance.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesInstance = 1701;

                /// <summary>
                /// PID for Recording Sessions Metadata Values Key.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesKey = 1702;

                /// <summary>
                /// PID for Recording Sessions Metadata Values Value.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesValue = 1703;

                /// <summary>
                /// PID for Recording Sessions Metadata Values Recording Session.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesRecordingSession = 1704;

                /// <summary>
                /// PID for Recording Sessions Metadata Values Recording Session ID.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesRecordingSessionId = 1705;

                /// <summary>
                /// PID for Recording Sessions Metadata Values Display Key.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesDisplayKey = 1706;

                /// <summary>
                /// PID for Recording Sessions Metadata Values Profile.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesProfile = 1707;

                /// <summary>
                /// PID for Recording Sessions Metadata Values Profile ID.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesProfileId = 1708;
            }

            /// <summary>
            /// Indexes for the Recording Sessions Metadata Values table.
            /// </summary>
            public static class Idx
            {
                /// <summary>
                /// Index for Recording Sessions Metadata Values Instance.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesInstance = 0;

                /// <summary>
                /// Index for Recording Sessions Metadata Values Key.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesKey = 1;

                /// <summary>
                /// Index for Recording Sessions Metadata Values Value.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesValue = 2;

                /// <summary>
                /// Index for Recording Sessions Metadata Values Recording Session.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesRecordingSession = 3;

                /// <summary>
                /// Index for Recording Sessions Metadata Values Recording Session ID.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesRecordingSessionId = 4;

                /// <summary>
                /// Index for Recording Sessions Metadata Values Display Key.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesDisplayKey = 5;

                /// <summary>
                /// Index for Recording Sessions Metadata Values Profile.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesProfile = 6;

                /// <summary>
                /// Index for Recording Sessions Metadata Values Profile ID.
                /// </summary>
                public static readonly int RecordingSessionsMetadataValuesProfileId = 7;
            }
        }
    }
}
