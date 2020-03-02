using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SchemeSharp
{
    public class SchemeContext
    {
        public SchemeContext Parent { get; }

        public ImmutableDictionary<string, SchemeVariable> Variable { get; }

        public SchemeContext(SchemeContext parent = null, ImmutableDictionary<string, SchemeVariable> variable = null)
        {
            Parent = parent;
            Variable = variable ?? ImmutableDictionary<string, SchemeVariable>.Empty;
        }

        public ISchemeValue Get(string key) => GetVariable(key)?.Value;

        public SchemeVariable GetVariable(string key) => Variable.ContainsKey(key) ? Variable[key] : Parent?.GetVariable(key);
        
        public SchemeContext Define(string key, ISchemeValue value)
        {
            return new SchemeContext(Parent, Variable.Add(key, new SchemeVariable(value)));
        }

        public void Set(string key, ISchemeValue value)
        {
            GetVariable(key).Value = value;
        }

        public bool ContainsKey(string key) => Get(key) != null;
    }

    public class SchemeVariable
    {
        public ISchemeValue Value { get; set; }

        public SchemeVariable(ISchemeValue value)
        {
            Value = value;
        }

        public SchemeVariable()
        {
        }
    }
}