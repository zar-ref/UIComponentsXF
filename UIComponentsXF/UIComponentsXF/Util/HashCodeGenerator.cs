using System;
namespace UIComponentsXF.Util
{
    public static class HashCodeGenerator
    {

        public static int GenerateCustomControlHashCode()
        {
             return Guid.NewGuid().GetHashCode();
        }
    }
}
