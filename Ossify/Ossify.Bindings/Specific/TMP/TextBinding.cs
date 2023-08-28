using System;
using TMPro;

namespace Ossify.Bindings.Specific.TMP
{
    public class TextBinding : GetterBinding<TMP_Text, string>
    {
        /// <inheritdoc />
        public TextBinding(TMP_Text bound, Func<string> getter) : base(bound, getter) { }

        /// <inheritdoc />
        protected override void SetValue(string value)
        {
            Bound.text = value;
        }
    }
}