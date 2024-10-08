// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: dwango/nicolive/chat/data/OperatorComment.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Dwango.Nicolive.Chat.Data {

  /// <summary>Holder for reflection information generated from dwango/nicolive/chat/data/OperatorComment.proto</summary>
  public static partial class OperatorCommentReflection {

    #region Descriptor
    /// <summary>File descriptor for dwango/nicolive/chat/data/OperatorComment.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static OperatorCommentReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ci9kd2FuZ28vbmljb2xpdmUvY2hhdC9kYXRhL09wZXJhdG9yQ29tbWVudC5w",
            "cm90bxIZZHdhbmdvLm5pY29saXZlLmNoYXQuZGF0YRokZHdhbmdvL25pY29s",
            "aXZlL2NoYXQvZGF0YS9DaGF0LnByb3RvInoKD09wZXJhdG9yQ29tbWVudBIP",
            "Cgdjb250ZW50GAEgASgJEgwKBG5hbWUYAiABKAkSOgoIbW9kaWZpZXIYAyAB",
            "KAsyKC5kd2FuZ28ubmljb2xpdmUuY2hhdC5kYXRhLkNoYXQuTW9kaWZpZXIS",
            "DAoEbGluaxgEIAEoCWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Dwango.Nicolive.Chat.Data.ChatReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.OperatorComment), global::Dwango.Nicolive.Chat.Data.OperatorComment.Parser, new[]{ "Content", "Name", "Modifier", "Link" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class OperatorComment : pb::IMessage<OperatorComment> {
    private static readonly pb::MessageParser<OperatorComment> _parser = new pb::MessageParser<OperatorComment>(() => new OperatorComment());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<OperatorComment> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Dwango.Nicolive.Chat.Data.OperatorCommentReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public OperatorComment() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public OperatorComment(OperatorComment other) : this() {
      content_ = other.content_;
      name_ = other.name_;
      modifier_ = other.modifier_ != null ? other.modifier_.Clone() : null;
      link_ = other.link_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public OperatorComment Clone() {
      return new OperatorComment(this);
    }

    /// <summary>Field number for the "content" field.</summary>
    public const int ContentFieldNumber = 1;
    private string content_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Content {
      get { return content_; }
      set {
        content_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "modifier" field.</summary>
    public const int ModifierFieldNumber = 3;
    private global::Dwango.Nicolive.Chat.Data.Chat.Types.Modifier modifier_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Dwango.Nicolive.Chat.Data.Chat.Types.Modifier Modifier {
      get { return modifier_; }
      set {
        modifier_ = value;
      }
    }

    /// <summary>Field number for the "link" field.</summary>
    public const int LinkFieldNumber = 4;
    private string link_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Link {
      get { return link_; }
      set {
        link_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as OperatorComment);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(OperatorComment other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Content != other.Content) return false;
      if (Name != other.Name) return false;
      if (!object.Equals(Modifier, other.Modifier)) return false;
      if (Link != other.Link) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Content.Length != 0) hash ^= Content.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (modifier_ != null) hash ^= Modifier.GetHashCode();
      if (Link.Length != 0) hash ^= Link.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Content.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Content);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (modifier_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Modifier);
      }
      if (Link.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Link);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Content.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Content);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (modifier_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Modifier);
      }
      if (Link.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Link);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(OperatorComment other) {
      if (other == null) {
        return;
      }
      if (other.Content.Length != 0) {
        Content = other.Content;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.modifier_ != null) {
        if (modifier_ == null) {
          modifier_ = new global::Dwango.Nicolive.Chat.Data.Chat.Types.Modifier();
        }
        Modifier.MergeFrom(other.Modifier);
      }
      if (other.Link.Length != 0) {
        Link = other.Link;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Content = input.ReadString();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            if (modifier_ == null) {
              modifier_ = new global::Dwango.Nicolive.Chat.Data.Chat.Types.Modifier();
            }
            input.ReadMessage(modifier_);
            break;
          }
          case 34: {
            Link = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
