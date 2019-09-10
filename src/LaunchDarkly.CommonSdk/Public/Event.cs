﻿using System;
using Newtonsoft.Json.Linq;

namespace LaunchDarkly.Client
{
    /// <summary>
    /// An analytics event that may be sent to LaunchDarkly. Used internally.
    /// </summary>
    /// <remarks>
    /// This class and its subclasses are public so as to be usable by custom implementations of
    /// <see cref="IEventProcessor"/>. Application code should not construct or modify events; they
    /// are generated by the client.
    /// </remarks>
    public abstract class Event
    {
        /// <summary>
        /// Attributes of the user who generated the event. Some attributes may not be sent
        /// to LaunchDarkly if they are private.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Date/timestamp of the event.
        /// </summary>
        public long CreationDate { get; private set; }

        /// <summary>
        /// The unique key of the feature flag involved in the event.
        /// </summary>
        public string Key { get; private set; }
        
        internal Event(long creationDate, string key, User user)
        {
            CreationDate = creationDate;
            Key = key;
            User = user;
        }
    }

    /// <summary>
    /// An analytics event generated by feature flag evaluation. Used internally.
    /// </summary>
    /// <remarks>
    /// The event classes are public so as to be usable by custom implementations of
    /// <see cref="IEventProcessor"/>. Application code should not construct or modify events; they
    /// are generated by the client.
    /// </remarks>
    public class FeatureRequestEvent : Event
    {
        /// <summary>
        /// The variation index for the computed value of the flag.
        /// </summary>
        public int? Variation { get; private set; }

        /// <summary>
        /// The computed value of the flag.
        /// </summary>
        [Obsolete("Use ImmutableJsonValue; JToken is a mutable type")]
        public JToken Value => ImmutableJsonValue.InnerValue;

        /// <summary>
        /// The computed value of the flag.
        /// </summary>
        public ImmutableJsonValue ImmutableJsonValue { get; private set; }

        /// <summary>
        /// The default value of the flag.
        /// </summary>
        [Obsolete("Use ImmutableJsonDefault; JToken is a mutable type")]
        public JToken Default => ImmutableJsonDefault.InnerValue;

        /// <summary>
        /// The default value of the flag.
        /// </summary>
        public ImmutableJsonValue ImmutableJsonDefault { get; private set; }

        /// <summary>
        /// The version of the flag.
        /// </summary>
        public int? Version { get; private set; }
        
        /// <summary>
        /// The key of the flag that this flag is a prerequisite of, if any.
        /// </summary>
        public string PrereqOf { get; private set; }

        /// <summary>
        /// True if full-fidelity analytics events should be sent for this flag.
        /// </summary>
        public bool TrackEvents { get; private set; }

        /// <summary>
        /// If set, debug events are being generated until this date/time.
        /// </summary>
        public long? DebugEventsUntilDate { get; private set; }

        /// <summary>
        /// If set, this is a debug event.
        /// </summary>
        public bool Debug { get; private set; }

        /// <summary>
        /// An explanation of how the value was calculated, or null if the reason was not requested.
        /// </summary>
        public EvaluationReason Reason { get; private set; }

        internal FeatureRequestEvent(long creationDate, string key, User user, int? variation,
            ImmutableJsonValue value, ImmutableJsonValue defaultValue, int? version, string prereqOf,
            bool trackEvents, long? debugEventsUntilDate,
            bool debug, EvaluationReason reason) : base(creationDate, key, user)
        {
            Variation = variation;
            ImmutableJsonValue = value;
            ImmutableJsonDefault = defaultValue;
            Version = version;
            PrereqOf = prereqOf;
            TrackEvents = trackEvents;
            DebugEventsUntilDate = debugEventsUntilDate;
            Debug = debug;
            Reason = reason;
        }
    }

    /// <summary>
    /// An analytics event generated by the Track method. Used internally.
    /// </summary>
    /// <remarks>
    /// The event classes are public so as to be usable by custom implementations of
    /// <see cref="IEventProcessor"/>. Application code should not construct or modify events; they
    /// are generated by the client.
    /// </remarks>
    public class CustomEvent : Event
    {
        /// <summary>
        /// Custom data provided for the event.
        /// </summary>
        [Obsolete("Use ImmutableJsonData")]
        public string Data => ImmutableJsonData.IsNull ? null : ImmutableJsonData.AsString;

        /// <summary>
        /// Custom data provided for the event.
        /// </summary>
        [Obsolete("Use ImmutableJsonData; JToken is a mutable type")]
        public JToken JsonData => ImmutableJsonData.InnerValue;

        /// <summary>
        /// Custom data provided for the event.
        /// </summary>
        public ImmutableJsonValue ImmutableJsonData { get; private set; }

        /// <summary>
        /// An optional numeric value that can be used in analytics.
        /// </summary>
        public double? MetricValue { get; private set; }

        internal CustomEvent(long creationDate, string key, User user, ImmutableJsonValue data, double? metricValue) :
            base(creationDate, key, user)
        {
            ImmutableJsonData = data;
            MetricValue = metricValue;
        }
    }

    /// <summary>
    /// An analytics event generated by the Identify method. Used internally.
    /// </summary>
    /// <remarks>
    /// The event classes are public so as to be usable by custom implementations of
    /// <see cref="IEventProcessor"/>. Application code should not construct or modify events; they
    /// are generated by the client.
    /// </remarks>
    public class IdentifyEvent : Event
    {
        internal IdentifyEvent(long creationDate, User user) :
            base(creationDate, user == null ? null : user.Key, user)
        {
        }
    }

    /// <summary>
    /// An analytics event that captures user details from another event. Used internally.
    /// </summary>
    /// <remarks>
    /// The event classes are public so as to be usable by custom implementations of
    /// <see cref="IEventProcessor"/>. Application code should not construct or modify events; they
    /// are generated by the client.
    /// </remarks>
    internal class IndexEvent : Event
    {
        internal IndexEvent(long creationDate, User user) :
            base(creationDate, user.Key, user)
        { }
    }
}