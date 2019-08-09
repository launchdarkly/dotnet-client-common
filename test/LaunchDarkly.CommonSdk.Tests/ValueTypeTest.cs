﻿using LaunchDarkly.Client;
using Newtonsoft.Json.Linq;
using Xunit;

namespace LaunchDarkly.Common.Tests
{
    public class ValueTypeTest
    {
        private const int jsonIntValue = 3;
        private const float jsonFloatValue = 3.25f;
        private const string jsonStringValue = "hi";

        private static readonly ImmutableJsonValue jsonBoolTrue = ImmutableJsonValue.Of(true);
        private static readonly ImmutableJsonValue jsonInt = ImmutableJsonValue.Of(jsonIntValue);
        private static readonly ImmutableJsonValue jsonFloat = ImmutableJsonValue.Of(jsonFloatValue);
        private static readonly ImmutableJsonValue jsonString = ImmutableJsonValue.Of(jsonStringValue);
        private static readonly ImmutableJsonValue jsonArray = ImmutableJsonValue.FromJToken(new JArray { new JValue("item") });
        private static readonly ImmutableJsonValue jsonObject = ImmutableJsonValue.FromJToken(new JObject { { "a", new JValue("b") } });

        [Fact]
        public void BoolFromJson()
        {
            Assert.True(ValueTypes.Bool.ValueFromJson(ImmutableJsonValue.Of(true)));
        }

        [Fact]
        public void BoolFromNonBoolValueIsError()
        {
            VerifyConversionError(ValueTypes.Bool, new ImmutableJsonValue[] {
                ImmutableJsonValue.Null, jsonInt, jsonFloat, jsonString, jsonArray, jsonObject
            });
        }

        [Fact]
        public void BoolToJson()
        {
            Assert.Equal(jsonBoolTrue, ValueTypes.Bool.ValueToJson(true));
        }
        
        [Fact]
        public void IntFromJsonInt()
        {
            Assert.Equal(jsonIntValue, ValueTypes.Int.ValueFromJson(jsonInt));
        }

        [Fact]
        public void IntFromJsonFloatRoundsToNearest()
        {
            // This behavior is defined by the Newtonsoft.Json conversion operator that we have been
            // relying on in the .NET SDK, so we must preserve it until the next major version.
            Assert.Equal(2, ValueTypes.Int.ValueFromJson(ImmutableJsonValue.Of(2.25f)));
            Assert.Equal(3, ValueTypes.Int.ValueFromJson(ImmutableJsonValue.Of(2.75f)));
        }

        [Fact]
        public void IntFromNonNumericValueIsError()
        {
            VerifyConversionError(ValueTypes.Int, new ImmutableJsonValue[] {
                ImmutableJsonValue.Null, jsonBoolTrue, jsonString, jsonArray, jsonObject
            });
        }

        [Fact]
        public void IntToJson()
        {
            Assert.Equal(jsonInt, ValueTypes.Int.ValueToJson(jsonIntValue));
        }
        
        [Fact]
        public void FloatFromJsonFloat()
        {
            Assert.Equal(jsonFloatValue, ValueTypes.Float.ValueFromJson(jsonFloat));
        }

        [Fact]
        public void FloatFromJsonInt()
        {
            Assert.Equal((float)jsonIntValue, ValueTypes.Float.ValueFromJson(jsonInt));
        }

        [Fact]
        public void FloatFromNonNumericValueIsError()
        {
            VerifyConversionError(ValueTypes.Float, new ImmutableJsonValue[] {
                ImmutableJsonValue.Null, ImmutableJsonValue.Of(true), jsonString, jsonArray, jsonObject
            });
        }

        [Fact]
        public void FloatToJson()
        {
            Assert.Equal(jsonFloat, ValueTypes.Float.ValueToJson(jsonFloatValue));
        }

        [Fact]
        public void StringFromJson()
        {
            Assert.Equal(jsonStringValue, ValueTypes.String.ValueFromJson(jsonString));
        }

        [Fact]
        public void StringFromNull()
        {
            Assert.Null(ValueTypes.String.ValueFromJson(ImmutableJsonValue.Null));
        }

        [Fact]
        public void StringFromJsonNull()
        {
            Assert.Null(ValueTypes.String.ValueFromJson(ImmutableJsonValue.Null));
        }

        [Fact]
        public void StringFromNonStringValueIsError()
        {
            VerifyConversionError(ValueTypes.String, new ImmutableJsonValue[] {
                jsonBoolTrue, jsonInt, jsonFloat, jsonArray, jsonObject
            });
        }

        [Fact]
        public void StringToJson()
        {
            Assert.Equal(jsonString, ValueTypes.String.ValueToJson(jsonStringValue));
        }

        [Fact]
        public void JsonFromJson()
        {
            Assert.Same(jsonObject.InnerValue, ValueTypes.Json.ValueFromJson(jsonObject).InnerValue);
        }

        [Fact]
        public void JsonFromNull()
        {
            Assert.Equal(ImmutableJsonValue.Null, ValueTypes.Json.ValueFromJson(ImmutableJsonValue.Null));
        }

        [Fact]
        public void JsonToJson()
        {
            Assert.Same(jsonObject.InnerValue, ValueTypes.Json.ValueToJson(jsonObject).InnerValue);
        }

        private void VerifyConversionError<T>(ValueType<T> type, ImmutableJsonValue[] badValues)
        {
            foreach (var v in badValues)
            {
                try
                {
                    type.ValueFromJson(v);
                    Assert.True(false, "converting from " + (v.IsNull ? "null" : v.AsJToken().Type.ToString()) +
                        " should throw exception");
                }
                catch (ValueTypeException) { }
            }
        }
    }
}
