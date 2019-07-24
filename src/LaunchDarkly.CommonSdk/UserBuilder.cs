﻿using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LaunchDarkly.Client
{
    /// <summary>
    /// A mutable object that uses the Builder pattern to specify properties for a <see cref="User"/> object.
    /// </summary>
    /// <remarks>
    /// Obtain an instance of this class by calling <see cref="User.Builder(string)"/>.
    /// 
    /// All of the builder methods for setting a user attribute return a reference to the same builder, so they can be
    /// chained together (see example). Some of them have the return type <see cref="IUserBuilderCanMakeAttributePrivate"/>
    /// rather than <c>IUserBuilder</c>; those are the user attributes that can be designated as private.
    /// </remarks>
    /// <example>
    /// <code>
    ///     var user = User.Build("my-key").Name("Bob").Email("test@example.com").Build();
    /// </code>
    /// </example>
    public interface IUserBuilder
    {
        /// <summary>
        /// Creates a <see cref="User"/> based on the properties that have been set on the builder.
        /// Modifying the builder after this point does not affect the returned <c>User</c>.
        /// </summary>
        /// <returns>the configured <c>User</c> object</returns>
        User Build();

        /// <summary>
        /// Sets the secondary key for a user.
        /// </summary>
        /// <remarks>
        /// This affects <a href="https://docs.launchdarkly.com/docs/targeting-users#section-targeting-rules-based-on-user-attributes">feature flag targeting</a>
        /// as follows: if you have chosen to bucket users by a specific attribute, the secondary key (if set)
        /// is used to further distinguish between users who are otherwise identical according to that attribute.
        /// </remarks>
        /// <param name="secondaryKey">the secondary key</param>
        /// <returns>the same builder</returns>
        IUserBuilder SecondaryKey(string secondaryKey);

        /// <summary>
        /// Sets the IP address for a user.
        /// </summary>
        /// <param name="ipAddress">the IP address for the user</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate IPAddress(string ipAddress);

        /// <summary>
        /// Sets the country identifier for a user.
        /// </summary>
        /// <remarks>
        /// This is commonly either a 2- or 3-character standard country code, but LaunchDarkly does not validate
        /// this property or restrict its possible values.
        /// </remarks>
        /// <param name="country">the country for the user</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Country(string country);

        /// <summary>
        /// Sets the first name for a user.
        /// </summary>
        /// <param name="firstName">the first name for the user</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate FirstName(string firstName);

        /// <summary>
        /// Sets the last name for a user.
        /// </summary>
        /// <param name="lastName">the last name for the user</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate LastName(string lastName);

        /// <summary>
        /// Sets the full name for a user.
        /// </summary>
        /// <param name="name">the name for the user</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Name(string name);

        /// <summary>
        /// Sets the avatar URL for a user.
        /// </summary>
        /// <param name="avatar">the avatar URL for the user</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Avatar(string avatar);

        /// <summary>
        /// Sets the email address for a user.
        /// </summary>
        /// <param name="email">the email address for the user</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Email(string email);

        /// <summary>
        /// Sets whether this user is anonymous, meaning that the user key will not appear on your LaunchDarkly dashboard.
        /// </summary>
        /// <param name="anonymous">true if the user is anonymous</param>
        /// <returns>the same builder</returns>
        IUserBuilder Anonymous(bool anonymous);

        /// <summary>
        /// Adds a custom attribute whose value is a JSON value of any kind.
        /// </summary>
        /// <remarks>
        /// When set to one of the <a href="http://docs.launchdarkly.com/docs/targeting-users#targeting-based-on-user-attributes">built-in
        /// user attribute keys</a>, this custom attribute will be ignored.
        /// </remarks>
        /// <param name="name">the key for the custom attribute</param>
        /// <param name="value">the value for the custom attribute</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Custom(string name, JToken value);

        /// <summary>
        /// Adds a custom attribute with a boolean value.
        /// </summary>
        /// <remarks>
        /// When set to one of the <a href="http://docs.launchdarkly.com/docs/targeting-users#targeting-based-on-user-attributes">built-in
        /// user attribute keys</a>, this custom attribute will be ignored.
        /// </remarks>
        /// <param name="name">the key for the custom attribute</param>
        /// <param name="value">the value for the custom attribute</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Custom(string name, bool value);

        /// <summary>
        /// Adds a custom attribute with a string value.
        /// </summary>
        /// <remarks>
        /// When set to one of the <a href="http://docs.launchdarkly.com/docs/targeting-users#targeting-based-on-user-attributes">built-in
        /// user attribute keys</a>, this custom attribute will be ignored.
        /// </remarks>
        /// <param name="name">the key for the custom attribute</param>
        /// <param name="value">the value for the custom attribute</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Custom(string name, string value);

        /// <summary>
        /// Adds a custom attribute with an integer value.
        /// </summary>
        /// <remarks>
        /// When set to one of the <a href="http://docs.launchdarkly.com/docs/targeting-users#targeting-based-on-user-attributes">built-in
        /// user attribute keys</a>, this custom attribute will be ignored.
        /// </remarks>
        /// <param name="name">the key for the custom attribute</param>
        /// <param name="value">the value for the custom attribute</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Custom(string name, int value);

        /// <summary>
        /// Adds a custom attribute with a floating-point value.
        /// </summary>
        /// <remarks>
        /// When set to one of the <a href="http://docs.launchdarkly.com/docs/targeting-users#targeting-based-on-user-attributes">built-in
        /// user attribute keys</a>, this custom attribute will be ignored.
        /// </remarks>
        /// <param name="name">the key for the custom attribute</param>
        /// <param name="value">the value for the custom attribute</param>
        /// <returns>the same builder</returns>
        IUserBuilderCanMakeAttributePrivate Custom(string name, float value);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IUserBuilderCanMakeAttributePrivate : IUserBuilder
    {
        /// <summary>
        /// Marks the last attribute that was set on this builder as being a private attribute: that is, its value will not be
        /// sent to LaunchDarkly.
        /// </summary>
        /// <remarks>
        /// This action only affects analytics events that are generated by this particular user object. To mark some (or all)
        /// user attribues as private for <i>all</i> users, use the configuration properties <see cref="LaunchDarkly.Common.IBaseConfiguration.PrivateAttributeNames"/>
        /// and <see cref="LaunchDarkly.Common.IBaseConfiguration.AllAttributesPrivate"/>.
        /// 
        /// Not all attributes can be made private: <c>Key</c>, <c>SecondaryKey</c>, and <c>Anonymous</c> cannot be private. This
        /// is enforced by the compiler, since the builder methods for attributes that can be made private are the only ones that
        /// return <see cref="IUserBuilderCanMakeAttributePrivate"/>; therefore, you cannot write an expression like
        /// `User.Builder("user-key").AsPrivateAttribute()` or `User.Builder("user-key").SecondaryKey("secondary").AsPrivateAttribute()`.
        /// </remarks>
        /// <example>
        /// In this example, <c>FirstName</c> and <c>LastName</c> are marked as private, but <c>Country</c> is not.
        /// 
        /// <code>
        ///     var user = User.Build("user-key")
        ///         .FirstName("Pierre").AsPrivateAttribute()
        ///         .LastName("Menard").AsPrivateAttribute()
        ///         .Country("ES")
        ///         .Build();
        /// </code>
        /// </example>
        /// <returns>the same builder</returns>
        IUserBuilder AsPrivateAttribute();
    }

    internal class UserBuilder : IUserBuilder
    {
        private readonly string _key;
        private string _secondaryKey;
        private string _ipAddress;
        private string _country;
        private string _firstName;
        private string _lastName;
        private string _name;
        private string _avatar;
        private string _email;
        private bool _anonymous;
        private HashSet<string> _privateAttributeNames;
        private Dictionary<string, JToken> _custom;

        internal UserBuilder(string key)
        {
            _key = key;
        }

        internal UserBuilder(User fromUser)
        {
            _key = fromUser.Key;
            _secondaryKey = fromUser.SecondaryKey;
            _ipAddress = fromUser.IPAddress;
            _country = fromUser.Country;
            _firstName = fromUser.FirstName;
            _lastName = fromUser.LastName;
            _name = fromUser.Name;
            _avatar = fromUser.Avatar;
            _email = fromUser.Email;
            _anonymous = fromUser.Anonymous.HasValue && fromUser.Anonymous.Value;
            _privateAttributeNames = fromUser.PrivateAttributeNames == null ? null :
                new HashSet<string>(fromUser.PrivateAttributeNames);
            _custom = fromUser.Custom == null ? null :
                new Dictionary<string, JToken>(fromUser.Custom);
        }

        public User Build()
        {
#pragma warning disable 618
            return new User(_key)
            {
                SecondaryKey = _secondaryKey,
#pragma warning disable 618
                IpAddress = _ipAddress,
#pragma warning restore 618
                Country = _country,
                FirstName = _firstName,
                LastName = _lastName,
                Name = _name,
                Avatar = _avatar,
                Email = _email,
                Anonymous = _anonymous ? (bool?)true : null,
                PrivateAttributeNames = _privateAttributeNames == null ? null :
                    new HashSet<string>(_privateAttributeNames),
                Custom = _custom == null ? new Dictionary<string, JToken>() :
                    new Dictionary<string, JToken>(_custom)
            };
#pragma warning restore 618
        }

        public IUserBuilder SecondaryKey(string secondaryKey)
        {
            _secondaryKey = secondaryKey;
            return this;
        }

        public IUserBuilderCanMakeAttributePrivate IPAddress(string ipAddress)
        {
            _ipAddress = ipAddress;
            return CanMakeAttributePrivate("ip");
        }
        
        public IUserBuilderCanMakeAttributePrivate Country(string country)
        {
            _country = country;
            return CanMakeAttributePrivate("country");
        }
        
        public IUserBuilderCanMakeAttributePrivate FirstName(string firstName)
        {
            _firstName = firstName;
            return CanMakeAttributePrivate("firstName");
        }
        
        public IUserBuilderCanMakeAttributePrivate LastName(string lastName)
        {
            _lastName = lastName;
            return CanMakeAttributePrivate("lastName");
        }
        
        public IUserBuilderCanMakeAttributePrivate Name(string name)
        {
            _name = name;
            return CanMakeAttributePrivate("name");
        }
        
        public IUserBuilderCanMakeAttributePrivate Avatar(string avatar)
        {
            _avatar = avatar;
            return CanMakeAttributePrivate("avatar");
        }
        
        public IUserBuilderCanMakeAttributePrivate Email(string email)
        {
            _email = email;
            return CanMakeAttributePrivate("email");
        }
        
        public IUserBuilder Anonymous(bool anonymous)
        {
            _anonymous = anonymous;
            return this;
        }

        public IUserBuilderCanMakeAttributePrivate Custom(string name, JToken value)
        {
            if (_custom == null)
            {
                _custom = new Dictionary<string, JToken>();
            }
            _custom[name] = value;
            return CanMakeAttributePrivate(name);
        }
        
        public IUserBuilderCanMakeAttributePrivate Custom(string name, bool value)
        {
            return Custom(name, new JValue(value));
        }
        
        public IUserBuilderCanMakeAttributePrivate Custom(string name, string value)
        {
            return Custom(name, new JValue(value));
        }
        
        public IUserBuilderCanMakeAttributePrivate Custom(string name, int value)
        {
            return Custom(name, new JValue(value));
        }
        
        public IUserBuilderCanMakeAttributePrivate Custom(string name, float value)
        {
            return Custom(name, new JValue(value));
        }

        private IUserBuilderCanMakeAttributePrivate CanMakeAttributePrivate(string attrName)
        {
            return new UserBuilderCanMakeAttributePrivate(this, attrName);
        }

        internal IUserBuilder AddPrivateAttribute(string attrName)
        {
            if (_privateAttributeNames == null)
            {
                _privateAttributeNames = new HashSet<string>();
            }
            _privateAttributeNames.Add(attrName);
            return this;
        }
    }

    internal class UserBuilderCanMakeAttributePrivate : IUserBuilderCanMakeAttributePrivate
    {
        private readonly UserBuilder _builder;
        private readonly string _attrName;

        internal UserBuilderCanMakeAttributePrivate(UserBuilder builder, string attrName)
        {
            _builder = builder;
            _attrName = attrName;
        }

        public User Build()
        {
            return _builder.Build();
        }

        public IUserBuilder SecondaryKey(string secondaryKey)
        {
            return _builder.SecondaryKey(secondaryKey);
        }

        public IUserBuilderCanMakeAttributePrivate IPAddress(string ipAddress)
        {
            return _builder.IPAddress(ipAddress);
        }

        public IUserBuilderCanMakeAttributePrivate Country(string country)
        {
            return _builder.Country(country);
        }

        public IUserBuilderCanMakeAttributePrivate FirstName(string firstName)
        {
            return _builder.FirstName(firstName);
        }

        public IUserBuilderCanMakeAttributePrivate LastName(string lastName)
        {
            return _builder.LastName(lastName);
        }

        public IUserBuilderCanMakeAttributePrivate Name(string name)
        {
            return _builder.Name(name);
        }

        public IUserBuilderCanMakeAttributePrivate Avatar(string avatar)
        {
            return _builder.Avatar(avatar);
        }

        public IUserBuilderCanMakeAttributePrivate Email(string email)
        {
            return _builder.Email(email);
        }

        public IUserBuilder Anonymous(bool anonymous)
        {
            return _builder.Anonymous(anonymous);
        }

        public IUserBuilderCanMakeAttributePrivate Custom(string name, JToken value)
        {
            return _builder.Custom(name, value);
        }

        public IUserBuilderCanMakeAttributePrivate Custom(string name, bool value)
        {
            return _builder.Custom(name, value);
        }

        public IUserBuilderCanMakeAttributePrivate Custom(string name, string value)
        {
            return _builder.Custom(name, value);
        }

        public IUserBuilderCanMakeAttributePrivate Custom(string name, int value)
        {
            return _builder.Custom(name, value);
        }

        public IUserBuilderCanMakeAttributePrivate Custom(string name, float value)
        {
            return _builder.Custom(name, value);
        }

        public IUserBuilder AsPrivateAttribute()
        {
            return _builder.AddPrivateAttribute(_attrName);
        }
    }
}
