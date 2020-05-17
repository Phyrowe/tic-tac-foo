using System;

namespace TicTacFoo.Logic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ScopedAttribute : Attribute
    {
    }
}