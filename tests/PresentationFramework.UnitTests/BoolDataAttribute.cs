using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PresentationFramework.UnitTests
{
    public class BoolDataAttribute : CommonMemberDataAttribute
    {
        public BoolDataAttribute() : base(typeof(BoolDataAttribute), nameof(TheoryData)) { }

        public static ReadOnlyTheoryData TheoryData { get; } = new(true, false);
    }

    public class CommonMemberDataAttribute : MemberDataAttributeBase
    {
        public CommonMemberDataAttribute(Type memberType, string memberName = "TheoryData")
            : this(memberType, memberName, null) { }

        public CommonMemberDataAttribute(Type memberType, string memberName, params object[]? parameters)
            : base(memberName, parameters)
        {
            MemberType = memberType;
        }

        protected override object[]? ConvertDataItem(MethodInfo testMethod, object? item)
        {
            if (item is null)
            {
                return null;
            }

            if (item is not object[] array)
            {
                throw new ArgumentException($"Property {MemberName} on {MemberType ?? testMethod.DeclaringType} yielded an item that is not an object[], but {item.GetType().Name}");
            }

            return array;
        }
    }


    public class StringWithNullDataAttribute : CommonMemberDataAttribute
    {
        public StringWithNullDataAttribute() : base(typeof(StringWithNullDataAttribute)) { }

        public static ReadOnlyTheoryData TheoryData { get; } = new(null, string.Empty, "teststring");
    }
    public class ReadOnlyTheoryData : IEnumerable<object?[]>
    {
        private readonly IEnumerable<object?[]> _data;

        public ReadOnlyTheoryData(IEnumerable<object?[]> data) => _data = data;

        public ReadOnlyTheoryData(IEnumerable data)
            => _data = data.Cast<object>().Select(i => new object?[] { i }).ToArray();

        public ReadOnlyTheoryData(IEnumerable<object> data)
            => _data = data.Select(i => new object?[] { i }).ToArray();

        public ReadOnlyTheoryData(params object?[] data)
            => _data = data.Select(i => new object?[] { i }).ToArray();

        IEnumerator<object?[]> IEnumerable<object?[]>.GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}
