// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: dwango/nicolive/chat/service/edge/BackwardSegment.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Dwango.Nicolive.Chat.Service.Edge {

  /// <summary>Holder for reflection information generated from dwango/nicolive/chat/service/edge/BackwardSegment.proto</summary>
  public static partial class BackwardSegmentReflection {

    #region Descriptor
    /// <summary>File descriptor for dwango/nicolive/chat/service/edge/BackwardSegment.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BackwardSegmentReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cjdkd2FuZ28vbmljb2xpdmUvY2hhdC9zZXJ2aWNlL2VkZ2UvQmFja3dhcmRT",
            "ZWdtZW50LnByb3RvEiFkd2FuZ28ubmljb2xpdmUuY2hhdC5zZXJ2aWNlLmVk",
            "Z2UaH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAucHJvdG8aNWR3YW5nby9u",
            "aWNvbGl2ZS9jaGF0L3NlcnZpY2UvZWRnZS9QYWNrZWRTZWdtZW50LnByb3Rv",
            "ItYBCg9CYWNrd2FyZFNlZ21lbnQSKQoFdW50aWwYASABKAsyGi5nb29nbGUu",
            "cHJvdG9idWYuVGltZXN0YW1wEkYKB3NlZ21lbnQYAiABKAsyNS5kd2FuZ28u",
            "bmljb2xpdmUuY2hhdC5zZXJ2aWNlLmVkZ2UuUGFja2VkU2VnbWVudC5OZXh0",
            "ElAKCHNuYXBzaG90GAMgASgLMj4uZHdhbmdvLm5pY29saXZlLmNoYXQuc2Vy",
            "dmljZS5lZGdlLlBhY2tlZFNlZ21lbnQuU3RhdGVTbmFwc2hvdGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, global::Dwango.Nicolive.Chat.Service.Edge.PackedSegmentReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Service.Edge.BackwardSegment), global::Dwango.Nicolive.Chat.Service.Edge.BackwardSegment.Parser, new[]{ "Until", "Segment", "Snapshot" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  ///*
  /// ストリーム開始以前のメッセージを表すチャンク。必ずストリームの先頭に送られてくる。
  /// </summary>
  public sealed partial class BackwardSegment : pb::IMessage<BackwardSegment> {
    private static readonly pb::MessageParser<BackwardSegment> _parser = new pb::MessageParser<BackwardSegment>(() => new BackwardSegment());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<BackwardSegment> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Dwango.Nicolive.Chat.Service.Edge.BackwardSegmentReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BackwardSegment() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BackwardSegment(BackwardSegment other) : this() {
      until_ = other.until_ != null ? other.until_.Clone() : null;
      segment_ = other.segment_ != null ? other.segment_.Clone() : null;
      snapshot_ = other.snapshot_ != null ? other.snapshot_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public BackwardSegment Clone() {
      return new BackwardSegment(this);
    }

    /// <summary>Field number for the "until" field.</summary>
    public const int UntilFieldNumber = 1;
    private global::Google.Protobuf.WellKnownTypes.Timestamp until_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.WellKnownTypes.Timestamp Until {
      get { return until_; }
      set {
        until_ = value;
      }
    }

    /// <summary>Field number for the "segment" field.</summary>
    public const int SegmentFieldNumber = 2;
    private global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.Next segment_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.Next Segment {
      get { return segment_; }
      set {
        segment_ = value;
      }
    }

    /// <summary>Field number for the "snapshot" field.</summary>
    public const int SnapshotFieldNumber = 3;
    private global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.StateSnapshot snapshot_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.StateSnapshot Snapshot {
      get { return snapshot_; }
      set {
        snapshot_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as BackwardSegment);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(BackwardSegment other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Until, other.Until)) return false;
      if (!object.Equals(Segment, other.Segment)) return false;
      if (!object.Equals(Snapshot, other.Snapshot)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (until_ != null) hash ^= Until.GetHashCode();
      if (segment_ != null) hash ^= Segment.GetHashCode();
      if (snapshot_ != null) hash ^= Snapshot.GetHashCode();
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
      if (until_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Until);
      }
      if (segment_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Segment);
      }
      if (snapshot_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Snapshot);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (until_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Until);
      }
      if (segment_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Segment);
      }
      if (snapshot_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Snapshot);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(BackwardSegment other) {
      if (other == null) {
        return;
      }
      if (other.until_ != null) {
        if (until_ == null) {
          until_ = new global::Google.Protobuf.WellKnownTypes.Timestamp();
        }
        Until.MergeFrom(other.Until);
      }
      if (other.segment_ != null) {
        if (segment_ == null) {
          segment_ = new global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.Next();
        }
        Segment.MergeFrom(other.Segment);
      }
      if (other.snapshot_ != null) {
        if (snapshot_ == null) {
          snapshot_ = new global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.StateSnapshot();
        }
        Snapshot.MergeFrom(other.Snapshot);
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
            if (until_ == null) {
              until_ = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(until_);
            break;
          }
          case 18: {
            if (segment_ == null) {
              segment_ = new global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.Next();
            }
            input.ReadMessage(segment_);
            break;
          }
          case 26: {
            if (snapshot_ == null) {
              snapshot_ = new global::Dwango.Nicolive.Chat.Service.Edge.PackedSegment.Types.StateSnapshot();
            }
            input.ReadMessage(snapshot_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
