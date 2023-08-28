namespace Ossify.Editor.Editor.Generator
{
    public interface ITypeTemplate
    {
        string TransformText(); 

        string FileName { get; }

        int MenuOrder { get; set; }
        string MenuName { get; set; }
        string Namespace { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string FriendlyName { get; set; }
    }

    public partial class VariableTemplate : ITypeTemplate { }

    public partial class VariablePublisherTemplate : ITypeTemplate { }

    public partial class VariableReferenceTemplate : ITypeTemplate { }

    public partial class VariableSubscriberTemplate : ITypeTemplate { }

    public partial class DispenserTemplate : ITypeTemplate { }

    public partial class ScriptableCollectionTemplate : ITypeTemplate { }
}