using Sirenix.OdinInspector;

namespace Ossify.Microtypes
{
    [UnityEngine.CreateAssetMenu(order = Consts.VariableOrder, menuName = "Ossify/Text")]
    public sealed class TextVariable : Variable<string>
    {
        [HideIf(nameof(ShowVolatileValue)),
         OnValueChanged(nameof(OnEditorChangedProtectedValue)),
         ShowInInspector,
         HideLabel,
         InlineProperty,
         MultiLineProperty(10)]
        private string ProtectedValue
        {
            get => Value;
            set => SetProtectedValue(value);
        }
        
         [ShowIf(nameof(ShowVolatileValue)),
          OnValueChanged(nameof(OnEditorChangedVolatileValue)),
          ShowInInspector,
          HideLabel,
          InlineProperty,
          MultiLineProperty(10)]
         private string VolatileValue
         {
             get => Value;
             set => Value = value;
         }
    }
}