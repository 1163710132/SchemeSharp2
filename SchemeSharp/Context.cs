using System;
using System.Collections.Generic;
using System.Text;

namespace SchemeSharp
{
    public class SchemeContext
    {
        public SchemeContext Parent { get; }

        public SchemeContext(SchemeContext parent = null)
        {
            Parent = parent;
            Variable = new Dictionary<string, ISchemeValue>();
        }

        public Dictionary<string, ISchemeValue> Variable { get; }

        public ISchemeValue Get(string key) => Variable.ContainsKey(key) ? Variable[key] : Parent?.Get(key);

        public void Define(string key, ISchemeValue value)
        {
            Variable[key] = value;
        }

        public void Set(string key, ISchemeValue value)
        {
            if (Variable.ContainsKey(key))
            {
                Variable[key] = value;
            }
            else
            {
                Parent?.Set(key, value);
            }
        }

        public bool ContainsKey(string key) => Get(key) != null;
    }
}