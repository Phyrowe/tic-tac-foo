using System;

namespace TicTacFoo.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TransientAttribute : Attribute
    {
    }
}