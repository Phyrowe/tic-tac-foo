using System;

namespace TicTacFoo.Logic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TransientAttribute : Attribute
    {
    }
}