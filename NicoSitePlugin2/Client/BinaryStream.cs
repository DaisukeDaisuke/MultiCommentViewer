using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoSitePlugin2.Client
{
    public class BinaryStream
    {
        private List<byte> _buffer;
        private int _offset = 0;

        public BinaryStream()//byte[] data
        {
            _buffer = new List<byte>();
            _offset = 0;
        }

        public void AddBuffer(byte[] data)
        {
            _buffer.AddRange(data);
        }

        public void CheckClearBuffer()
        {
            if (_buffer.Count == _offset)
            {
                _buffer.Clear();
                _offset = 0;
            }
        }

        private (int value, int offset)? DecodeVarint(ref int offset)
        {
            int value = 0;
            int shift = 0;
            int length = _buffer.Count;
            bool more;

            do
            {
                if (length <= offset)
                {
                    return null;
                }

                byte byteValue = _buffer[offset];
                more = (byteValue & 128) != 0;
                value |= (byteValue & 127) << shift;

                if (more)
                {
                    offset++;
                    shift += 7;
                }
            } while (more);

            return (value, offset);
        }

        public byte[] FromBinary(byte[] data)
        {
            // 必要な処理を追加
            return data;
        }

        public IEnumerable<byte[]> Read()
        {
            int offset = _offset;

            while (true)
            {
                var result = DecodeVarint(ref offset);
                if (result == null)
                {
                    break;
                }

                int value = result.Value.value;
                int newOffset = result.Value.offset;
                int start = newOffset + 1;
                int end = start + value;

                if (_buffer.Count < end)
                {
                    break;
                }

                offset = end;
                _offset = end;
                byte[] binaryData = _buffer.GetRange(start, end - start).ToArray();
                yield return binaryData;
            }

            //if (offset > 0)
            //{
            //    _buffer = _buffer.GetRange(offset, _buffer.Count - offset);
            //}
        }
    }
}
