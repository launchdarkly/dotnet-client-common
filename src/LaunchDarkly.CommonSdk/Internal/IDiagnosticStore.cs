using System;

namespace LaunchDarkly.Common
{
    internal interface IDiagnosticStore {
        DiagnosticId DiagnosticId { get; }
        bool SendInitEvent { get; }
        DiagnosticEvent LastStats { get; }
        DateTime DataSince { get; }
        DiagnosticEvent.Statistics CreateEventAndReset(long droppedEvents, long deduplicatedUsers, long eventsInQueue);
    }
}
