// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: dwango/nicolive/chat/data/Nicoad.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Dwango.Nicolive.Chat.Data {

  /// <summary>Holder for reflection information generated from dwango/nicolive/chat/data/Nicoad.proto</summary>
  public static partial class NicoadReflection {

    #region Descriptor
    /// <summary>File descriptor for dwango/nicolive/chat/data/Nicoad.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static NicoadReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiZkd2FuZ28vbmljb2xpdmUvY2hhdC9kYXRhL05pY29hZC5wcm90bxIZZHdh",
            "bmdvLm5pY29saXZlLmNoYXQuZGF0YSLSAwoGTmljb2FkEjIKAnYwGAEgASgL",
            "MiQuZHdhbmdvLm5pY29saXZlLmNoYXQuZGF0YS5OaWNvYWQuVjBIABIyCgJ2",
            "MRgCIAEoCzIkLmR3YW5nby5uaWNvbGl2ZS5jaGF0LmRhdGEuTmljb2FkLlYx",
            "SAAapAIKAlYwEjsKBmxhdGVzdBgBIAEoCzIrLmR3YW5nby5uaWNvbGl2ZS5j",
            "aGF0LmRhdGEuTmljb2FkLlYwLkxhdGVzdBI9CgdyYW5raW5nGAIgAygLMiwu",
            "ZHdhbmdvLm5pY29saXZlLmNoYXQuZGF0YS5OaWNvYWQuVjAuUmFua2luZxIT",
            "Cgt0b3RhbF9wb2ludBgDIAEoBRo8CgZMYXRlc3QSEgoKYWR2ZXJ0aXNlchgB",
            "IAEoCRINCgVwb2ludBgCIAEoBRIPCgdtZXNzYWdlGAMgASgJGk8KB1Jhbmtp",
            "bmcSEgoKYWR2ZXJ0aXNlchgBIAEoCRIMCgRyYW5rGAIgASgFEg8KB21lc3Nh",
            "Z2UYAyABKAkSEQoJdXNlcl9yYW5rGAQgASgFGi0KAlYxEhYKDnRvdGFsX2Fk",
            "X3BvaW50GAEgASgFEg8KB21lc3NhZ2UYAiABKAlCCgoIdmVyc2lvbnNiBnBy",
            "b3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.Nicoad), global::Dwango.Nicolive.Chat.Data.Nicoad.Parser, new[]{ "V0", "V1" }, new[]{ "Versions" }, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0), global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Parser, new[]{ "Latest", "Ranking", "TotalPoint" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Latest), global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Latest.Parser, new[]{ "Advertiser", "Point", "Message" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Ranking), global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Ranking.Parser, new[]{ "Advertiser", "Rank", "Message", "UserRank" }, null, null, null)}),
            new pbr::GeneratedClrTypeInfo(typeof(global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V1), global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V1.Parser, new[]{ "TotalAdPoint", "Message" }, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Nicoad : pb::IMessage<Nicoad> {
    private static readonly pb::MessageParser<Nicoad> _parser = new pb::MessageParser<Nicoad>(() => new Nicoad());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Nicoad> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Dwango.Nicolive.Chat.Data.NicoadReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Nicoad() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Nicoad(Nicoad other) : this() {
      switch (other.VersionsCase) {
        case VersionsOneofCase.V0:
          V0 = other.V0.Clone();
          break;
        case VersionsOneofCase.V1:
          V1 = other.V1.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Nicoad Clone() {
      return new Nicoad(this);
    }

    /// <summary>Field number for the "v0" field.</summary>
    public const int V0FieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0 V0 {
      get { return versionsCase_ == VersionsOneofCase.V0 ? (global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0) versions_ : null; }
      set {
        versions_ = value;
        versionsCase_ = value == null ? VersionsOneofCase.None : VersionsOneofCase.V0;
      }
    }

    /// <summary>Field number for the "v1" field.</summary>
    public const int V1FieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V1 V1 {
      get { return versionsCase_ == VersionsOneofCase.V1 ? (global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V1) versions_ : null; }
      set {
        versions_ = value;
        versionsCase_ = value == null ? VersionsOneofCase.None : VersionsOneofCase.V1;
      }
    }

    private object versions_;
    /// <summary>Enum of possible cases for the "versions" oneof.</summary>
    public enum VersionsOneofCase {
      None = 0,
      V0 = 1,
      V1 = 2,
    }
    private VersionsOneofCase versionsCase_ = VersionsOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public VersionsOneofCase VersionsCase {
      get { return versionsCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearVersions() {
      versionsCase_ = VersionsOneofCase.None;
      versions_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Nicoad);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Nicoad other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(V0, other.V0)) return false;
      if (!object.Equals(V1, other.V1)) return false;
      if (VersionsCase != other.VersionsCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (versionsCase_ == VersionsOneofCase.V0) hash ^= V0.GetHashCode();
      if (versionsCase_ == VersionsOneofCase.V1) hash ^= V1.GetHashCode();
      hash ^= (int) versionsCase_;
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
      if (versionsCase_ == VersionsOneofCase.V0) {
        output.WriteRawTag(10);
        output.WriteMessage(V0);
      }
      if (versionsCase_ == VersionsOneofCase.V1) {
        output.WriteRawTag(18);
        output.WriteMessage(V1);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (versionsCase_ == VersionsOneofCase.V0) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(V0);
      }
      if (versionsCase_ == VersionsOneofCase.V1) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(V1);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Nicoad other) {
      if (other == null) {
        return;
      }
      switch (other.VersionsCase) {
        case VersionsOneofCase.V0:
          if (V0 == null) {
            V0 = new global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0();
          }
          V0.MergeFrom(other.V0);
          break;
        case VersionsOneofCase.V1:
          if (V1 == null) {
            V1 = new global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V1();
          }
          V1.MergeFrom(other.V1);
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
            global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0 subBuilder = new global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0();
            if (versionsCase_ == VersionsOneofCase.V0) {
              subBuilder.MergeFrom(V0);
            }
            input.ReadMessage(subBuilder);
            V0 = subBuilder;
            break;
          }
          case 18: {
            global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V1 subBuilder = new global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V1();
            if (versionsCase_ == VersionsOneofCase.V1) {
              subBuilder.MergeFrom(V1);
            }
            input.ReadMessage(subBuilder);
            V1 = subBuilder;
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the Nicoad message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class V0 : pb::IMessage<V0> {
        private static readonly pb::MessageParser<V0> _parser = new pb::MessageParser<V0>(() => new V0());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<V0> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Dwango.Nicolive.Chat.Data.Nicoad.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public V0() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public V0(V0 other) : this() {
          latest_ = other.latest_ != null ? other.latest_.Clone() : null;
          ranking_ = other.ranking_.Clone();
          totalPoint_ = other.totalPoint_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public V0 Clone() {
          return new V0(this);
        }

        /// <summary>Field number for the "latest" field.</summary>
        public const int LatestFieldNumber = 1;
        private global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Latest latest_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Latest Latest {
          get { return latest_; }
          set {
            latest_ = value;
          }
        }

        /// <summary>Field number for the "ranking" field.</summary>
        public const int RankingFieldNumber = 2;
        private static readonly pb::FieldCodec<global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Ranking> _repeated_ranking_codec
            = pb::FieldCodec.ForMessage(18, global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Ranking.Parser);
        private readonly pbc::RepeatedField<global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Ranking> ranking_ = new pbc::RepeatedField<global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Ranking>();
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public pbc::RepeatedField<global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Ranking> Ranking {
          get { return ranking_; }
        }

        /// <summary>Field number for the "total_point" field.</summary>
        public const int TotalPointFieldNumber = 3;
        private int totalPoint_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int TotalPoint {
          get { return totalPoint_; }
          set {
            totalPoint_ = value;
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as V0);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(V0 other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (!object.Equals(Latest, other.Latest)) return false;
          if(!ranking_.Equals(other.ranking_)) return false;
          if (TotalPoint != other.TotalPoint) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (latest_ != null) hash ^= Latest.GetHashCode();
          hash ^= ranking_.GetHashCode();
          if (TotalPoint != 0) hash ^= TotalPoint.GetHashCode();
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
          if (latest_ != null) {
            output.WriteRawTag(10);
            output.WriteMessage(Latest);
          }
          ranking_.WriteTo(output, _repeated_ranking_codec);
          if (TotalPoint != 0) {
            output.WriteRawTag(24);
            output.WriteInt32(TotalPoint);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (latest_ != null) {
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(Latest);
          }
          size += ranking_.CalculateSize(_repeated_ranking_codec);
          if (TotalPoint != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(TotalPoint);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(V0 other) {
          if (other == null) {
            return;
          }
          if (other.latest_ != null) {
            if (latest_ == null) {
              latest_ = new global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Latest();
            }
            Latest.MergeFrom(other.Latest);
          }
          ranking_.Add(other.ranking_);
          if (other.TotalPoint != 0) {
            TotalPoint = other.TotalPoint;
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
                if (latest_ == null) {
                  latest_ = new global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Types.Latest();
                }
                input.ReadMessage(latest_);
                break;
              }
              case 18: {
                ranking_.AddEntriesFrom(input, _repeated_ranking_codec);
                break;
              }
              case 24: {
                TotalPoint = input.ReadInt32();
                break;
              }
            }
          }
        }

        #region Nested types
        /// <summary>Container for nested types declared in the V0 message type.</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static partial class Types {
          public sealed partial class Latest : pb::IMessage<Latest> {
            private static readonly pb::MessageParser<Latest> _parser = new pb::MessageParser<Latest>(() => new Latest());
            private pb::UnknownFieldSet _unknownFields;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public static pb::MessageParser<Latest> Parser { get { return _parser; } }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public static pbr::MessageDescriptor Descriptor {
              get { return global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Descriptor.NestedTypes[0]; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            pbr::MessageDescriptor pb::IMessage.Descriptor {
              get { return Descriptor; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public Latest() {
              OnConstruction();
            }

            partial void OnConstruction();

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public Latest(Latest other) : this() {
              advertiser_ = other.advertiser_;
              point_ = other.point_;
              message_ = other.message_;
              _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public Latest Clone() {
              return new Latest(this);
            }

            /// <summary>Field number for the "advertiser" field.</summary>
            public const int AdvertiserFieldNumber = 1;
            private string advertiser_ = "";
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public string Advertiser {
              get { return advertiser_; }
              set {
                advertiser_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
              }
            }

            /// <summary>Field number for the "point" field.</summary>
            public const int PointFieldNumber = 2;
            private int point_;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public int Point {
              get { return point_; }
              set {
                point_ = value;
              }
            }

            /// <summary>Field number for the "message" field.</summary>
            public const int MessageFieldNumber = 3;
            private string message_ = "";
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public string Message {
              get { return message_; }
              set {
                message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
              }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public override bool Equals(object other) {
              return Equals(other as Latest);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public bool Equals(Latest other) {
              if (ReferenceEquals(other, null)) {
                return false;
              }
              if (ReferenceEquals(other, this)) {
                return true;
              }
              if (Advertiser != other.Advertiser) return false;
              if (Point != other.Point) return false;
              if (Message != other.Message) return false;
              return Equals(_unknownFields, other._unknownFields);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public override int GetHashCode() {
              int hash = 1;
              if (Advertiser.Length != 0) hash ^= Advertiser.GetHashCode();
              if (Point != 0) hash ^= Point.GetHashCode();
              if (Message.Length != 0) hash ^= Message.GetHashCode();
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
              if (Advertiser.Length != 0) {
                output.WriteRawTag(10);
                output.WriteString(Advertiser);
              }
              if (Point != 0) {
                output.WriteRawTag(16);
                output.WriteInt32(Point);
              }
              if (Message.Length != 0) {
                output.WriteRawTag(26);
                output.WriteString(Message);
              }
              if (_unknownFields != null) {
                _unknownFields.WriteTo(output);
              }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public int CalculateSize() {
              int size = 0;
              if (Advertiser.Length != 0) {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Advertiser);
              }
              if (Point != 0) {
                size += 1 + pb::CodedOutputStream.ComputeInt32Size(Point);
              }
              if (Message.Length != 0) {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
              }
              if (_unknownFields != null) {
                size += _unknownFields.CalculateSize();
              }
              return size;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public void MergeFrom(Latest other) {
              if (other == null) {
                return;
              }
              if (other.Advertiser.Length != 0) {
                Advertiser = other.Advertiser;
              }
              if (other.Point != 0) {
                Point = other.Point;
              }
              if (other.Message.Length != 0) {
                Message = other.Message;
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
                    Advertiser = input.ReadString();
                    break;
                  }
                  case 16: {
                    Point = input.ReadInt32();
                    break;
                  }
                  case 26: {
                    Message = input.ReadString();
                    break;
                  }
                }
              }
            }

          }

          public sealed partial class Ranking : pb::IMessage<Ranking> {
            private static readonly pb::MessageParser<Ranking> _parser = new pb::MessageParser<Ranking>(() => new Ranking());
            private pb::UnknownFieldSet _unknownFields;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public static pb::MessageParser<Ranking> Parser { get { return _parser; } }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public static pbr::MessageDescriptor Descriptor {
              get { return global::Dwango.Nicolive.Chat.Data.Nicoad.Types.V0.Descriptor.NestedTypes[1]; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            pbr::MessageDescriptor pb::IMessage.Descriptor {
              get { return Descriptor; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public Ranking() {
              OnConstruction();
            }

            partial void OnConstruction();

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public Ranking(Ranking other) : this() {
              advertiser_ = other.advertiser_;
              rank_ = other.rank_;
              message_ = other.message_;
              userRank_ = other.userRank_;
              _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public Ranking Clone() {
              return new Ranking(this);
            }

            /// <summary>Field number for the "advertiser" field.</summary>
            public const int AdvertiserFieldNumber = 1;
            private string advertiser_ = "";
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public string Advertiser {
              get { return advertiser_; }
              set {
                advertiser_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
              }
            }

            /// <summary>Field number for the "rank" field.</summary>
            public const int RankFieldNumber = 2;
            private int rank_;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public int Rank {
              get { return rank_; }
              set {
                rank_ = value;
              }
            }

            /// <summary>Field number for the "message" field.</summary>
            public const int MessageFieldNumber = 3;
            private string message_ = "";
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public string Message {
              get { return message_; }
              set {
                message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
              }
            }

            /// <summary>Field number for the "user_rank" field.</summary>
            public const int UserRankFieldNumber = 4;
            private int userRank_;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public int UserRank {
              get { return userRank_; }
              set {
                userRank_ = value;
              }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public override bool Equals(object other) {
              return Equals(other as Ranking);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public bool Equals(Ranking other) {
              if (ReferenceEquals(other, null)) {
                return false;
              }
              if (ReferenceEquals(other, this)) {
                return true;
              }
              if (Advertiser != other.Advertiser) return false;
              if (Rank != other.Rank) return false;
              if (Message != other.Message) return false;
              if (UserRank != other.UserRank) return false;
              return Equals(_unknownFields, other._unknownFields);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public override int GetHashCode() {
              int hash = 1;
              if (Advertiser.Length != 0) hash ^= Advertiser.GetHashCode();
              if (Rank != 0) hash ^= Rank.GetHashCode();
              if (Message.Length != 0) hash ^= Message.GetHashCode();
              if (UserRank != 0) hash ^= UserRank.GetHashCode();
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
              if (Advertiser.Length != 0) {
                output.WriteRawTag(10);
                output.WriteString(Advertiser);
              }
              if (Rank != 0) {
                output.WriteRawTag(16);
                output.WriteInt32(Rank);
              }
              if (Message.Length != 0) {
                output.WriteRawTag(26);
                output.WriteString(Message);
              }
              if (UserRank != 0) {
                output.WriteRawTag(32);
                output.WriteInt32(UserRank);
              }
              if (_unknownFields != null) {
                _unknownFields.WriteTo(output);
              }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public int CalculateSize() {
              int size = 0;
              if (Advertiser.Length != 0) {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Advertiser);
              }
              if (Rank != 0) {
                size += 1 + pb::CodedOutputStream.ComputeInt32Size(Rank);
              }
              if (Message.Length != 0) {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
              }
              if (UserRank != 0) {
                size += 1 + pb::CodedOutputStream.ComputeInt32Size(UserRank);
              }
              if (_unknownFields != null) {
                size += _unknownFields.CalculateSize();
              }
              return size;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            public void MergeFrom(Ranking other) {
              if (other == null) {
                return;
              }
              if (other.Advertiser.Length != 0) {
                Advertiser = other.Advertiser;
              }
              if (other.Rank != 0) {
                Rank = other.Rank;
              }
              if (other.Message.Length != 0) {
                Message = other.Message;
              }
              if (other.UserRank != 0) {
                UserRank = other.UserRank;
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
                    Advertiser = input.ReadString();
                    break;
                  }
                  case 16: {
                    Rank = input.ReadInt32();
                    break;
                  }
                  case 26: {
                    Message = input.ReadString();
                    break;
                  }
                  case 32: {
                    UserRank = input.ReadInt32();
                    break;
                  }
                }
              }
            }

          }

        }
        #endregion

      }

      public sealed partial class V1 : pb::IMessage<V1> {
        private static readonly pb::MessageParser<V1> _parser = new pb::MessageParser<V1>(() => new V1());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<V1> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Dwango.Nicolive.Chat.Data.Nicoad.Descriptor.NestedTypes[1]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public V1() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public V1(V1 other) : this() {
          totalAdPoint_ = other.totalAdPoint_;
          message_ = other.message_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public V1 Clone() {
          return new V1(this);
        }

        /// <summary>Field number for the "total_ad_point" field.</summary>
        public const int TotalAdPointFieldNumber = 1;
        private int totalAdPoint_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int TotalAdPoint {
          get { return totalAdPoint_; }
          set {
            totalAdPoint_ = value;
          }
        }

        /// <summary>Field number for the "message" field.</summary>
        public const int MessageFieldNumber = 2;
        private string message_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public string Message {
          get { return message_; }
          set {
            message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as V1);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(V1 other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (TotalAdPoint != other.TotalAdPoint) return false;
          if (Message != other.Message) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (TotalAdPoint != 0) hash ^= TotalAdPoint.GetHashCode();
          if (Message.Length != 0) hash ^= Message.GetHashCode();
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
          if (TotalAdPoint != 0) {
            output.WriteRawTag(8);
            output.WriteInt32(TotalAdPoint);
          }
          if (Message.Length != 0) {
            output.WriteRawTag(18);
            output.WriteString(Message);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (TotalAdPoint != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(TotalAdPoint);
          }
          if (Message.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(V1 other) {
          if (other == null) {
            return;
          }
          if (other.TotalAdPoint != 0) {
            TotalAdPoint = other.TotalAdPoint;
          }
          if (other.Message.Length != 0) {
            Message = other.Message;
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
              case 8: {
                TotalAdPoint = input.ReadInt32();
                break;
              }
              case 18: {
                Message = input.ReadString();
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
