// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET6_0_OR_GREATER
namespace Microsoft.AspNetCore.Internal;

/// <summary>
/// <para lang="zh">An 枚举erable that can supply the name/value pairs from a URI query string.
///</para>
/// <para lang="en">An enumerable that can supply the name/value pairs from a URI query string.
///</para>
/// </summary>
[ExcludeFromCodeCoverage]
internal readonly struct QueryStringEnumerable
{
    private readonly ReadOnlyMemory<char> _queryString;

    /// <summary>
    /// <para lang="zh">Constructs an 实例 of <see cref="QueryStringEnumerable"/>.
    ///</para>
    /// <para lang="en">Constructs an instance of <see cref="QueryStringEnumerable"/>.
    ///</para>
    /// </summary>
    /// <param name="queryString">The query string.</param>
    public QueryStringEnumerable(string? queryString)
        : this(queryString.AsMemory())
    {
    }

    /// <summary>
    /// <para lang="zh">Constructs an 实例 of <see cref="QueryStringEnumerable"/>.
    ///</para>
    /// <para lang="en">Constructs an instance of <see cref="QueryStringEnumerable"/>.
    ///</para>
    /// </summary>
    /// <param name="queryString">The query string.</param>
    public QueryStringEnumerable(ReadOnlyMemory<char> queryString)
    {
        _queryString = queryString;
    }

    /// <summary>
    /// <para lang="zh">Retrieves an object that can iterate through the name/value pairs in the query string.
    ///</para>
    /// <para lang="en">Retrieves an object that can iterate through the name/value pairs in the query string.
    ///</para>
    /// </summary>
    /// <returns>An object that can iterate through the name/value pairs in the query string.</returns>
    public Enumerator GetEnumerator() => new(_queryString);

    /// <summary>
    /// <para lang="zh">Represents a single name/value pair extracted from a query string during 枚举eration.
    ///</para>
    /// <para lang="en">Represents a single name/value pair extracted from a query string during enumeration.
    ///</para>
    /// </summary>
    public readonly struct EncodedNameValuePair
    {
        /// <summary>
        /// <para lang="zh">获得 the name from this name/value pair in its original encoded form.
        /// To get the decoded string, call <see cref="DecodeName"/>.
        ///</para>
        /// <para lang="en">Gets the name from this name/value pair in its original encoded form.
        /// To get the decoded string, call <see cref="DecodeName"/>.
        ///</para>
        /// </summary>
        public readonly ReadOnlyMemory<char> EncodedName { get; }

        /// <summary>
        /// <para lang="zh">获得 the value from this name/value pair in its original encoded form.
        /// To get the decoded string, call <see cref="DecodeValue"/>.
        ///</para>
        /// <para lang="en">Gets the value from this name/value pair in its original encoded form.
        /// To get the decoded string, call <see cref="DecodeValue"/>.
        ///</para>
        /// </summary>
        public readonly ReadOnlyMemory<char> EncodedValue { get; }

        internal EncodedNameValuePair(ReadOnlyMemory<char> encodedName, ReadOnlyMemory<char> encodedValue)
        {
            EncodedName = encodedName;
            EncodedValue = encodedValue;
        }

        /// <summary>
        /// <para lang="zh">Decodes the name from this name/value pair.
        ///</para>
        /// <para lang="en">Decodes the name from this name/value pair.
        ///</para>
        /// </summary>
        /// <returns>Characters representing the decoded name.</returns>
        public ReadOnlyMemory<char> DecodeName()
            => Decode(EncodedName);

        /// <summary>
        /// <para lang="zh">Decodes the value from this name/value pair.
        ///</para>
        /// <para lang="en">Decodes the value from this name/value pair.
        ///</para>
        /// </summary>
        /// <returns>Characters representing the decoded value.</returns>
        public ReadOnlyMemory<char> DecodeValue()
            => Decode(EncodedValue);

        private static ReadOnlyMemory<char> Decode(ReadOnlyMemory<char> chars)
        {
            return Uri.UnescapeDataString(chars.ToString()).AsMemory();

            // If the value is short, it's cheap to check up front if it really needs decoding. If it doesn't,
            // then we can save some allocations.
            //return chars.Length < 16 && chars.Span.IndexOfAny('%', '+') < 0
            //    ? chars
            //    : Uri.UnescapeDataString(chars.Span.ToString().Replace("+", " ")).AsMemory();
        }
    }

    /// <summary>
    /// <para lang="zh">An 枚举erator that supplies the name/value pairs from a URI query string.
    ///</para>
    /// <para lang="en">An enumerator that supplies the name/value pairs from a URI query string.
    ///</para>
    /// </summary>
    public struct Enumerator
    {
        private ReadOnlyMemory<char> _query;

        internal Enumerator(ReadOnlyMemory<char> query)
        {
            Current = default;
            _query = query.IsEmpty || query.Span[0] != '?'
                ? query
                : query.Slice(1);
        }

        /// <summary>
        /// <para lang="zh">获得 the currently referenced key/value pair in the query string being 枚举erated.
        ///</para>
        /// <para lang="en">Gets the currently referenced key/value pair in the query string being enumerated.
        ///</para>
        /// </summary>
        public EncodedNameValuePair Current { get; private set; }

        /// <summary>
        /// <para lang="zh">Moves to the next key/value pair in the query string being 枚举erated.
        ///</para>
        /// <para lang="en">Moves to the next key/value pair in the query string being enumerated.
        ///</para>
        /// </summary>
        /// <returns>True if there is another key/value pair, otherwise false.</returns>
        public bool MoveNext()
        {
            while (!_query.IsEmpty)
            {
                // Chomp off the next segment
                ReadOnlyMemory<char> segment;
                var delimiterIndex = _query.Span.IndexOf('&');
                if (delimiterIndex >= 0)
                {
                    segment = _query[..delimiterIndex];
                    _query = _query[(delimiterIndex + 1)..];
                }
                else
                {
                    segment = _query;
                    _query = default;
                }

                // If it's nonempty, emit it
                var equalIndex = segment.Span.IndexOf('=');
                if (equalIndex >= 0)
                {
                    Current = new EncodedNameValuePair(
                        segment[..equalIndex],
                        segment[(equalIndex + 1)..]);
                    return true;
                }
                else if (!segment.IsEmpty)
                {
                    Current = new EncodedNameValuePair(segment, default);
                    return true;
                }
            }

            Current = default;
            return false;
        }
    }
}
#endif
