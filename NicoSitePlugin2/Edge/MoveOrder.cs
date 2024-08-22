// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: dwango/nicolive/chat/data/MoveOrder.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Dwango.Nicolive.Chat.Data {

  /// <summary>Holder for reflection information generated from dwango/nicolive/chat/data/MoveOrder.proto</summary>
  public static partial class MoveOrderReflection {

    #region Descriptor
    /// <summary>File descriptor for dwango/nicolive/chat/data/MoveOrder.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MoveOrderReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cilkd2FuZ28vbmljb2xpdmUvY2hhdC9kYXRhL01vdmVPcmRlci5wcm90bxIZ",
            "ZHdhbmdvLm5pY29saXZlLmNoYXQuZGF0YRokZHdhbmdvL25pY29saXZlL2No",
            "YXQvZGF0YS9KdW1wLnByb3RvGihkd2FuZ28vbmljb2xpdmUvY2hhdC9kYXRh",
            "L1JlZGlyZWN0LnByb3RvInsKCU1vdmVPcmRlchIvCgRqdW1wGAEgASgLMh8u",
            "ZHdhbmdvLm5pY29saXZlLmNoYXQuZGF0YS5KdW1wSAASNwoIcmVkaXJlY3QY",
            "AiABKAsyIy5kd2FuZ28ubmljb2xpdmUuY2hhdC5kYXRhLlJlZGlyZWN0SABC",
            "BAoCdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Dwango.Nicolive.Chat.Data.JumpReflection.Descriptor, global::Dwango.Nicolive.Chat.Data.RedirectReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.MoveOrder), global::Dwango.Nicolive.Chat.Data.MoveOrder.Parser, new[]{ "Jump", "Redirect" }, new[]{ "To" }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class MoveOrder : pb::IMessage<MoveOrder> {
    private static readonly pb::MessageParser<MoveOrder> _parser = new pb::MessageParser<MoveOrder>(() => new MoveOrder());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MoveOrder> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Dwango.Nicolive.Chat.Data.MoveOrderReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MoveOrder() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MoveOrder(MoveOrder other) : this() {
      switch (other.ToCase) {
        case ToOneofCase.Jump:
          Jump = other.Jump.Clone();
          break;
        case ToOneofCase.Redirect:
          Redirect = other.Redirect.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MoveOrder Clone() {
      return new MoveOrder(this);
    }

    /// <summary>Field number for the "jump" field.</summary>
    public const int JumpFieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Dwango.Nicolive.Chat.Data.Jump Jump {
      get { return toCase_ == ToOneofCase.Jump ? (global::Dwango.Nicolive.Chat.Data.Jump) to_ : null; }
      set {
        to_ = value;
        toCase_ = value == null ? ToOneofCase.None : ToOneofCase.Jump;
      }
    }

    /// <summary>Field number for the "redirect" field.</summary>
    public const int RedirectFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Dwango.Nicolive.Chat.Data.Redirect Redirect {
      get { return toCase_ == ToOneofCase.Redirect ? (global::Dwango.Nicolive.Chat.Data.Redirect) to_ : null; }
      set {
        to_ = value;
        toCase_ = value == null ? ToOneofCase.None : ToOneofCase.Redirect;
      }
    }

    private object to_;
    /// <summary>Enum of possible cases for the "to" oneof.</summary>
    public enum ToOneofCase {
      None = 0,
      Jump = 1,
      Redirect = 2,
    }
    private ToOneofCase toCase_ = ToOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ToOneofCase ToCase {
      get { return toCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearTo() {
      toCase_ = ToOneofCase.None;
      to_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MoveOrder);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MoveOrder other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Jump, other.Jump)) return false;
      if (!object.Equals(Redirect, other.Redirect)) return false;
      if (ToCase != other.ToCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (toCase_ == ToOneofCase.Jump) hash ^= Jump.GetHashCode();
      if (toCase_ == ToOneofCase.Redirect) hash ^= Redirect.GetHashCode();
      hash ^= (int) toCase_;
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
      if (toCase_ == ToOneofCase.Jump) {
        output.WriteRawTag(10);
        output.WriteMessage(Jump);
      }
      if (toCase_ == ToOneofCase.Redirect) {
        output.WriteRawTag(18);
        output.WriteMessage(Redirect);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (toCase_ == ToOneofCase.Jump) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Jump);
      }
      if (toCase_ == ToOneofCase.Redirect) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Redirect);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MoveOrder other) {
      if (other == null) {
        return;
      }
      switch (other.ToCase) {
        case ToOneofCase.Jump:
          if (Jump == null) {
            Jump = new global::Dwango.Nicolive.Chat.Data.Jump();
          }
          Jump.MergeFrom(other.Jump);
          break;
        case ToOneofCase.Redirect:
          if (Redirect == null) {
            Redirect = new global::Dwango.Nicolive.Chat.Data.Redirect();
          }
          Redirect.MergeFrom(other.Redirect);
          break;
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
            global::Dwango.Nicolive.Chat.Data.Jump subBuilder = new global::Dwango.Nicolive.Chat.Data.Jump();
            if (toCase_ == ToOneofCase.Jump) {
              subBuilder.MergeFrom(Jump);
            }
            input.ReadMessage(subBuilder);
            Jump = subBuilder;
            break;
          }
          case 18: {
            global::Dwango.Nicolive.Chat.Data.Redirect subBuilder = new global::Dwango.Nicolive.Chat.Data.Redirect();
            if (toCase_ == ToOneofCase.Redirect) {
              subBuilder.MergeFrom(Redirect);
            }
            input.ReadMessage(subBuilder);
            Redirect = subBuilder;
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code