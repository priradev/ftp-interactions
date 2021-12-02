using System;
using System.Collections.Generic;

namespace PriRa.GitHub.Actions.Ftp
{
    internal class ItemComparer : IEqualityComparer<Item>
    {

        public static ItemComparer Default { get; } = new ItemComparer();

        public bool Equals(Item? left, Item? right)
        {
            return string.Equals(left?.LocalPath, right?.LocalPath, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Item item)
        {
            return item.LocalPath.GetHashCode();
        }

    }
}