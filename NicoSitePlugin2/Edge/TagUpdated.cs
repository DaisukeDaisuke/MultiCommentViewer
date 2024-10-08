// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: dwango/nicolive/chat/data/TagUpdated.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Dwango.Nicolive.Chat.Data {

  /// <summary>Holder for reflection information generated from dwango/nicolive/chat/data/TagUpdated.proto</summary>
  public static partial class TagUpdatedReflection {

    #region Descriptor
    /// <summary>File descriptor for dwango/nicolive/chat/data/TagUpdated.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TagUpdatedReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cipkd2FuZ28vbmljb2xpdmUvY2hhdC9kYXRhL1RhZ1VwZGF0ZWQucHJvdG8S",
            "GWR3YW5nby5uaWNvbGl2ZS5jaGF0LmRhdGEiqQEKClRhZ1VwZGF0ZWQSNwoE",
            "dGFncxgBIAMoCzIpLmR3YW5nby5uaWNvbGl2ZS5jaGF0LmRhdGEuVGFnVXBk",
            "YXRlZC5UYWcSFAoMb3duZXJfbG9ja2VkGAIgASgIGkwKA1RhZxIMCgR0ZXh0",
            "GAEgASgJEg4KBmxvY2tlZBgCIAEoCBIQCghyZXNlcnZlZBgDIAEoCBIVCg1u",
            "aWNvcGVkaWFfdXJpGAQgASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.TagUpdated), global::Dwango.Nicolive.Chat.Data.TagUpdated.Parser, new[]{ "Tags", "OwnerLocked" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.TagUpdated.Types.Tag), global::Dwango.Nicolive.Chat.Data.TagUpdated.Types.Tag.Parser, new[]{ "Text", "Locked", "Reserved", "NicopediaUri" }, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class TagUpdated : pb::IMessage<TagUpdated> {
    private static readonly pb::MessageParser<TagUpdated> _parser = new pb::MessageParser<TagUpdated>(() => new TagUpdated());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<TagUpdated> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Dwango.Nicolive.Chat.Data.TagUpdatedReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TagUpdated() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TagUpdated(TagUpdated other) : this() {
      tags_ = other.tags_.Clone();
      ownerLocked_ = other.ownerLocked_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TagUpdated Clone() {
      return new TagUpdated(this);
    }

    /// <summary>Field number for the "tags" field.</summary>
    public const int TagsFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Dwango.Nicolive.Chat.Data.TagUpdated.Types.Tag> _repeated_tags_codec
        = pb::FieldCodec.ForMessage(10, global::Dwango.Nicolive.Chat.Data.TagUpdated.Types.Tag.Parser);
    private readonly pbc::RepeatedField<global::Dwango.Nicolive.Chat.Data.TagUpdated.Types.Tag> tags_ = new pbc::RepeatedField<global::Dwango.Nicolive.Chat.Data.TagUpdated.Types.Tag>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Dwango.Nicolive.Chat.Data.TagUpdated.Types.Tag> Tags {
      get { return tags_; }
    }

    /// <summary>Field number for the "owner_locked" field.</summary>
    public const int OwnerLockedFieldNumber = 2;
    private bool ownerLocked_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool OwnerLocked {
      get { return ownerLocked_; }
      set {
        ownerLocked_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as TagUpdated);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(TagUpdated other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!tags_.Equals(other.tags_)) return false;
      if (OwnerLocked != other.OwnerLocked) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= tags_.GetHashCode();
      if (OwnerLocked != false) hash ^= OwnerLocked.GetHashCode();
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
      tags_.WriteTo(output, _repeated_tags_codec);
      if (OwnerLocked != false) {
        output.WriteRawTag(16);
        output.WriteBool(OwnerLocked);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += tags_.CalculateSize(_repeated_tags_codec);
      if (OwnerLocked != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(TagUpdated other) {
      if (other == null) {
        return;
      }
      tags_.Add(other.tags_);
      if (other.OwnerLocked != false) {
        OwnerLocked = other.OwnerLocked;
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
            tags_.AddEntriesFrom(input, _repeated_tags_codec);
            break;
          }
          case 16: {
            OwnerLocked = input.ReadBool();
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the TagUpdated message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class Tag : pb::IMessage<Tag> {
        private static readonly pb::MessageParser<Tag> _parser = new pb::MessageParser<Tag>(() => new Tag());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<Tag> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Dwango.Nicolive.Chat.Data.TagUpdated.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Tag() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Tag(Tag other) : this() {
          text_ = other.text_;
          locked_ = other.locked_;
          reserved_ = other.reserved_;
          nicopediaUri_ = other.nicopediaUri_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public Tag Clone() {
          return new Tag(this);
        }

        /// <summary>Field number for the "text" field.</summary>
        public const int TextFieldNumber = 1;
        private string text_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Text {
          get { return text_; }
          set {
            text_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        /// <summary>Field number for the "locked" field.</summary>
        public const int LockedFieldNumber = 2;
        private bool locked_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Locked {
          get { return locked_; }
          set {
            locked_ = value;
          }
        }

        /// <summary>Field number for the "reserved" field.</summary>
        public const int ReservedFieldNumber = 3;
        private bool reserved_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Reserved {
          get { return reserved_; }
          set {
            reserved_ = value;
          }
        }

        /// <summary>Field number for the "nicopedia_uri" field.</summary>
        public const int NicopediaUriFieldNumber = 4;
        private string nicopediaUri_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string NicopediaUri {
          get { return nicopediaUri_; }
          set {
            nicopediaUri_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as Tag);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(Tag other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (Text != other.Text) return false;
          if (Locked != other.Locked) return false;
          if (Reserved != other.Reserved) return false;
          if (NicopediaUri != other.NicopediaUri) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (Text.Length != 0) hash ^= Text.GetHashCode();
          if (Locked != false) hash ^= Locked.GetHashCode();
          if (Reserved != false) hash ^= Reserved.GetHashCode();
          if (NicopediaUri.Length != 0) hash ^= NicopediaUri.GetHashCode();
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
          if (Text.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(Text);
          }
          if (Locked != false) {
            output.WriteRawTag(16);
            output.WriteBool(Locked);
          }
          if (Reserved != false) {
            output.WriteRawTag(24);
            output.WriteBool(Reserved);
          }
          if (NicopediaUri.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(NicopediaUri);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (Text.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Text);
          }
          if (Locked != false) {
            size += 1 + 1;
          }
          if (Reserved != false) {
            size += 1 + 1;
          }
          if (NicopediaUri.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(NicopediaUri);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(Tag other) {
          if (other == null) {
            return;
          }
          if (other.Text.Length != 0) {
            Text = other.Text;
          }
          if (other.Locked != false) {
            Locked = other.Locked;
          }
          if (other.Reserved != false) {
            Reserved = other.Reserved;
          }
          if (other.NicopediaUri.Length != 0) {
            NicopediaUri = other.NicopediaUri;
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
                Text = input.ReadString();
                break;
              }
              case 16: {
                Locked = input.ReadBool();
                break;
              }
              case 24: {
                Reserved = input.ReadBool();
                break;
              }
              case 34: {
                NicopediaUri = input.ReadString();
                break;
              }
            }
          }
        }

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
